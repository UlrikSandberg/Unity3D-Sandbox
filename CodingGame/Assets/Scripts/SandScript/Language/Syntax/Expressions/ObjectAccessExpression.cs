using System.Collections.Generic;

namespace SandScript.Language.Syntax.Expressions
{
    public class ObjectAccessExpression : Expression
    {
        public Stack<IdentifierExpression> ObjectAccessChain { get; }

        public ObjectAccessExpression(Stack<IdentifierExpression> objectAccessChain)
        {
            ObjectAccessChain = objectAccessChain;
        }
    }
}