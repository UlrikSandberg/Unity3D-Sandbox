using SandScript.Language.Syntax.Expressions.Enums;

namespace SandScript.Language.Syntax.Expressions
{
    public class ConstantExpression : Expression
    {
        public string Value { get; }
        public ConstantType ConstantType { get; }

        public ConstantExpression(string value, ConstantType constantType)
        {
            Value = value;
            ConstantType = constantType;
        }
    }
}