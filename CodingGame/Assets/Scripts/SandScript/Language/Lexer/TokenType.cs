namespace SandScript.Language.Lexer
{
    public enum TokenType
    {
        // Global
        BOF,
        EOF,
        Error,
        
        // Literals
        IntegerLiteral,
        FloatingPointLiteral,
        StringLiteral,
        
        
        // Identifiers
        Keyword,
        Identifier,
        
        // Trivia
        Whitespace,
        LineComment,
        BlockComment,
        Newline,
        
        // Operators
        Assignment,
        Addition,
        Subtraction,
        Multiplication,
        Divition,
        Modulu,
        
        // Punctuation
        Comma,
        SemiColon,
        Dot,
        LeftParen,
        RightParen,
        LeftSquare,
        RightSquare,
        LeftBracket,
        RightBracket,
        
    }
}