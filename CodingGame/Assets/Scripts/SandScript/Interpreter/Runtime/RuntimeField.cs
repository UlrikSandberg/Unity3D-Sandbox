using SandScript.Interpreter.Native;

namespace SandScript.Interpreter.Runtime
{
    public abstract class RuntimeField
    {
        public abstract RuntimeObject GetValue();
        public abstract void SetValue(RuntimeObject value);
        
        public abstract string FieldName { get; }
        
        public abstract TypeInfo TypeInfo { get; }
    }
}