using System.Text.Json.Serialization;

namespace Exercise.Model
{
    public class OilPrice
    {
        [JsonPropertyName("Date")]
        public DateTime? Date { get; set; }

        [JsonPropertyName("Price")]
        public double Price { get; set; }
    }
}
