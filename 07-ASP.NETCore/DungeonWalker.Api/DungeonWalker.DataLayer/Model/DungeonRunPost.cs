using DungeonWalker.DataLayer.Model;

namespace DungeonWalker.DataLayer.Model;

/// <summary>
///     Model used for creating new DungeonRun entities.
/// </summary>
public sealed class DungeonRunPost
{
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
    ///     Creates a new instance <see cref="DungeonRunPost"/>.
    /// </summary>
    /// <param name="roomsCleared">Number of Dungeon's rooms cleared in the run.</param>
    /// <param name="damageDealt">Damage dealt by the Hero during the run.</param>
    /// <param name="dungeonName">Name of the Dungeon.</param>
    /// <param name="heroClass">Name of the Hero's class.</param>
    public DungeonRunPost(int roomsCleared, int damageDealt, string dungeonName, string heroClass) =>
        (RoomsCleared, DamageDealt, DungeonName, HeroClass) =
            (roomsCleared, damageDealt, dungeonName, heroClass);
}