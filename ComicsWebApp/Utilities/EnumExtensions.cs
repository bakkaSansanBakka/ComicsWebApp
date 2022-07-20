﻿using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace ComicsWebApp.Utilities
{
    public static class EnumExtensions
    {
        public static string GetDisplayName(this Enum enumValue)
        {
            FieldInfo fi = enumValue.GetType().GetField(enumValue.ToString());
            var displayAttribute = fi.CustomAttributes.FirstOrDefault(c => c.AttributeType == typeof(DisplayAttribute));
            if (displayAttribute == null) return enumValue.ToString();
            return displayAttribute.NamedArguments.FirstOrDefault(a => a.MemberName == "Name").TypedValue.Value.ToString();
        }
    }
}
