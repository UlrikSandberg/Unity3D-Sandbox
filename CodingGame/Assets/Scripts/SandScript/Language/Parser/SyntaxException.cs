using System;

namespace SandScript.Language.Parser
{
    public class SyntaxException : Exception
    {
        public SyntaxException(string message) : base(message)
        {
        }
    }
}