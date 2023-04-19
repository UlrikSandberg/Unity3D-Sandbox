using System;
using System.Collections.Generic;
using System.Linq;
using SandScript.Language.Lexer;

namespace SandScript.Language.Parser
{
    public partial class SandScriptParser
    {
        private readonly SandScriptLexer _sandScriptLexer;

        private List<Token> tokens = new();
        private int index = 1;

        private Token Current => tokens[index];
        private Token Next => tokens[index + 1 == tokens.Count ? index : index + 1];
        private Token Last => tokens[index == 0 ? 0 : index - 1];
        
        public SandScriptParser()
        {
            _sandScriptLexer = new SandScriptLexer();
        }

        public SyntaxNode ParseScript(string sourceCode)
        {
            InitializeParser(_sandScriptLexer.LexSourceCode(sourceCode).Where(t => t.TokenCategory != TokenCategory.Trivia));

            var rootNodes = new List<SyntaxNode>();
            
            while (Current != TokenType.EOF)
            {
                rootNodes.Add(ParseStatement());
            }
            
            return new SyntaxRoot(rootNodes);
        }

        private void InitializeParser(IEnumerable<Token> tokens)
        {
            this.tokens = new List<Token>(tokens);
            index = 1;
        }

        private Token ConsumeToken(TokenType tokenType)
        {
            if (Current.TokenType != tokenType)
                throw new SyntaxException("Unexpected token");
            
            var current = Current;
            index++;
            return current;
        }

        private T ConsumeToken<T>(Func<T> func)
        {
            var current = func();
            index++;
            return current;
        }
    }
}