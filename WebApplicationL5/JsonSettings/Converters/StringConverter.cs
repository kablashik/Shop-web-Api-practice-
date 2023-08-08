using System.Text.Json;
using System.Text.Json.Serialization;

namespace WebApplicationL5.JsonSettings.Converters;

public class StringConverter : JsonConverter<string>
{
    public override string? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var value = reader.GetString();

        if (value == "хуй")
        {
            return "цензура";
        }

        return value;
    }

    public override void Write(Utf8JsonWriter writer, string value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value);
    }
}