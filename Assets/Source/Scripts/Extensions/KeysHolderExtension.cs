using System;
using System.ComponentModel;

namespace Source.Scripts.Extensions
{
    // public static class KeysHolderExtension
    // {
    //     public static string GetDescription(this WeaponKeyHolder value)
    //     {
    //         var field = value.GetType().GetField(value.ToString());
    //         var attribute = (DescriptionAttribute)Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute));
    //         return attribute.Description;
    //     }
    // }
    public static class KeysHolderExtension
    {
        public static string GetDescription<T>(this T value) where T : Enum
        {
            var field = value.GetType().GetField(value.ToString());
            var attribute = (DescriptionAttribute)Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute));
            return attribute.Description;
        }
    }
}