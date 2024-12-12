using System.Text.Json.Serialization;

namespace Exercise.Model
{
    public class GeneralRequest
    {
        [JsonPropertyName("id")]
        public int? Id { get; set; }

        [JsonPropertyName("jsonrpc")]
        public string? JsonRpc { get; set; }

        [JsonPropertyName("method")]
        public string? Method { get; set; }

        [JsonPropertyName("params")]
        public object? Params { get; set; }
    }
}
