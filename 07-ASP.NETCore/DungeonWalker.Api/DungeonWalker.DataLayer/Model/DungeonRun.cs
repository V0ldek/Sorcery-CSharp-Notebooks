namespace DungeonWalker.DataLayer.Model;

public sealed class DungeonRun
{
    public int Id { get; private init; }

    public int RoomsCleared { get; private init; }

    public int DamageDealt { get; private init; }

    public Dungeon Dungeon { get; private init; } = null!;

    public Hero Hero { get; private init; } = null!;

    private DungeonRun(int roomsCleared, int damageDealt) =>
        (RoomsCleared, DamageDealt) = (roomsCleared, damageDealt);

    public DungeonRun(Dungeon dungeon, Hero hero, int roomsCleared, int damageDealt) =>
        (Dungeon, Hero, RoomsCleared, DamageDealt) =
            (dungeon, hero, roomsCleared, damageDealt);
}
