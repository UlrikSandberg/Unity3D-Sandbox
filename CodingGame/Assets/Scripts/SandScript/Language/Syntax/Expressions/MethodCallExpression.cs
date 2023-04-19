namespace SandScript.Language.Syntax.Expressions
{
    public class MethodCallExpression : Expression
    {
        public Expression Target { get; } = null;
        public Expression MethodName { get; }
        public Expression[] Args { get; }

        public MethodCallExpression(Expression methodName, Expression target, params Expression[] args)
        {
            Target = target;
            MethodName = methodName;
            Args = args;
        }
    }
}