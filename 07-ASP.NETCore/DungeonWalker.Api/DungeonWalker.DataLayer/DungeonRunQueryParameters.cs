namespace DungeonWalker.DataLayer;

public sealed class DungeonRunQueryParameters
{
    public string? DungeonName { get; }

    public string? HeroClass { get; }

    public DungeonRunQueryParameters(string? dungeonName, string? heroClass) =>
        (DungeonName, HeroClass) = (dungeonName, heroClass);
}
