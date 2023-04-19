using System;
using System.Linq;
using System.Reflection;
using SandScript.Interpreter.Native;
using SandScript.Interpreter.Runtime;
using TypeInfo = SandScript.Interpreter.Native.TypeInfo;

namespace SandScript.Interpreter.Interop
{
    public static class ClrExtensions
    {
        public static RuntimeObject CastToRuntimeObject(this object value)
        {
            if (value == null)
                return RuntimeObject.Null;
            
            return value switch
            {
                int intValue => new IntegerObject(intValue),
                float floatValue => new FloatObject(floatValue),
                double doubleValue => new FloatObject(doubleValue),
                string stringValue => new StringObject(),
                _ => new ClrObject(value)
            };
        }

        public static TypeInfo InferClrTypeInfo(this FieldInfo fieldInfo)
        {
            return TypeInfo.Error;
        }

        public static object CastToClr(this RuntimeObject value)
        {
            return value switch
            {
                IntegerObject intObj => intObj.Value,
                FloatObject floatObj => floatObj.Value,
                _ => throw new RuntimeException($"Does not support casting {value.GetType()} to Clr")
            };
        }

        public static object[] CastToSingles(this object[] args)
        {
            return args.Select(ConvertToSingle).ToArray();

        }

        private static object ConvertToSingle(object o)
        {
            try
            {
                return Convert.ToSingle(o);
            }
            catch (Exception ex)
            {
                return o;
            }
        } 
    }
}