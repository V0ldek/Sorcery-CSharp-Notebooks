namespace BenchmarkDemo.Library;

public static class FromShortestWithEliminationSearch
{
    public static string FindLongestSubstring(string needle, string haystack)
    {
        ArgumentNullException.ThrowIfNull(needle);

        return FindLongestSubstring(needle, 1, 0, haystack) ?? "";
    }

    public static string? FindLongestSubstring(string needle, int length, int startFrom, string haystack)
    {
        ArgumentNullException.ThrowIfNull(needle);

        for (var start = startFrom; start + length <= needle.Length; start += 1)
        {
            var substring = needle.Substring(start, length);

            if (haystack.Contains(substring))
            {
                var tryLarger = FindLongestSubstring(needle, length + 1, start, haystack);

                return tryLarger ?? substring;
            }
        }

        return null;
    }
}
