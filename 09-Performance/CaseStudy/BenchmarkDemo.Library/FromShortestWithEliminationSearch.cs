namespace BenchmarkDemo.Library;

public static class FromShortestWithEliminationSearch
{
    public static string FindLongestSubstring(string needle, string haystack)
    {
        ArgumentNullException.ThrowIfNull(needle);
        ArgumentNullException.ThrowIfNull(haystack);

        return FindLongestSubstring(needle, 1, 0, haystack) ?? "";
    }

    private static string? FindLongestSubstring(string needle, int length, int startFrom, string haystack)
    {
        for (var start = startFrom; start + length <= needle.Length; start += 1)
        {
            var substring = needle.Substring(start, length);

            if (haystack.Contains(substring, StringComparison.Ordinal))
            {
                var tryLarger = FindLongestSubstring(needle, length + 1, start, haystack);

                return tryLarger ?? substring;
            }
        }

        return null;
    }

    public static string SpanBasedFindLongestSubstring(string needle, string haystack)
    {
        ArgumentNullException.ThrowIfNull(needle);
        ArgumentNullException.ThrowIfNull(haystack);

        return SpanBasedFindLongestSubstring(needle, 1, 0, haystack) ?? "";
    }

    private static string? SpanBasedFindLongestSubstring(ReadOnlySpan<char> needle, int length, int startFrom, ReadOnlySpan<char> haystack)
    {
        for (var start = startFrom; start + length <= needle.Length; start += 1)
        {
            ReadOnlySpan<char> substring = needle.Slice(start, length);

            if (haystack.Contains(substring, StringComparison.Ordinal))
            {
                var tryLarger = SpanBasedFindLongestSubstring(needle, length + 1, start, haystack);

                return tryLarger ?? new string(substring);
            }
        }

        return null;
    }
}
