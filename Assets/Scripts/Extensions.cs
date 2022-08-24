using System;
using System.ComponentModel;

public static class Extensions
{
    public static string GetDescription(this Enum val)
    {
        DescriptionAttribute[] attributes = (DescriptionAttribute[])val
           .GetType()
           .GetField(val.ToString())
           .GetCustomAttributes(typeof(DescriptionAttribute), false);
        return attributes.Length > 0 ? attributes[0].Description : string.Empty;
    }

    public static T Increment<T>(this T src, int index) where T : Enum
    {
        T[] arr = (T[])Enum.GetValues(src.GetType());
        int i = (Array.IndexOf(arr, src) + index) % arr.Length;
        return i >= 0 ? arr[i] : arr[arr.Length + i];
    }
}