namespace SandScript.Language.Syntax.Statements
{
    public class ImportStatement : Statement
    {
        public string ModuleName { get; }

        public ImportStatement(string moduleName)
        {
            ModuleName = moduleName;
        }
    }
}