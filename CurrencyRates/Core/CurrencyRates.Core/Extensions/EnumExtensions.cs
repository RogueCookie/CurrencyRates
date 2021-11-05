using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using CurrencyRates.Core.Attribute;

namespace CurrencyRates.Core.Extensions
{
    public static class EnumExtensions
    {
        public static string GetEnumDescription(this Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes = fi.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];

            if (attributes != null && attributes.Any())
            {
                return attributes.First().Description;
            }

            return value.ToString();
        }

        public static string GetFullName(this Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            var attributes = fi.GetCustomAttributes(typeof(FullNameCurrency), false) as FullNameCurrency[];

            if (attributes != null && attributes.Any())
            {
                return attributes.First().FullName;
            }

            return value.ToString();
        }
    }
}