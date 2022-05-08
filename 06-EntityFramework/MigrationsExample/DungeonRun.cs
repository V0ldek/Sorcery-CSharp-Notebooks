namespace MigrationsExample;

public sealed class DungeonRun
{
    public int Id { get; private init; }

    public string HeroClass { get; private init; }

    public string DungeonName { get; private init; }

    public int RoomsCleared { get; private init; }

    public int DamageDealt { get; private init; }

    public DungeonRun(string heroClass, string dungeonName, int roomsCleared, int damageDealt) =>
        (HeroClass, DungeonName, RoomsCleared, DamageDealt) =
            (heroClass, dungeonName, roomsCleared, damageDealt);
}