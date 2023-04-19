using SandScript.Interpreter.Runtime;

namespace SandScript.Interpreter.Native
{
    public class StringObject : RuntimeObject
    {
        public override TypeInfo TypeInfo { get; } = TypeInfo.String;
    }
}