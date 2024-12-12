using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace Exercise.Model
{
    [ExcludeFromCodeCoverage]
    public class GetOilPriceTrendResponse : GeneralResponse
    {
        public GetOilPriceTrendResponse(GeneralRequest request) : base(request)
        {
        }

        public Result Result { get; set; }
    }

    public class Result
    {
        [JsonPropertyName("prices")]
        public List<PriceObj> Prices { get; set; }
    }

    public class PriceObj
    {
        public PriceObj(){}

        public PriceObj(DateTime date, double price)
        {
            DateISO8601 = date.ToString("yyyy-MM-dd");
            Price = price;
        }

        [JsonPropertyName("dateISO8601")]
        public string DateISO8601 { get; set; }

        [JsonPropertyName("price")]
        public double Price { get; set; }
    }
}
