using System.Text.Json;
using System.Text.Json.Serialization;
using NodaTime.Serialization.SystemTextJson;

namespace GalacticAnnouncements.API;

public static class Json
{
    public static readonly JsonSerializerOptions SerializerOptions = SetSerializerSettings(new JsonSerializerOptions());

    public static JsonSerializerOptions SetSerializerSettings(JsonSerializerOptions settings)
    {
        settings.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        settings.Converters.Add(new JsonStringEnumConverter());
        settings.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        settings.ConfigureForNodaTime(DateTimeZoneProviders.Tzdb);
        return settings;
    }
}
