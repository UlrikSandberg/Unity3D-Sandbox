using SandScript.Interpreter.Runtime;

namespace SandScript.Interpreter.Native.Interfaces
{
    public interface ICallable
    {
        public RuntimeObject Invoke(params object[] args);
        public string GetMethodName();
    }
}