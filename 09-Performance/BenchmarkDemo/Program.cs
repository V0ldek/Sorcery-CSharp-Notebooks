using System.Text;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;

BenchmarkRunner.Run<Benches>();

[SimpleJob(RuntimeMoniker.Net60)]
public class Benches
{
    [Params(1_000)]
    public int HaystackLength { get; set; }

    [Params(50)]
    public int NeedleLength { get; set; }

    public string Haystack { get; private set; } = null!;

    public string Needle { get; private set; } = null!;

    [GlobalSetup]
    public void Setup()
    {
        var haystackBuilder = new StringBuilder(HaystackLength);
        var currentLength = 1;

        while (haystackBuilder.Length < HaystackLength)
        {
            for (var i = 0; i < currentLength; i += 1)
            {
                haystackBuilder.Append('a');
            }

            haystackBuilder.Append('b');

            currentLength = Math.Min(currentLength + 1, HaystackLength - haystackBuilder.Length - 1);
        }

        Needle = string.Concat(Enumerable.Repeat('a', NeedleLength));
        Haystack = haystackBuilder.ToString();
    }


    [Benchmark(Baseline = true)]
    public string SimpleSearch() => BenchmarkDemo.Library.SimpleSearch.FindLongestSubstring(Needle, Haystack);
}