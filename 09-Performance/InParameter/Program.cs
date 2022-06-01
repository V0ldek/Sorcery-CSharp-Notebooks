using System.Runtime.CompilerServices;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
using InParameter;

BenchmarkRunner.Run<Benches>();

[SimpleJob(RuntimeMoniker.Net60)]
[MemoryDiagnoser]
public class Benches
{
    [Benchmark(Baseline = true)]
    public void TakeByValue()
    {
        Large large = default;
        TakeByValue(large);
    }

    [Benchmark]
    public void TakeByInReference()
    {
        Large large = default;
        TakeByInReference(in large);
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    void TakeByValue(Large large)
    {
        large.Foo();
        large.Bar();
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    void TakeByInReference(in Large large)
    {
        large.Foo();
        large.Bar();
    }
}