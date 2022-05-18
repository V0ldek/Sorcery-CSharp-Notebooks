using DungeonWalker.DataLayer.Model;

namespace DungeonWalker.Api.Model;

internal static class EntityToViewExtensions
{
    public static DungeonView ToView(this Dungeon dungeon) => DungeonView.FromDungeon(dungeon);

    public static DungeonRunView ToView(this DungeonRun dungeonRun) => DungeonRunView.FromDungeonRun(dungeonRun);
}