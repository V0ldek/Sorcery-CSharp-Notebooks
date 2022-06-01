namespace BenchmarkDemo.Library;

public static class FromLongestSearch
{
    public static string FindLongestSubstring(string needle, string haystack)
    {
        ArgumentNullException.ThrowIfNull(needle);
        ArgumentNullException.ThrowIfNull(haystack);

        for (var length = needle.Length; length > 0; length -= 1)
        {
            for (var start = 0; start + length <= needle.Length; start += 1)
            {
                var substring = needle.Substring(start, length);

                if (haystack.Contains(substring, StringComparison.Ordinal))
                {
                    return substring;
                }
            }
        }

        return "";
    }

    public static string SpanBasedFindLongestSubstring(string needle, string haystack)
    {
        ArgumentNullException.ThrowIfNull(needle);
        ArgumentNullException.ThrowIfNull(haystack);

        for (var length = needle.Length; length > 0; length -= 1)
        {
            for (var start = 0; start + length <= needle.Length; start += 1)
            {
                ReadOnlySpan<char> substring = needle.AsSpan().Slice(start, length);

                if (haystack.AsSpan().Contains(substring, StringComparison.Ordinal))
                {
                    return new string(substring);
                }
            }
        }

        return "";
    }
}