using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace Exercise.Model
{
    [ExcludeFromCodeCoverage]
    public class ExceptionResponse
    {
        public ExceptionResponse(string message)
        {
            ErrorMessage = message;
        }

        [JsonPropertyName("errorMessage")]
        public string ErrorMessage { get; set; }
    }
}
