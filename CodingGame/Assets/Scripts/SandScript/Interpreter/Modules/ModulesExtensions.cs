using SandScript.Interpreter.Runtime;

namespace SandScript.Interpreter.Modules
{
    public static class ModulesExtensions
    {
        public static Module MathModule()
        {
            return new Module("math", new MathModule());
        }
    }
}