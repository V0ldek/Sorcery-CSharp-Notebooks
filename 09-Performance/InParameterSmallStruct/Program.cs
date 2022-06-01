using System.Runtime.CompilerServices;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;

BenchmarkRunner.Run<Benches>();

[SimpleJob(RuntimeMoniker.Net60)]
[MemoryDiagnoser]
public class Benches
{
    public int X { get; set; }

    [GlobalSetup]
    public void Setup() => X = 42;

    [Benchmark(Baseline = true)]
    public void TakeByValue() => TakeByValue(X);

    [Benchmark]
    public void TakeByInReference() => TakeByInReference(X);

    [MethodImpl(MethodImplOptions.NoInlining)]
    void TakeByValue(int _)
    {
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    void TakeByInReference(in int _)
    {
    }
}