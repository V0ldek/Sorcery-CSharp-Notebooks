namespace DungeonWalker.DataLayer.Model;

public sealed class Hero
{
    public int Id { get; private init; }

    public string Name { get; private init; }

    public IReadOnlyCollection<DungeonRun> Runs { get; private init; } = new HashSet<DungeonRun>();

    public Hero(string name) => Name = name;
}
