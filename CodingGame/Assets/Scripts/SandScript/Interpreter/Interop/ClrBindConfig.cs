using System.Collections.Generic;

namespace SandScript.Interpreter.Interop
{
    public class ClrBindConfig
    {
        public List<string> FieldNames { get; } = new();
        public List<string> MethodNames { get; } = new();


        public ClrBindConfig(List<string> fieldNames = null, List<string> methodNames = null)
        {
            if (fieldNames != null)
                FieldNames = fieldNames;
            
            if (methodNames != null)
                MethodNames = methodNames;
        }
    }
}