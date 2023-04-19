using System.Reflection;
using SandScript.Interpreter.Native.Interfaces;
using SandScript.Interpreter.Runtime;

namespace SandScript.Interpreter.Interop
{
    public class ClrMethod : ICallable
    {
        private readonly object _parent;
        private readonly MethodInfo _methodInfo;

        public ClrMethod(object parent, MethodInfo methodInfo)
        {
            _parent = parent;
            _methodInfo = methodInfo;
        }
        
        public RuntimeObject Invoke(params object[] args)
        {
            var method = _parent.GetType().GetMethod(_methodInfo.Name);
            var result = method.Invoke(_parent, args.CastToSingles());
            return result.CastToRuntimeObject();
        }

        public string GetMethodName()
        {
            return _methodInfo.Name;
        }
    }
}