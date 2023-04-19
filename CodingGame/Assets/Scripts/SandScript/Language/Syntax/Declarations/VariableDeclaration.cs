using SandScript.Language.Parser;
using SandScript.Language.Syntax.Statements;

namespace SandScript.Language.Syntax.Declarations
{
    public class VariableDeclaration : Statement
    {
        public string Type { get; }
        public string Name { get; }
        public SyntaxNode Value { get; }
        
        public VariableDeclaration(string type, string name, SyntaxNode value)
        {
            Type = type;
            Name = name;
            Value = value;
        }
    }
}