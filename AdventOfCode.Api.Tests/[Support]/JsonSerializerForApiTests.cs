using AdventOfCode.Application.Attributes;
using AdventOfCode.Application.Extensions;
using AdventOfCode.Application.Json;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Unicode;

namespace AdventOfCode.Api.Tests;

public class JsonSerializerForApiTests : IJsonSerializer
{
    private static readonly JsonSerializerOptions DeserializationOptions = new JsonSerializerOptions
    {

        PropertyNameCaseInsensitive = true,
        Converters = { new TypedAttributesJsonConverter() }
    };

    private static readonly IList<JsonConverter> JsonConverters = new List<JsonConverter>
        {
            new TypedAttributesJsonConverter()
        };

    string IJsonSerializer.ToJsonString(
        object value,
        bool useCamelCaseNamingStrategy,
        bool? writeIndented)
    {
        return ToJsonString(
            value,
            useCamelCaseNamingStrategy);
    }

    public static string ToJsonString(
        object value,
        bool useCamelCaseNamingStrategy = true)
    {
        var options = new JsonSerializerOptions
        {
            WriteIndented = true,
            Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.All),
            PropertyNamingPolicy = useCamelCaseNamingStrategy ? JsonNamingPolicy.CamelCase : null
        };
        JsonConverters.ForEach(converter => options.Converters.Add(converter));

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
        return FromJsonString(
            jsonString,
            type,
            DeserializationOptions);
    }

    public static T FromJsonString<T>(string jsonString)
    {
        return (T)FromJsonString(
            jsonString,
            typeof(T),
            DeserializationOptions);
    }

    public static object FromJsonString(
        string jsonString,
        Type type,
        JsonSerializerOptions options)
    {
        return JsonSerializer.Deserialize(
            jsonString,
            type,
            options);
    }

}
