using System.Text.Json;
using System.Text.Json.Serialization;

namespace AdventOfCode.Application.Attributes;
public class TypedAttributesJsonConverter : JsonConverter<TypedAttributes>
{
    public override TypedAttributes Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options)
    {
        var attributesDictionary = JsonSerializer.Deserialize<Dictionary<string, object>>(
            ref reader,
            options);

        return new TypedAttributes(attributesDictionary);
    }

    public override void Write(
        Utf8JsonWriter writer,
        TypedAttributes value,
        JsonSerializerOptions options)
    {
        var attributes = value?.Attributes;
        if (attributes?.Count == 0)
        {
            attributes = null;
        }

        JsonSerializer.Serialize(
            writer,
            attributes,
            options);
    }
}
