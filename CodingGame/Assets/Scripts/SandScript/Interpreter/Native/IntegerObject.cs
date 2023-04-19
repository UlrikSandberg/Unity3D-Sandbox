using SandScript.Interpreter.Native.Interfaces;
using SandScript.Interpreter.Runtime;
using SandScript.Language.Syntax.Expressions.Enums;

namespace SandScript.Interpreter.Native
{
    public class IntegerObject : RuntimeObject, IArithmeticSupport
    {
        public long Value { get; }
        public override TypeInfo TypeInfo { get; } = TypeInfo.Integer;

        public IntegerObject(long value)
        {
            Value = value;
        }

        public IntegerObject(string value)
        {
            Value = long.Parse(value);
        }

        public override string ToString()
        {
            return Value.ToString();
        }

        public Completion PerformArithmetic(IArithmeticSupport runtimeObject, ArithmeticOperator @operator)
        {
            return runtimeObject switch
            {
                IntegerObject integerObject => @operator switch
                {
                    ArithmeticOperator.Plus => new Completion(new IntegerObject(Value + integerObject.Value)),
                    ArithmeticOperator.Minus => new Completion(new IntegerObject(Value - integerObject.Value)),
                    ArithmeticOperator.Multiply => new Completion(new IntegerObject(Value * integerObject.Value)),
                    ArithmeticOperator.Divide => new Completion(new IntegerObject(Value / integerObject.Value)),
                    ArithmeticOperator.Modulo => new Completion(new IntegerObject(Value % integerObject.Value))
                },
                FloatObject floatObject => @operator switch
                {
                    ArithmeticOperator.Plus => new Completion(new FloatObject(Value + floatObject.Value)),
                    ArithmeticOperator.Minus => new Completion(new FloatObject(Value - floatObject.Value)),
                    ArithmeticOperator.Multiply => new Completion(new FloatObject(Value * floatObject.Value)),
                    ArithmeticOperator.Divide => new Completion(new FloatObject(Value / floatObject.Value)),
                    ArithmeticOperator.Modulo => new Completion(new FloatObject(Value % floatObject.Value))
                },
                _ => throw new RuntimeException($"Integer object does not support arithmetic operation with {runtimeObject}")
            };
        }
    }
}