namespace KDrom.Utilities.Extensions;

public static class StringExtensions
{
    public static string Clean(this string value)
    {
        return value.Trim().Replace(" ", string.Empty).ToLower();
    }
}