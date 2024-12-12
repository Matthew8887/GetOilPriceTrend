using System.Diagnostics.CodeAnalysis;
using System.Text.Json;

namespace Exercise.Helper
{
    [ExcludeFromCodeCoverage]
    public static class JsonHelper
    {
        public static string SerializeObject(this object obj)
        {
            return JsonSerializer.Serialize(obj);
        }

        public static T? DeserializeObject<T>(this string? value)
        {
            return value == null ? default : JsonSerializer.Deserialize<T>(value);
        }
    }
}
