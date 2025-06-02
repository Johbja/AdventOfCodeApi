using System.Text.Json;

namespace AdventOfCode.Application.Extensions;
public static class ObjectExtensions
{
    public static int ToIntOrDefault(this object obj, int defaultValue = default)
    {
        return obj switch
        {
            null => defaultValue,
            int typedValue => typedValue,
            JsonElement jsonElement => jsonElement.GetIntOrDefault(defaultValue),
            _ => defaultValue
        };
    }
    public static long ToLongOrDefault(this object obj, long defaultValue = default)
    {
        return obj switch
        {
            null => defaultValue,
            int intValue => intValue,
            long typedValue => typedValue,
            JsonElement jsonElement => jsonElement.GetLongOrDefault(defaultValue),
            _ => defaultValue
        };
    }

    public static string ToStringOrDefault(this object obj, string defaultValue = default)
    {
        return obj switch
        {
            null => defaultValue,
            string typedValue => typedValue,
            JsonElement jsonElement => jsonElement.GetStringOrDefault(defaultValue),
            _ => defaultValue
        };
    }

    public static decimal ToDecimalOrDefault(this object obj, decimal defaultValue = default)
    {
        return obj switch
        {
            null => defaultValue,
            int intValue => intValue,
            long longValue => longValue,
            decimal typedValue => typedValue,
            JsonElement jsonElement => jsonElement.GetDecimalOrDefault(defaultValue),
            _ => defaultValue
        };
    }

    public static bool ToBoolOrDefault(this object obj, bool defaultValue = default)
    {
        return obj switch
        {
            null => defaultValue,
            bool typedValue => typedValue,
            JsonElement jsonElement => jsonElement.GetBoolOrDefault(defaultValue),
            _ => defaultValue
        };
    }

    public static DateTime ToDateTimeOrDefault(this object obj, DateTime defaultValue = default)
    {
        return obj switch
        {
            null => defaultValue,
            DateTime typedValue => typedValue,
            JsonElement jsonElement => jsonElement.GetDateTimeOrDefault(defaultValue),
            _ => defaultValue
        };
    }

}
