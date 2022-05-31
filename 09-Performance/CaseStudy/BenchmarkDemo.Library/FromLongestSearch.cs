namespace BenchmarkDemo.Library;

public static class FromLongestSearch
{
    public static string FindLongestSubstring(string needle, string haystack)
    {
        ArgumentNullException.ThrowIfNull(needle);

        for (var length = needle.Length; length > 0; length -= 1)
        {
            for (var start = 0; start + length <= needle.Length; start += 1)
            {
                var substring = needle.Substring(start, length);

                if (haystack.Contains(substring))
                {
                    return substring;
                }
            }
        }

        return "";
    }
}
