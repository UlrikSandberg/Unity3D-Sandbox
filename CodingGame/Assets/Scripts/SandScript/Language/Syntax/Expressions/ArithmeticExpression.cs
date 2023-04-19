using SandScript.Language.Parser;
using SandScript.Language.Syntax.Expressions.Enums;

namespace SandScript.Language.Syntax.Expressions
{
    public class ArithmeticExpression : Expression
    {
        public SyntaxNode Left { get; }
        public SyntaxNode Right { get; }
        public ArithmeticOperator ArithmeticOperator { get; }

        public ArithmeticExpression(SyntaxNode left, SyntaxNode right, ArithmeticOperator arithmeticOperator)
        {
            Left = left;
            Right = right;
            ArithmeticOperator = arithmeticOperator;
        }
    }
}