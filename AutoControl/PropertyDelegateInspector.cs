using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace AutoControlLib
{
    delegate T PropertyGetter<T>();
    delegate void PropertySetter<T>(T val);
    class PropertyDelegateInspector<T>
    {
        public static void GetDelegates(object obj, string displayName, out PropertyGetter<T> getter, out PropertySetter<T> setter)
        {
            var inspector = new PropertyDelegateInspector<T>(obj, displayName);
            getter = inspector.Getter;
            setter = inspector.Setter;
        }

        public PropertyDelegateInspector(object obj, string displayName)
        {
            Type objType = obj.GetType();
            PropertyInfo prop = objType.GetProperties()
                .Select(pi => new { Prop = pi, Attr = pi.GetDisplayNameAttribute() })
                .FirstOrDefault(item => item.Attr.DisplayName.Equals(displayName)).Prop;
            MethodInfo getMethod = prop.GetGetMethod();
            MethodInfo setMethod = prop.GetSetMethod();
            if (prop.PropertyType.IsEnum)
            {
                Getter = () => (T)getMethod.Invoke(obj, new object[0]);
                Setter = t => { setMethod.Invoke(obj, new object[] { t }); };
            }
            else
            {
                Getter = (PropertyGetter<T>)Delegate.CreateDelegate(typeof(PropertyGetter<T>), obj, getMethod);
                Setter = (PropertySetter<T>)Delegate.CreateDelegate(typeof(PropertySetter<T>), obj, setMethod);
            }
        }

        public PropertyGetter<T> Getter { get; private set; }
        public PropertySetter<T> Setter { get; private set; }
    }
}
