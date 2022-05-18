using DungeonWalker.DataLayer.Model;

namespace DungeonWalker.Api.Model;

/// <summary>
///     Dungeon in the system.
/// </summary>
public sealed class DungeonView
{
    /// <summary>
    ///     Unique identifier of the Dungeon.
    /// </summary>
    public int Id { get; private init; }

    /// <summary>
    ///     Unique name of the Dungeon.
    /// </summary>
    public string Name { get; private init; }

    /// <summary>
    ///     Size of the Dungeon in rooms.
    /// </summary>
    public int NumberOfRooms { get; private init; }

    /// <summary>
    ///     Creates a new instance of <see cref="DungeonView"/>.
    /// </summary>
    /// <param name="id">Unique identifier of the Dungeon.</param>
    /// <param name="name">Unique name of the Dungeon.</param>
    /// <param name="numberOfRooms">Size of the Dungeon in rooms.</param>
    public DungeonView(int id, string name, int numberOfRooms) =>
        (Id, Name, NumberOfRooms) = (id, name, numberOfRooms);

    internal static DungeonView FromDungeon(Dungeon dungeon) =>
        new(dungeon.Id, dungeon.Name, dungeon.NumberOfRooms);
}
