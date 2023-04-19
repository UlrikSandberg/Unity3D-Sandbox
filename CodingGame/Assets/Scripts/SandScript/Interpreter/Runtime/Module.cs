using SandScript.Interpreter.Interop;

namespace SandScript.Interpreter.Runtime
{
    public class Module : ClrObject
    {
        public string ModuleName { get; }
        
        public Module(string moduleName, object o) : base (o)
        {
            ModuleName = moduleName;
        }
    }
}