using System.Collections.Generic;
using System.Linq;
using SandScript.Interpreter.Native;
using SandScript.Interpreter.Native.Interfaces;
using SandScript.Language.Syntax.Expressions;

namespace SandScript.Interpreter.Runtime
{
    /// <summary>
    /// All values: literals, functions, classes etc, all exists as a base RuntimeObject
    /// </summary>
    public abstract class RuntimeObject
    {
        private List<ICallable> methods = new ();
        private List<RuntimeField> fields = new ();
        
        /// <summary>
        /// All runtime objects needs to be of some type
        /// </summary>
        public abstract TypeInfo TypeInfo { get; }

        public static RuntimeObject Null => new NullObject();
        
        public virtual string TypeName => TypeInfo.ToString().ToLower();

        public RuntimeObject GetValue(string referenceName)
        {
            return fields.First(e => e.FieldName == referenceName).GetValue();
        }

        public RuntimeObject CallMethod(IdentifierExpression methodName, params object[] args)
        {
            var callable = methods.First(method =>  method.GetMethodName() == methodName.Identifier);
            return callable.Invoke(args);
        }

        public void BindField(RuntimeField runtimeField)
        {
            if (fields.Any(field => field.FieldName == runtimeField.FieldName))
                throw new RuntimeException($"Can not bind runtime field. Field {runtimeField.FieldName} already declared.");
            
            fields.Add(runtimeField);
        }

        public void BindMethod(ICallable callable)
        {
            if (methods.Any(method => method.GetMethodName() == callable.GetMethodName()))
                throw new RuntimeException(
                    $"Can not bind runtime method. Method {callable.GetMethodName()} already declared");
            
            methods.Add(callable);
        }
    }
}