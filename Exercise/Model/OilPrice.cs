using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace Exercise.Model
{
    [ExcludeFromCodeCoverage]
    public class OilPrice
    {
        [JsonPropertyName("Date")]
        public DateTime? Date { get; set; }

        [JsonPropertyName("Price")]
        public double Price { get; set; }
    }
}
