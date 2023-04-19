using System;
using System.Collections.Generic;
using System.Text;

namespace SandScript.Language.Lexer
{
    public partial class SandScriptLexer
    {
        private readonly string[] keywords = { "var", "import" };
        private readonly string punctuation = "(){}[].,";
        private readonly string operators = "+-*/%=";
        
        private SourceCode sourceCode;
        private StringBuilder tokenBuilder;

        private int index = 0;

        private char Current => sourceCode.CharAt(index);
        private char Last => sourceCode.CharAt(index - 1);
        private char Next => sourceCode.CharAt(index + 1);
        
        public SandScriptLexer()
        {
            tokenBuilder = new StringBuilder();
        }

        public IEnumerable<Token> LexSourceCode(string sourceCode)
        {
            Reset();
            this.sourceCode = new SourceCode(sourceCode);
            return LexTokens();
        }

        private Token CreateToken(TokenType tokenType)
        {
            var value = tokenBuilder.ToString();
            tokenBuilder.Clear();
            return new Token(value, tokenType);
        }
        
        private IEnumerable<Token> LexTokens()
        {
            yield return CreateToken(TokenType.BOF);
            
            while (!IsEOF)
            {
                yield return LexToken();
            }

            yield return CreateToken(TokenType.EOF);
        }

        private Token LexToken()
        {
            if (IsComment)
                return ScanComment();
            
            if (IsIdentifier)
                return ScanIdentifier();

            if (IsWhitespace)
                return ScanWhitespace();

            if (IsNewline)
                return ScanNewline();

            if (IsOperator)
                return ScanOperator();

            if (IsDigit)
                return ScanNumber();

            if (IsPunctuation)
                return ScanPunctuation();
            
            throw new Exception("Unexpected token during lexing.");
        }

        private void Consume()
        {
            tokenBuilder.Append(Current);
            index++;
        }

        private Token Consume(TokenType tokenType)
        {
            tokenBuilder.Append(Current);
            index++;
            return CreateToken(tokenType);
        }

        private void Advance()
        {
            index++;
        }

        private void Reset()
        {
            tokenBuilder.Clear();
            index = 0;
        }
        
        // Is section
        private bool IsEOF => Current == '\0';
        private bool IsNewline => Current == '\n';
        private bool IsIdentifier => char.IsLetter(Current) || Current == '_';
        private bool IsLetterOrDigit => char.IsLetterOrDigit(Current);
        private bool IsWhitespace => Current == ' ';
        private bool IsOperator => operators.Contains(Current);
        private bool IsPunctuation => punctuation.Contains(Current);
        private bool IsDigit => char.IsDigit(Current) && !IsEOF;
        private bool IsComment => Current == '/' && (Next == '/' || Next == '*') && !IsEOF;

    }
}