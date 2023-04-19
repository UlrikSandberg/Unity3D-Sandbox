using System.Linq;

namespace SandScript.Language.Lexer
{
    public partial class SandScriptLexer
    {
        private Token ScanIdentifier()
        {
            if (!IsIdentifier)
                throw new LexicalException(nameof(ScanIdentifier), "_ or alphanumeric", Current.ToString());

            while (IsLetterOrDigit || Current == '_')
            {
                if (IsEOF)
                    throw new LexicalException("Encountered unexpected EOF");
                
                Consume();
            }

            return keywords.Contains(tokenBuilder.ToString())
                ? CreateToken(TokenType.Keyword)
                : CreateToken(TokenType.Identifier);
        }

        private Token ScanWhitespace()
        {
            if (!IsWhitespace)
                throw new LexicalException(nameof(ScanWhitespace), "whitespace", Current.ToString());

            while (IsWhitespace)
            {
                Consume();
            }

            return CreateToken(TokenType.Whitespace);
        }

        private Token ScanOperator()
        {
            if (!IsOperator)
                throw new LexicalException(nameof(ScanOperator), operators, Current.ToString());

            return Current switch
            {
                '=' => Consume(TokenType.Assignment),
                '+' => Consume(TokenType.Addition),
                '-' => Consume(TokenType.Subtraction),
                '*' => Consume(TokenType.Multiplication),
                '/' => Consume(TokenType.Divition),
                '%' => Consume(TokenType.Modulu),
                _ => throw new LexicalException("Encountered unexpected token while scanning for operator")
            };
        }

        private Token ScanPunctuation()
        {
            if (!IsPunctuation)
                throw new LexicalException(nameof(ScanPunctuation), punctuation, Current.ToString());

            return Current switch
            {
                '.' => Consume(TokenType.Dot),
                '(' => Consume(TokenType.LeftParen),
                ')' => Consume(TokenType.RightParen),
                ',' => Consume(TokenType.Comma),
                '[' => Consume(TokenType.LeftSquare),
                ']' => Consume(TokenType.RightSquare),
                _ => throw new LexicalException("Encountered unexpected token while scanning for punctuation")
            };

        }

        private Token ScanComment()
        {
            Consume();

            if (Current == '/')
            {
                while (!IsNewline)
                {
                    if (IsEOF)
                        throw new LexicalException("Encountered unexpected EOF");
                        
                    Consume();
                }
                
                return CreateToken(TokenType.LineComment);
            }

            if (Current == '*')
            {
                while (!(Current == '*' && Next == '/'))
                {
                    if (IsEOF)
                        throw new LexicalException("Encountered unexpected EOF");
                    
                    Consume();
                }

                Consume();
                Consume();

                return CreateToken(TokenType.BlockComment);
            }

            throw new LexicalException(nameof(ScanComment), "Unexpected token scanning comment", Current.ToString());
        }

        private Token ScanNumber(bool scanningFloat = false)
        {
            if (!IsDigit)
                throw new LexicalException(nameof(ScanNumber), "Expected integer between 0-9", Current.ToString());

            while (IsDigit)
            {
                Consume();
            }

            if (Current == 'f')
            {
                Advance();
                return CreateToken(TokenType.FloatingPointLiteral);
            }

            if (!scanningFloat && Current == '.')
            {
                Consume();
                return ScanNumber(true);
            }

            if (scanningFloat)
                return CreateToken(TokenType.FloatingPointLiteral);
            
            return CreateToken(TokenType.IntegerLiteral);
        }

        private Token ScanNewline()
        {
            if (!IsNewline)
                throw new LexicalException(nameof(ScanNewline), '\n', Current);
            
            Consume();
            return CreateToken(TokenType.Newline);
        }
    }
}