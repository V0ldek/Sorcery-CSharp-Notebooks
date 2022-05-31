using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;

BenchmarkRunner.Run<Benches>();

[SimpleJob(RuntimeMoniker.Net60)]
[MemoryDiagnoser]
public class Benches
{
    private const int Seed = 2137;

    private int[] _array = null!;

    [Params(10_000)]
    public int ArraySize { get; set; }

    [Params(10)]
    public int SliceSize { get; set; }

    [Benchmark(Baseline = true)]
    public long AllocatingImpl() =>
        RefStructs.AllocatingImpl.Calculate(_array, SliceSize);

    [Benchmark]
    public long NonallocatingImpl() =>
        RefStructs.NonallocatingImpl.Calculate(_array, SliceSize);

    [GlobalSetup]
    public void Setup()
    {
        var random = new Random(Seed);
        _array = Enumerable.Range(0, ArraySize).Select(_ => random.Next(1, 100)).ToArray();
    }
}