using System.Text.RegularExpressions;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;

BenchmarkRunner.Run<Benches>();

[SimpleJob(RuntimeMoniker.Net60)]
public class Benches
{
    [Params(RemoveLong.All, RemoveLong.OnlyLongest, RemoveLong.None)]
    public RemoveLong RemoveLong { get; set; }

    public string Haystack { get; private set; } = null!;

    public static string Needle => "interrelationships";

    private static readonly Regex AllLongRegex =
        new("(interrelation)|(interrelations)|(interrelationship)|(interrelationships)|(interrelationship's)");

    private static readonly Regex OnlyLongestRegex =
        new("(interrelationship)|(interrelationships)|(interrelationship's)");

    [GlobalSetup]
    public async Task Setup()
    {
        Haystack = await File.ReadAllTextAsync("./data/words.txt");

        Haystack = RemoveLong switch
        {
            RemoveLong.None => Haystack,
            RemoveLong.All => AllLongRegex.Replace(Haystack, "keep looking"),
            RemoveLong.OnlyLongest => OnlyLongestRegex.Replace(Haystack, "keep looking"),
            _ => throw new ArgumentOutOfRangeException(nameof(RemoveLong))
        };
    }


    [Benchmark(Baseline = true)]
    public string SimpleSearch() =>
        BenchmarkDemo.Library.SimpleSearch.FindLongestSubstring(Needle, Haystack);

    [Benchmark]
    public string FromShortestSearch() =>
        BenchmarkDemo.Library.FromShortestSearch.FindLongestSubstring(Needle, Haystack);
}

public enum RemoveLong
{
    All,
    OnlyLongest,
    None
}
