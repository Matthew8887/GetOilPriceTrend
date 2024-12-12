using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace Exercise.Model
{
    [ExcludeFromCodeCoverage]
    public class GetOilPriceTrendRequest : GeneralRequest
    {
        [JsonPropertyName("startDateISO8601")]
        public DateTime? StartDateISO8601 { get; set; }

        [JsonPropertyName("endDateISO8601")]
        public DateTime? EndDateISO8601 { get; set; }
    }
}
