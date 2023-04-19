namespace SandScript.Language.Lexer
{
    public enum TokenCategory
    {
        Global,
        
        
        // Keyword, identifier
        Identifier,
        // Line comment, block comment, newline and whitespacea
        Trivia,
        Arithmetic,
        Assignment,
        Literal,
        Error,
        Punctuation
    }
}