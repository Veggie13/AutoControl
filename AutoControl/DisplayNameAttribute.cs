using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace AutoControlLib
{
    [AttributeUsage(AttributeTargets.Property)]
    public class DisplayNameAttribute : Attribute
    {
        public DisplayNameAttribute(string displayName)
        {
            DisplayName = displayName;
        }

        public string DisplayName
        {
            get;
            private set;
        }
    }

    static class DisplayNameExtensions
    {
        public static DisplayNameAttribute GetDisplayNameAttribute(this PropertyInfo prop)
        {
            return (DisplayNameAttribute)prop.GetCustomAttributes(typeof(DisplayNameAttribute), true).FirstOrDefault();
        }

        public static string[] GetDisplayNames<T>(this T me)
        {
            return me.GetType().GetProperties()
                .Select(p => p.GetDisplayNameAttribute())
                .Select(a => a.DisplayName).ToArray();
        }
    }
}
