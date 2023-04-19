namespace SandScript.Interpreter.Runtime
{
    /// <summary>
    /// Completion is the product of evaluating parts of an AST.
    /// </summary>
    public class Completion
    {
        public bool ShouldReturn { get; }
        public RuntimeObject Value { get; }

        public Completion(RuntimeObject value)
        {
            ShouldReturn = false;
            Value = value;
        }
        
        public Completion(bool shouldReturn, RuntimeObject value)
        {
            ShouldReturn = shouldReturn;
            Value = value;
        }
    }
}