using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using Waterschapshuis.CatchRegistration.Core.Utils;

namespace Waterschapshuis.CatchRegistration.Core
{
    public static class EnumExtensions
    {
        public static string GetDisplayName(this Enum value)
        {
            var enumType = value.GetType();

            var memberInfo = enumType.GetMember(value.ToString());
            Attribute? attribute = memberInfo
                .First()
                .GetCustomAttribute(typeof(DisplayAttribute));

            return attribute is DisplayAttribute attr ? attr.Name : String.Empty;
        }

        public static string GetDescription<TEnum>(this TEnum o)
        {
            return o.GetAttribute<TEnum, DescriptionAttribute>().Description;
        }

        public static string GetCode<TEnum>(this TEnum o)
        {
            return o.FindAttribute<TEnum, CodeAndDescriptionAttribute>()?.Code ?? String.Empty;
        }

        public static string? FindCode<TEnum>(this TEnum o)
        {
            return o.FindAttribute<TEnum, CodeAndDescriptionAttribute>()?.Code;
        }

        public static TDescriptionAttribute GetAttribute<TEnum, TDescriptionAttribute>(this TEnum o)
            where TDescriptionAttribute : DescriptionAttribute
        {
            TDescriptionAttribute? result = FindAttribute<TEnum, TDescriptionAttribute>(o);
            Type attributeType = typeof(TDescriptionAttribute);
            if (result == null)
            {
                throw new InvalidOperationException($"Attribute of type {attributeType} was not found");
            }

            return result;
        }

        private static TDescriptionAttribute? FindAttribute<TEnum, TDescriptionAttribute>(this TEnum o)
            where TDescriptionAttribute : DescriptionAttribute
        {
            Type enumType = o!.GetType();
            FieldInfo? field = enumType.GetField(o.ToString() ?? String.Empty);
            Type attributeType = typeof(TDescriptionAttribute);
            object[] attributes = field != null ? field.GetCustomAttributes(attributeType, false) : new object[0];
            if (attributes.Length == 0)
            {
                return null;
            }

            return (TDescriptionAttribute)attributes[0];
        }
    }
}
