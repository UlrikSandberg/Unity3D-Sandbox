using SandScript.Language.Lexer;
using SandScript.Language.Syntax.Statements;

namespace SandScript.Language.Parser
{
    public partial class SandScriptParser
    {
        public SyntaxNode ParseStatement()
        {
            if (Current.TokenType == TokenType.Keyword)
            {
                if (Current.Value == "var")
                {
                    return ParseVariableDeclaration();
                }

                if (Current.Value == "import")
                {
                    return ParseImportStatement();
                }

                throw new SyntaxException($"Keyword {Current.Value} not supported by the parser.");
            }

            if (Current.TokenType == TokenType.Identifier)
            {
                return ParseExpression();
            }

            throw new SyntaxException($"Unexpected token: {Current.Value} when parsing statement");
        }

        public SyntaxNode ParseImportStatement()
        {
            ConsumeToken(TokenType.Keyword);
            return new ImportStatement(ConsumeToken(TokenType.Identifier).Value);
        }
    }
}