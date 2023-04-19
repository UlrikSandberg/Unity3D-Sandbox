namespace SandScript.Language.Lexer
{
    public class Token
    {
        public string Value { get; }

        public TokenType TokenType { get; }

        public Token(string value, TokenType tokenType)
        {
            Value = value;
            TokenType = tokenType;
        }

        public TokenCategory TokenCategory => TokenType switch
        {
            TokenType.EOF => TokenCategory.Global,
            TokenType.BOF => TokenCategory.Global,
            
            TokenType.Identifier => TokenCategory.Identifier,
            TokenType.Keyword => TokenCategory.Identifier,
            
            TokenType.Whitespace => TokenCategory.Trivia,
            TokenType.Newline => TokenCategory.Trivia,
            TokenType.LineComment => TokenCategory.Trivia,
            TokenType.BlockComment => TokenCategory.Trivia,
            
            TokenType.Addition => TokenCategory.Arithmetic,
            TokenType.Subtraction => TokenCategory.Arithmetic,
            TokenType.Multiplication => TokenCategory.Arithmetic,
            TokenType.Divition => TokenCategory.Arithmetic,
            TokenType.Modulu => TokenCategory.Arithmetic,
            
            TokenType.Assignment => TokenCategory.Assignment,
            
            TokenType.IntegerLiteral => TokenCategory.Literal,
            TokenType.FloatingPointLiteral => TokenCategory.Literal,
            TokenType.StringLiteral => TokenCategory.Literal,

            TokenType.SemiColon => TokenCategory.Punctuation,
            TokenType.Dot => TokenCategory.Punctuation,
            TokenType.LeftParen => TokenCategory.Punctuation,
            TokenType.RightParen => TokenCategory.Punctuation,
            TokenType.LeftSquare => TokenCategory.Punctuation,
            TokenType.RightSquare => TokenCategory.Punctuation,
            TokenType.LeftBracket => TokenCategory.Punctuation,
            TokenType.RightBracket => TokenCategory.Punctuation,
            
            _ => TokenCategory.Error
        };

        public static bool operator ==(Token a, TokenType b) => a.TokenType == b;
        public static bool operator !=(Token a, TokenType b) => !(a == b);
    }
}