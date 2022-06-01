using System.Runtime.CompilerServices;

namespace InParameter;

public readonly struct Large
{
    public QuiteBig Value1 { get; init; }
    public QuiteBig Value2 { get; init; }
    public QuiteBig Value3 { get; init; }
    public QuiteBig Value4 { get; init; }
    public QuiteBig Value5 { get; init; }
    public QuiteBig Value6 { get; init; }
    public QuiteBig Value7 { get; init; }
    public QuiteBig Value8 { get; init; }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public void Foo() { }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public void Bar() { }
}