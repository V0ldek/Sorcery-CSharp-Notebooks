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

    [Params(100_000)]
    public int ArraySize { get; set; }

    [Benchmark(Baseline = true)]
    public long ForeachSum()
    {
        long sum = 0;

        foreach (var x in _array)
        {
            sum += x;
        }

        return sum;
    }

    [Benchmark]
    public long LinqSum() => _array.Sum(x => (long)x);

    [Benchmark]
    public long ForSum()
    {
        long sum = 0;

        for (var i = 0; i < _array.Length; i += 1)
        {
            sum += _array[i];
        }

        return sum;
    }

    [Benchmark]
    public long UnsafeSum()
    {
        long sum = 0;
        int i = 0;
        int n = _array.Length;

        unsafe
        {
            fixed (int* pointer = &_array[0])
            {
                while (i < n)
                {
                    sum += pointer[i];
                    i += 1;
                }
            }
        }

        return sum;
    }

    [GlobalSetup]
    public void Setup()
    {
        var random = new Random(Seed);
        _array = Enumerable.Range(0, ArraySize).Select(_ => random.Next(1, 100)).ToArray();
    }
}