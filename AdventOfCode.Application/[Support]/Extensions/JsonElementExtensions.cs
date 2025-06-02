using System.Text.Json;

namespace AdventOfCode.Application.Extensions;
public static class JsonElementExtensions
{
    public static int GetIntOrDefault(this JsonElement jsonElement, int defaultValue = default)
    {
        if (jsonElement.ValueKind == JsonValueKind.Number)
            return jsonElement.GetInt32();

        return defaultValue;
    }

    public static long GetLongOrDefault(this JsonElement jsonElement, long defaultValue = default)
    {
        if (jsonElement.ValueKind == JsonValueKind.Number)
            return jsonElement.GetInt64();

        return defaultValue;
    }

    public static decimal GetDecimalOrDefault(this JsonElement jsonElement, decimal defaultValue = default)
    {
        if (jsonElement.ValueKind == JsonValueKind.Number)
            return jsonElement.GetDecimal();

        return defaultValue;
    }

    public static bool GetBoolOrDefault(this JsonElement jsonElement, bool defaultValue = default)
    {
        if (jsonElement.ValueKind == JsonValueKind.True || jsonElement.ValueKind == JsonValueKind.False)
            return jsonElement.GetBoolean();

        return defaultValue;
    }

    public static DateTime GetDateTimeOrDefault(this JsonElement jsonElement, DateTime defaultValue = default)
    {
        try
        {
            return jsonElement.GetDateTime();
        }
        catch
        {
            return defaultValue;
        }
    }

    public static string GetStringOrDefault(this JsonElement jsonElement, string defaultValue = default)
    {
        if (jsonElement.ValueKind == JsonValueKind.String)
            return jsonElement.GetString();

        return defaultValue;
    }
}
