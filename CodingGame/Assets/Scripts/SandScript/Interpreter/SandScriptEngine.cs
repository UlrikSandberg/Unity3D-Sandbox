using System;
using System.Collections.Generic;
using System.Linq;
using SandScript.Interpreter.Interop;
using SandScript.Interpreter.Runtime;
using SandScript.Language.Parser;
using SandScript.Language.Syntax.Expressions;
using SandScript.Language.Syntax.Statements;

namespace SandScript.Interpreter
{
    public class SandScriptEngine
    {
        private SandScriptParser sandScriptParser;
        private List<Module> moduleLoader = new List<Module>();
        
        // Environment
        private Stack<SandScriptEnvironment> executionContextStack = new ();
        private SandScriptEnvironment rootContext;
        private SandScriptEnvironment executionContext => executionContextStack.Peek();
        
        // Interpreter helpers
        private readonly ExpressionInterpreter _expressionInterpreter;
        private readonly StatementInterpreter _statementInterpreter;
        
        public SandScriptEngine()
        {
            rootContext = new SandScriptEnvironment();
            executionContextStack.Push(rootContext);
            sandScriptParser = new SandScriptParser();

            _expressionInterpreter = new ExpressionInterpreter(this);
            _statementInterpreter = new StatementInterpreter(this);
        }

        public Completion Execute(string sourceCode)
        {
            var syntaxRoot = sandScriptParser.ParseScript(sourceCode);

            var result = Evaluate(syntaxRoot);
            return result;
        }

        public Completion Evaluate(SyntaxNode node)
        {
            return node switch
            {
                SyntaxRoot root => EvaluateSyntaxRoot(root),
                Statement statement => _statementInterpreter.EvaluateStatement(statement),
                Expression expression => _expressionInterpreter.EvaluateExpression(expression),
                _ => throw new NotImplementedException("not implemented")
            };
        }

        private Completion EvaluateSyntaxRoot(SyntaxRoot root)
        {
            Completion last = null;
            
            foreach (var rootNode in root.RootNodes)
            {
                last = Evaluate(rootNode);
            }

            return last;
        }

        public void LoadModule(Module module)
        {
            moduleLoader.Add(module);
        }

        public RuntimeObject ImportModuleToEnvironment(string moduleName)
        {
            var module = moduleLoader.FirstOrDefault(module => module.ModuleName == moduleName);
            if (module == null)
                throw new RuntimeException($"Module loader could not find module by the name: '{moduleName}'");
            
            SetReference(moduleName, module);
            return module;
        }

        public RuntimeObject GetValue(string referenceName)
        {
            var runtimeReference = executionContext.GetReference(referenceName);
            if (runtimeReference == null)
                throw new RuntimeException($"Could not find reference: {referenceName}");

            return runtimeReference.Value;
        }

        public bool IsDefined(string referenceName) => executionContext.IsDefined(referenceName);
        public void SetReference(string referenceName, RuntimeObject value) =>
            executionContext.SetReference(referenceName, value);

        public RuntimeObject CallRootMethod(IdentifierExpression methodName, params object[] args)
        {
            throw new RuntimeException($"Method: '{methodName.Identifier}' could not be found");
        }
    }
}