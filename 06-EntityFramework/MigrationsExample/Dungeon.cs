namespace MigrationsExample;

public sealed class Dungeon
{
    public int Id { get; private init; }

    public string Name { get; private init; }

    public int NumberOfRooms { get; private init; }

    public IReadOnlyCollection<DungeonRun> Runs { get; private init; } = new HashSet<DungeonRun>();

    public Dungeon(string name, int numberOfRooms) =>
        (Name, NumberOfRooms) = (name, numberOfRooms);
}