namespace BackendDeveloperProject.Core.Extensions
{
    public static class StringExtensions
    {
        public static string IsNullOrEmpty(this string? value, string defaultValue = "")
        {
            return string.IsNullOrEmpty(value) ? defaultValue : value;
        }
    }
}
