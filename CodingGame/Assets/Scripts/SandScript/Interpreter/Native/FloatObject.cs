using SandScript.Interpreter.Native.Interfaces;
using SandScript.Interpreter.Runtime;
using SandScript.Language.Syntax.Expressions.Enums;

namespace SandScript.Interpreter.Native
{
    public class FloatObject : RuntimeObject, IArithmeticSupport
    {
        public override TypeInfo TypeInfo { get; } = TypeInfo.Float;

        public double Value { get; }
        
        public FloatObject(double value)
        {
            Value = value;
        }

        public FloatObject(float value)
        {
            Value = value;
        }

        public FloatObject(string value)
        {
            Value = double.Parse(value);
        }

        public Completion PerformArithmetic(IArithmeticSupport runtimeObject, ArithmeticOperator @operator)
        {
            return runtimeObject switch
            {
                IntegerObject integerObject => @operator switch
                {
                    ArithmeticOperator.Plus => new Completion(new FloatObject(Value + integerObject.Value)),
                    ArithmeticOperator.Minus => new Completion(new FloatObject(Value - integerObject.Value)),
                    ArithmeticOperator.Multiply => new Completion(new FloatObject(Value * integerObject.Value)),
                    ArithmeticOperator.Divide => new Completion(new FloatObject(Value / integerObject.Value)),
                    ArithmeticOperator.Modulo => new Completion(new FloatObject(Value % integerObject.Value))
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

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}