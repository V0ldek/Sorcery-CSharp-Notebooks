using System.Text;
using BenchmarkDemo.Library;

namespace BenchmarkDemo.Tests;

public class FindSubstringTests
{
    //cSpell:disable
    public static TheoryData<string, string, string> FindSubstringData
    {
        get
        {
            var data = new TheoryData<string, string, string>()
            {
                { "", "abcd", "" },
                { "a", "bcde", "" },
                { "a", "bcdea", "a" },
                { "ala", "ma kota ala", "ala" },
                { "alan", "ma kota ala", "ala" },
                { "alakot", "ma kota ala", "ala" },
                { "cumulative sum", "lorem ipsum dolor sit amet", "sum" },
                { "aaaaaaaaaaaa", "a aa aaa aaaa aaaaa aaaaaa aaaaaaa", "aaaaaaa" },
                { "aaaaaaaaaa", "abaabaaabaaaabaaaaabaaaaaab", "aaaaaa"}
            };

            var (needle, haystack, expected) = BenchmarkGenerator(5, 100);
            data.Add(needle, haystack, expected);
            (needle, haystack, expected) = BenchmarkGenerator(50, 100);
            data.Add(needle, haystack, expected);
            (needle, haystack, expected) = BenchmarkGenerator(5, 1_000);
            data.Add(needle, haystack, expected);
            (needle, haystack, expected) = BenchmarkGenerator(50, 1_000);
            data.Add(needle, haystack, expected);

            return data;
        }
    }
    //cSpell:enable

    private static (string needle, string haystack, string expected) BenchmarkGenerator(
        int needleLength,
        int haystackLength)
    {

        var haystackBuilder = new StringBuilder(haystackLength);
        var currentLength = 1;
        var longest = 1;

        while (haystackBuilder.Length < haystackLength)
        {
            haystackBuilder.Append(string.Concat(Enumerable.Repeat('a', currentLength)));
            haystackBuilder.Append('b');

            currentLength = Math.Min(currentLength + 1, haystackLength - haystackBuilder.Length - 1);
            longest = Math.Max(longest, currentLength);
        }

        var needle = string.Concat(Enumerable.Repeat('a', needleLength));
        var haystack = haystackBuilder.ToString();
        var expected = string.Concat(Enumerable.Repeat('a', Math.Min(needleLength, longest)));

        return (needle, haystack, expected);
    }

    [Theory]
    [MemberData(nameof(FindSubstringData))]
    public void SimpleSearchCorrectness(string needle, string haystack, string expected)
    {
        var actual = FromLongestSearch.FindLongestSubstring(needle, haystack);

        Assert.Equal(expected, actual);
    }

    [Theory]
    [MemberData(nameof(FindSubstringData))]
    public void FromShortestSearchCorrectness(string needle, string haystack, string expected)
    {
        var actual = FromShortestSearch.FindLongestSubstring(needle, haystack);

        Assert.Equal(expected, actual);
    }

    [Theory]
    [MemberData(nameof(FindSubstringData))]
    public void FromShortestWithEliminationSearchCorrectness(string needle, string haystack, string expected)
    {
        var actual = FromShortestWithEliminationSearch.FindLongestSubstring(needle, haystack);

        Assert.Equal(expected, actual);
    }
}