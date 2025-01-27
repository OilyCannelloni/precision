using System.Text.Json;
using System.Text.Json.Serialization;
using Precision.models;

namespace Precision.algorithm.json;

public class HoldingSerializer : JsonConverter<Holding>
{
    public override Holding? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return new Holding(reader.GetString() ?? throw new ArgumentNullException(nameof(reader)));
    }

    public override void Write(Utf8JsonWriter writer, Holding value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }
}