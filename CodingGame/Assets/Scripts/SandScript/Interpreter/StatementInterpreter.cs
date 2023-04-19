using System;
using SandScript.Interpreter.Runtime;
using SandScript.Language.Syntax.Declarations;
using SandScript.Language.Syntax.Statements;

namespace SandScript.Interpreter
{
    public class StatementInterpreter
    {
        private readonly SandScriptEngine _sandScriptEngine;

        public StatementInterpreter(SandScriptEngine sandScriptEngine)
        {
            _sandScriptEngine = sandScriptEngine;
        }
        
        public Completion EvaluateStatement(Statement statement)
        {
            return statement switch
            {
                VariableDeclaration varDeclaration => DeclareVariable(varDeclaration),
                ImportStatement importStatement => ImportModule(importStatement),
                _ => throw new NotImplementedException("not implemented")
            };
        }

        private Completion ImportModule(ImportStatement importStatement)
        {
            var module = _sandScriptEngine.ImportModuleToEnvironment(importStatement.ModuleName);
            return new Completion(false, module);
        }

        private Completion DeclareVariable(VariableDeclaration variableDeclaration)
        {
            // Check if the variable has already been declared in the current execution context
            if (_sandScriptEngine.IsDefined(variableDeclaration.Name))
                throw new RuntimeException($"Variable {variableDeclaration.Name} already defined");
            
            // Evaluate variable declaration value SyntaxNode and set reference in runtime.
            var value = _sandScriptEngine.Evaluate(variableDeclaration.Value).Value;
            _sandScriptEngine.SetReference(variableDeclaration.Name, value);
            
            return new Completion(false, value);
        }
    }
}