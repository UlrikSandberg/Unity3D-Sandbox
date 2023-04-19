using SandScript.Interpreter.Runtime;

namespace SandScript.Interpreter.Native
{
    public class NullObject : RuntimeObject
    {
        public override TypeInfo TypeInfo { get; } = TypeInfo.Null;
    }
}