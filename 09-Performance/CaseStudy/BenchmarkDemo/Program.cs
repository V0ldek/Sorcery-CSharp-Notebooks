﻿using System.Text;
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
[StatisticalTestColumn(StatisticalTestKind.MannWhitney, ThresholdUnit.Ratio, 0.03, true)]
public class Benches
{
    [Params(0.0, 0.5, 0.75, 1.0)]
    public double ResultSize { get; set; }

    public string Haystack { get; private set; } = null!;

    public static string Needle => "interrelationships";

    [Benchmark(Baseline = true)]
    public string FromLongestSearch() =>
        BenchmarkDemo.Library.FromLongestSearch.FindLongestSubstring(Needle, Haystack);

    // [Benchmark]
    // public string SpanBasedFromLongestSearch() =>
    //     BenchmarkDemo.Library.FromLongestSearch.SpanBasedFindLongestSubstring(Needle, Haystack);

    [Benchmark]
    public string FromShortestSearch() =>
        BenchmarkDemo.Library.FromShortestSearch.FindLongestSubstring(Needle, Haystack);

    // [Benchmark]
    // public string SpanBasedFromShortestSearch() =>
    //     BenchmarkDemo.Library.FromShortestSearch.SpanBasedFindLongestSubstring(Needle, Haystack);

    // [Benchmark]
    // public string FromShortestWithEliminationSearch() =>
    //     BenchmarkDemo.Library.FromShortestWithEliminationSearch.FindLongestSubstring(Needle, Haystack);

    // [Benchmark]
    // public string SpanBasedFromShortestWithEliminationSearch() =>
    //     BenchmarkDemo.Library.FromShortestWithEliminationSearch.SpanBasedFindLongestSubstring(Needle, Haystack);

    [GlobalSetup]
    public async Task SetupAsync()
    {
        StringBuilder regexBuilder = new();

        var length = Math.Max((int)Math.Ceiling(ResultSize * Needle.Length), 1);

        for (var start = 0; start + length <= Needle.Length; start += 1)
        {
            regexBuilder.Append('(');
            regexBuilder.Append(Needle.AsSpan().Slice(start, length));
            regexBuilder.Append(")|");
        }

        Haystack = await File.ReadAllTextAsync("./data/words.txt");

        if (regexBuilder.Length > 0)
        {
            regexBuilder.Length -= 1;
            var regex = new Regex(regexBuilder.ToString());
            Haystack = regex.Replace(Haystack, ReplaceWithXs);
        }

        static string ReplaceWithXs(Match match) => string.Concat(Enumerable.Repeat('#', match.Length));
    }
}