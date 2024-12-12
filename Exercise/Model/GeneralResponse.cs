using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace Exercise.Model
{
    [ExcludeFromCodeCoverage]
    public class GeneralResponse
    {
        public GeneralResponse(GeneralRequest request)
        {
            JsonRpc = request.JsonRpc ?? throw new ArgumentNullException();
            Id = request.Id ?? throw new ArgumentNullException();
        }

        [JsonPropertyName("jsonrpc")]
        public string JsonRpc { get; set; }

        [JsonPropertyName("id")]
        public int? Id { get; set; }
    }
}
