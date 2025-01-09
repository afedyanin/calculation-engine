using System.Text.Json;

namespace CalculationEngine.Core.Helpers;

internal static class SerializationHelper
{
    public static object? Deserialize(string json, string typeString, JsonSerializerOptions? jsonSerializerOptions = null)
    {
        var type = Type.GetType(typeString);
        var res = JsonSerializer.Deserialize(json, type!, jsonSerializerOptions);
        return res;
    }

    public static (string, string) Serialize(object obj, JsonSerializerOptions? jsonSerializerOptions = null)
    {
        var objType = obj.GetType();
        var typeString = $"{objType.FullName}, {objType.Assembly.GetName().Name}";
        var json = JsonSerializer.Serialize(obj, objType, jsonSerializerOptions);

        return (json, typeString);
    }
}
