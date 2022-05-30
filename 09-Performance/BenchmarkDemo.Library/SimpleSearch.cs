namespace BenchmarkDemo.Library;

public static class SimpleSearch
{
    public static string FindLongestSubstring(string needle, string haystack)
    {
        ArgumentNullException.ThrowIfNull(needle);

        if (needle == "")
        {
            return "";
        }

        if (haystack.Contains(needle))
        {
            return needle;
        }

        var substring1 = FindLongestSubstring(needle[1..], haystack);
        var substring2 = FindLongestSubstring(needle[..^1], haystack);

        return substring1.Length >= substring2.Length ? substring1 : substring2;
    }
}
