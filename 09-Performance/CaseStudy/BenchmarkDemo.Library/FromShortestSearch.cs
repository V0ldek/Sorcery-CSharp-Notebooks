namespace BenchmarkDemo.Library;

public static class FromShortestSearch
{
    public static string FindLongestSubstring(string needle, string haystack)
    {
        ArgumentNullException.ThrowIfNull(needle);

        return FindLongestSubstring(needle, 1, haystack) ?? "";
    }

    public static string? FindLongestSubstring(string needle, int length, string haystack)
    {
        ArgumentNullException.ThrowIfNull(needle);

        for (var start = 0; start + length <= needle.Length; start += 1)
        {
            var substring = needle.Substring(start, length);

            if (haystack.Contains(substring))
            {
                var tryLarger = FindLongestSubstring(needle, length + 1, haystack);

                return tryLarger ?? substring;
            }
        }

        return null;
    }
}
