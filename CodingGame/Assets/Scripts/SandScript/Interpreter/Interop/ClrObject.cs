using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using SandScript.Interpreter.Runtime;
using UnityEngine;
using TypeInfo = SandScript.Interpreter.Native.TypeInfo;

namespace SandScript.Interpreter.Interop
{
    public class ClrObject : RuntimeObject
    {
        private readonly object _o;
        private readonly ClrBindConfig _clrBindConfig;
        private readonly List<string> monoMethodNames = new (typeof(MonoBehaviour).GetMethods().Select(e => e.Name));
        public override TypeInfo TypeInfo { get; } = TypeInfo.Clr;

        public ClrObject(object o)
        {
            _o = o;
            
            BindFields(o);
            BindMethods(o);
        }

        public ClrObject(object o, ClrBindConfig clrBindConfig)
        {
            _clrBindConfig = clrBindConfig;
            _o = o;
            
            BindFields(o);
            BindMethods(o);
        }

        public void BindFields(object o)
        {
            var fieldsInfo = o.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance);
            
            if (_clrBindConfig != null)
            {
                foreach (var fieldInfo in fieldsInfo)
                {
                    if (!_clrBindConfig.FieldNames.Contains(fieldInfo.Name))
                        continue;
                    BindField(new ClrField(fieldInfo.Name, o));
                }
            }
            else
            {
                foreach (var field in fieldsInfo)
                {
                    BindField(new ClrField(field.Name, o));
                }
            }
        }

        public void BindMethods(object o)
        {
            var methodInfos = o.GetType() .GetMethods(BindingFlags.Public | BindingFlags.Instance);

            if (_clrBindConfig != null)
            {
                foreach (var methodInfo in methodInfos)
                {
                    if (!_clrBindConfig.MethodNames.Contains(methodInfo.Name))
                        continue;
                    BindMethod(new ClrMethod(o, methodInfo));
                }
            }
            else
            {
                foreach(var methodInfo in methodInfos)
                {
                    if (monoMethodNames.Contains(methodInfo.Name))
                        continue;
                    BindMethod(new ClrMethod(o, methodInfo));
                }
            }
        }
    }
}