using BenchmarkDemo.Library;

namespace BenchmarkDemo.Tests;

public class FindSubstringTests
{
    public static TheoryData<string, string, string> FindSubstringData => new()
    {
        { "", "abcd", "" },
        { "a", "bcde", "" },
        { "a", "bcdea", "a" },
        { "ala", "ma kota ala", "ala" },
        { "alan", "ma kota ala", "ala" },
        { "cumulative sum", "lorem ipsum dolor sit amet", "sum" },
        { "aaaaaaaaaaaa", "a aa aaa aaaa aaaaa aaaaaa aaaaaaa", "aaaaaaa" }
    };

    [Theory]
    [MemberData(nameof(FindSubstringData))]
    public void SimpleSearchCorrectness(string needle, string haystack, string expected)
    {
        var actual = SimpleSearch.FindLongestSubstring(needle, haystack);

        Assert.Equal(expected, actual);
    }
}