using SandScript.Language.Lexer;
using SandScript.Language.Syntax.Declarations;

namespace SandScript.Language.Parser
{
    public partial class SandScriptParser
    {
        private SyntaxNode ParseVariableDeclaration()
        {
            var type = Current.Value == "var" 
                ? ConsumeToken(TokenType.Keyword).Value 
                : ConsumeToken(TokenType.Identifier).Value;
            
            var name = ConsumeToken(TokenType.Identifier).Value;
            
            // Ingest the assignment token
            ConsumeToken(TokenType.Assignment);
            
            var value = ParseExpression();

            return new VariableDeclaration(type, name, value);
        }
    }
}