using DungeonWalker.DataLayer.Model;

namespace DungeonWalker.Api.Model;

/// <summary>
///     Dungeon run of a Hero recorded in the system.
/// </summary>
public sealed class DungeonRunView
{
    /// <summary>
    ///     Unique identifier of the DungeonRun.
    /// </summary>
    public int Id { get; }

    /// <summary>
    ///     Number of Dungeon's rooms cleared in the run.
    /// </summary>
    public int RoomsCleared { get; }

    /// <summary>
    ///     Damage dealt by the Hero during the run.
    /// </summary>
    public int DamageDealt { get; }

    /// <summary>
    ///     Name of the Dungeon.
    /// </summary>
    public string DungeonName { get; }

    /// <summary>
    ///     Name of the Hero's class.
    /// </summary>
    public string HeroClass { get; }

    /// <summary>
    ///     Creates a new instance <see cref="DungeonRunView"/>.
    /// </summary>
    /// <param name="id">Unique identifier of the DungeonRun.</param>
    /// <param name="roomsCleared">Number of Dungeon's rooms cleared in the run.</param>
    /// <param name="damageDealt">Damage dealt by the Hero during the run.</param>
    /// <param name="dungeonName">Name of the Dungeon.</param>
    /// <param name="heroClass">Name of the Hero's class.</param>
    public DungeonRunView(int id, int roomsCleared, int damageDealt, string dungeonName, string heroClass) =>
        (Id, RoomsCleared, DamageDealt, DungeonName, HeroClass) =
            (id, roomsCleared, damageDealt, dungeonName, heroClass);

    internal static DungeonRunView FromDungeonRun(DungeonRun dungeonRun) =>
        new(
            dungeonRun.Id,
            dungeonRun.RoomsCleared,
            dungeonRun.DamageDealt,
            dungeonRun.Dungeon.Name,
            dungeonRun.Hero.Name
        );
}
