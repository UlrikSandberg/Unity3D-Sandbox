namespace SandScript.Interpreter.Runtime
{
    public class Reference
    {
        public RuntimeObject Value { get; set; }
        public string ReferenceName { get; }

        public Reference(string referenceName)
        {
            ReferenceName = referenceName;
        }

        public Reference(string referenceName, RuntimeObject value) : this(referenceName)
        {
            Value = value;
        }
    }
}