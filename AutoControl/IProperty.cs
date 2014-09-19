using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AutoControlLib
{
    delegate void PropertyEditEvent(IProperty property);
    interface IProperty
    {
        event PropertyEditEvent PropertyEdited;

        bool Dirty { get; }
        string Name { get; }

        Control CreateEditor();
        void Commit();
        void Reset();
    }

    static class PropertyExtensions
    {
        public static IProperty GetProperty<T>(this T me, string displayName)
        {
            var prop = me.GetType().GetProperties()
                .Select(p => new { Prop = p, Attr = p.GetDisplayNameAttribute() })
                .FirstOrDefault(item => item.Attr.DisplayName.Equals(displayName)).Prop;
            Type propType = prop.PropertyType;
            if (propType == typeof(string))
            {
                return new StringProperty(me, displayName);
            }
            if (propType == typeof(int))
            {
                return new IntegerProperty(me, displayName);
            }
            if (propType == typeof(bool))
            {
                return new BooleanProperty(me, displayName);
            }
            if (propType.IsEnum)
            {
                return new EnumProperty(me, propType, displayName);
            }

            throw new NotSupportedException("Property type not supported.");
        }

        public static IProperty[] GetProperties<T>(this T me)
        {
            return me.GetDisplayNames().Select(name => me.GetProperty(name)).ToArray();
        }
    }
}
