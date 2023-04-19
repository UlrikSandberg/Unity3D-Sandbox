using System;
using System.Collections.Generic;
using System.Linq;
using SandScript.Interpreter.Interop;
using SandScript.Interpreter.Native;
using SandScript.Interpreter.Native.Interfaces;
using SandScript.Interpreter.Runtime;
using SandScript.Language.Syntax.Expressions;
using SandScript.Language.Syntax.Expressions.Enums;

namespace SandScript.Interpreter
{
    public class ExpressionInterpreter
    {
        private readonly SandScriptEngine _sandScriptEngine;

        public ExpressionInterpreter(SandScriptEngine sandScriptEngine)
        {
            _sandScriptEngine = sandScriptEngine;
        }
        
        public Completion EvaluateExpression(Expression expression) => expression switch
        {
            ConstantExpression constExp => EvaluateConstantExpression(constExp),
            IdentifierExpression identifierExp => EvaluateIdentifierExpression(identifierExp),
            ObjectAccessExpression accessExpression => EvaluateObjectAccessExpression(accessExpression),
            ArithmeticExpression arithmeticExp => EvaluateArithmeticExpression(arithmeticExp),
            MethodCallExpression methodCallExp => EvaluateMethodCallExpression(methodCallExp),
            _ => throw new NotImplementedException($"Evaluation of {expression} not yet implemented")
        };

        private Completion EvaluateObjectAccessExpression(ObjectAccessExpression expression)
        {
            RuntimeObject runtimeObject = null;
            
            foreach(var identifierExpression in expression.ObjectAccessChain)
            {
                if (runtimeObject == null)
                {
                    runtimeObject = _sandScriptEngine.GetValue(identifierExpression.Identifier);
                }
                else
                {
                    runtimeObject = runtimeObject.GetValue(identifierExpression.Identifier);
                }
            }
            
            return new Completion(runtimeObject);
        }

        private Completion EvaluateMethodCallExpression(MethodCallExpression methodCallExpression)
        {
            if (methodCallExpression.MethodName is IdentifierExpression methodIdentifier)
            {
                var args = methodCallExpression.Args
                    .Select(EvaluateExpression)
                    .Select(e => e.Value)
                    .Select(e => e.CastToClr())
                    .ToArray();
                // This means that the method/function is defined at the root level
                if (methodCallExpression.Target == null)
                {
                    var result =_sandScriptEngine.CallRootMethod(methodIdentifier, args);
                    return new Completion(false, result);
                }
                else
                {
                    var target = EvaluateExpression(methodCallExpression.Target).Value;
                    var result = target.CallMethod(methodIdentifier, args);
                    return new Completion(false, result);
                }
            }
            
            throw new RuntimeException($"Found invalid method name identifier when interpreting MethodCallExpression");
        }
        
        private Completion EvaluateConstantExpression(ConstantExpression expression) => expression.ConstantType switch
        {
            ConstantType.Integer => new Completion(false, new IntegerObject(expression.Value)),
            ConstantType.Float => new Completion(false, new FloatObject(expression.Value)),
            _ => throw new NotImplementedException(
                $"Evaluate of constant type {expression.ConstantType} not yet implemented.")
        };

        private Completion EvaluateIdentifierExpression(IdentifierExpression expression)
        {
            var runtimeValue = _sandScriptEngine.GetValue(expression.Identifier);
            return new Completion(runtimeValue);
        }

        private Completion EvaluateArithmeticExpression(ArithmeticExpression expression)
        {
            var left = _sandScriptEngine.Evaluate(expression.Left);
            var right = _sandScriptEngine.Evaluate(expression.Right);

            if (left.Value is IArithmeticSupport leftArithmetic && right.Value is IArithmeticSupport rightArithmetic)
            {
                return leftArithmetic.PerformArithmetic(rightArithmetic, expression.ArithmeticOperator);
            }
            
            throw new RuntimeException($"Can't perform {expression.ArithmeticOperator} on {left.Value} and {right.Value}");
        }
    }
}