using System.Collections.Generic;
using System.Linq;
using SandScript.Interpreter.Interop;

namespace SandScript.Interpreter.Runtime
{
    public class SandScriptEnvironment
    {
        private readonly List<Reference> _references = new ();

        public bool IsDefined(string referenceName) => _references.Any(r => r.ReferenceName == referenceName);

        public void AddReference(string referenceName, RuntimeObject value)
        {
            if (IsDefined(referenceName))
                throw new RuntimeException($"Identifier {referenceName} is already defined");

            _references.Add(new Reference(referenceName, value));
        }

        public void SetReference(string referenceName, RuntimeObject value)
        {
            if (IsDefined(referenceName))
            {
                _references.First(r => r.ReferenceName == referenceName).Value = value;
            }
            else
            {
                AddReference(referenceName, value);
            }
        }

        public Reference GetReference(string referenceName) => _references.Find(r => r.ReferenceName == referenceName);
    }
}