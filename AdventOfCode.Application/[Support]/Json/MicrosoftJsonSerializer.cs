using AdventOfCode.Application.Attributes;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace AdventOfCode.Application.Json;

public class MicrosoftJsonSerializer : IJsonSerializer
{
    private static readonly JsonSerializerOptions DeserializationOptions = new JsonSerializerOptions
    {
        PropertyNameCaseInsensitive = true,
        Converters = { new TypedAttributesJsonConverter() },
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault
    };

    protected virtual bool WriteIndented { get; } = false;

    string IJsonSerializer.ToJsonString(
        object value,
        bool useCamelCaseNamingStrategy,
        bool? writeIndented)
    {
        return ToJsonString(
            value,
            useCamelCaseNamingStrategy,
            writeIndented.GetValueOrDefault(WriteIndented));
    }

    public static string ToJsonString(
        object value,
        bool useCamelCaseNamingStrategy = true,
        bool indented = true)
    {
        var options = new JsonSerializerOptions
        {
            WriteIndented = indented,
            PropertyNamingPolicy = useCamelCaseNamingStrategy ? JsonNamingPolicy.CamelCase : null,
            Converters = { new TypedAttributesJsonConverter() }
        };

        return ToJsonString(value, options);
    }

    public static string ToJsonString(
        object value,
        JsonSerializerOptions options)
    {
        return JsonSerializer.Serialize(value, options);
    }

    T IJsonSerializer.FromJsonString<T>(string jsonString)
    {
        return FromJsonString<T>(jsonString);
    }

    object IJsonSerializer.FromJsonString(string jsonString, Type type)
    {
        return FromJsonString(jsonString, type, DeserializationOptions);
    }

    public static T FromJsonString<T>(string jsonString)
    {
        return (T)FromJsonString(jsonString, typeof(T), DeserializationOptions);
    }

    public static object FromJsonString(string jsonString, Type type, JsonSerializerOptions options)
    {
        return JsonSerializer.Deserialize(
            jsonString,
            type,
            options);
    }
}


