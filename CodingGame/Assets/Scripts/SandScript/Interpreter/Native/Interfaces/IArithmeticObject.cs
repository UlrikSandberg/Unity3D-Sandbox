using SandScript.Interpreter.Runtime;
using SandScript.Language.Syntax.Expressions.Enums;

namespace SandScript.Interpreter.Native.Interfaces
{
    public interface IArithmeticSupport
    {
        Completion PerformArithmetic(IArithmeticSupport runtimeObject, ArithmeticOperator @operator);
    }
}