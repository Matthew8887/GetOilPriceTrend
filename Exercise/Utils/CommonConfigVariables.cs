using System.Diagnostics.CodeAnalysis;
using Exercise.Model;

namespace Exercise.Utils
{
    [ExcludeFromCodeCoverage]
    public static class CommonConfigVariables
    {
        public const string Url = nameof(Url);
        public static List<OilPrice> ListOilPrices;
    }
}
