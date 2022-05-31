using System.Runtime.CompilerServices;

const int Repetitions = 64;

var benches = new Benches();
benches.RemoveMode = RemoveMode.All;
await benches.SetupAsync();

for (var i = 0; i < Repetitions; i += 1)
{
    Console.Write($"{i}/{Repetitions}");
    Console.CursorLeft = 0;
    Use(benches.FromShortestSearchIterative());
}
Console.Write($"{Repetitions}/{Repetitions}");

[MethodImpl(MethodImplOptions.NoInlining)]
void Use<T>(T _) { }