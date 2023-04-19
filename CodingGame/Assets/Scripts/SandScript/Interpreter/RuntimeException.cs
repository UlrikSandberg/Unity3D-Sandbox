using System;

namespace SandScript.Interpreter
{
    public class RuntimeException : Exception
    {
        public RuntimeException(string message) : base(message)
        {
            
        }
    }
}