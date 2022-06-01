using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
using RefReturns;

BenchmarkRunner.Run<Benches>();

[SimpleJob(RuntimeMoniker.Net60)]
[MemoryDiagnoser]
public class Benches
{
    private readonly Box _box = new();

    private readonly RefBox _refBox = new();

    [Benchmark(Baseline = true)]
    public Large Box() => _box.Large;

    [Benchmark]
    public ref readonly Large RefBox() => ref _refBox.Large;
}