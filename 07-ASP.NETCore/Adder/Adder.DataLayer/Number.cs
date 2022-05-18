namespace Adder.DataLayer;

public sealed class Number
{
    public int Id { get; private init; }

    public string Name { get; private init; }

    public int Value { get; private init; }

    public Number(string name, int value) => (Name, Value) = (name, value);
}