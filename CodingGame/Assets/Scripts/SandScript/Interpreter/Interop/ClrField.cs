using SandScript.Interpreter.Native;
using SandScript.Interpreter.Runtime;

namespace SandScript.Interpreter.Interop
{
    public class ClrField : RuntimeField
    {
        public override string FieldName { get; }
        public override TypeInfo TypeInfo { get; }
        
        private readonly object _parent;

        public ClrField(string fieldName, object parent)
        {
            FieldName = fieldName;
            _parent = parent;

            TypeInfo = _parent.GetType().GetField(FieldName).InferClrTypeInfo();
        }

        public override RuntimeObject GetValue()
        {
            var fieldValue = _parent.GetType().GetField(FieldName).GetValue(_parent);
            return fieldValue.CastToRuntimeObject();
        }

        public override void SetValue(RuntimeObject value)
        {
            _parent.GetType().GetField(FieldName).SetValue(value.CastToClr(), _parent);
        }
    }
}