using System.Diagnostics.CodeAnalysis;

namespace Exercise.Helper
{
    [ExcludeFromCodeCoverage]
    public static class StringHelper
    {
        public static bool IsNullOrEmptyOrWhiteSpace(this string? value)
        {
            return string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value);
        }

        public static bool IsNull(this int? value)
        {
            return value == null;
        }

        public static bool IsNull(this object? value)
        {
            return value == null;
        }

        public static string ThrowExIfNullOrEmpty(this string? value, string? customErrorMessage = null,
            [System.Runtime.CompilerServices.CallerArgumentExpression("value")] string source = "")
        {
            if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
                throw new Exception(customErrorMessage ?? $"{source} is null, empty or whiteSpace");

            return value;
        }
    }
}
