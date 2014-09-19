using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace AutoControlLib
{
    static class AttributeExtensions
    {
        public static T GetClassAttribute<T>(this object obj) where T : Attribute
        {
            return (T)obj.GetType().GetCustomAttributes(typeof(T), true).FirstOrDefault();
        }

        public static T GetMemberAttribute<T>(this MemberInfo info) where T : Attribute
        {
            return (T)info.GetCustomAttributes(typeof(T), true).FirstOrDefault();
        }
    }
}
