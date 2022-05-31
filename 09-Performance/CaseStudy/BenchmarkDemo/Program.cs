using System.Text;
using System.Text.RegularExpressions;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
using Perfolizer.Mathematics.SignificanceTesting;
using Perfolizer.Mathematics.Thresholds;

BenchmarkRunner.Run<Benches>();

[SimpleJob(RuntimeMoniker.Net60)]
[HtmlExporter]
[MarkdownExporter]
[RPlotExporter]
[MemoryDiagnoser]
[StatisticalTestColumn(StatisticalTestKind.MannWhitney, ThresholdUnit.Ratio, 0.05, true)]
public class Benches
{
    [Params(RemoveMode.All, RemoveMode.AllLong, RemoveMode.OnlyLongest, RemoveMode.None)]
    public RemoveMode RemoveMode { get; set; }

    public string Haystack { get; private set; } = null!;

    public static string Needle => "interrelationships";

    [Benchmark(Baseline = true)]
    public string FromLongestSearch() =>
        BenchmarkDemo.Library.FromLongestSearch.FindLongestSubstring(Needle, Haystack);

    [Benchmark]
    public string FromShortestSearch() =>
        BenchmarkDemo.Library.FromShortestSearch.FindLongestSubstring(Needle, Haystack);

    [GlobalSetup]
    public async Task SetupAsync()
    {
        Haystack = await File.ReadAllTextAsync("./data/words.txt");

        Haystack = RemoveMode switch
        {
            RemoveMode.None => Haystack,
            RemoveMode.OnlyLongest => OnlyLongestRegex.Replace(Haystack, ReplaceWithXs),
            RemoveMode.AllLong => AllLongRegex.Replace(Haystack, ReplaceWithXs),
            RemoveMode.All => AllSubstringsRegex.Replace(Haystack, ReplaceWithXs),
            _ => throw new ArgumentOutOfRangeException(nameof(RemoveMode))
        };

        static string ReplaceWithXs(Match match) => string.Concat(Enumerable.Repeat('x', match.Length));
    }

    private static readonly Regex AllLongRegex =
        new("(relationship)|(relationships)|(interrelation)|(interrelations)|(interrelationship)|(interrelationships)|(interrelationship's)");

    private static readonly Regex OnlyLongestRegex =
        new("(interrelationship)|(interrelationships)|(interrelationship's)");

    private static readonly Regex AllSubstringsRegex;

    static Benches()
    {
        StringBuilder regexBuilder = new();
        for (var length = 1; length <= Needle.Length; length += 1)
        {
            for (var start = 0; start + length <= Needle.Length; start += 1)
            {
                var substring = Needle.Substring(start, length);
                regexBuilder.Append($"({substring})|");
            }
        }

        regexBuilder.Length -= 1;
        AllSubstringsRegex = new(regexBuilder.ToString());
    }
}

public enum RemoveMode
{
    All,
    AllLong,
    OnlyLongest,
    None
}
