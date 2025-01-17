using System.Text.Json;
using System.Text.Json.Serialization;
using Precision.models;

namespace Precision.algorithm.json;

public class CardJsonConverter : JsonConverter<Card>
{
    public override Card? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return new Card(reader.GetString() ?? throw new JsonException("Invalid Card JSON"));
    }

    public override void Write(Utf8JsonWriter writer, Card value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }
}