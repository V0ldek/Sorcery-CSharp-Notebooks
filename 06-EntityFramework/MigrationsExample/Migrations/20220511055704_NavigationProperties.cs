using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MigrationsExample.Migrations
{
    public partial class NavigationProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DungeonName",
                table: "DungeonRun");

            migrationBuilder.DropColumn(
                name: "HeroClass",
                table: "DungeonRun");

            migrationBuilder.AddColumn<int>(
                name: "DungeonId",
                table: "DungeonRun",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "HeroId",
                table: "DungeonRun",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Dungeon",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    NumberOfRooms = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dungeon", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Hero",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hero", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DungeonRun_DungeonId",
                table: "DungeonRun",
                column: "DungeonId");

            migrationBuilder.CreateIndex(
                name: "IX_DungeonRun_HeroId",
                table: "DungeonRun",
                column: "HeroId");

            migrationBuilder.AddForeignKey(
                name: "FK_DungeonRun_Dungeon_DungeonId",
                table: "DungeonRun",
                column: "DungeonId",
                principalTable: "Dungeon",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DungeonRun_Hero_HeroId",
                table: "DungeonRun",
                column: "HeroId",
                principalTable: "Hero",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DungeonRun_Dungeon_DungeonId",
                table: "DungeonRun");

            migrationBuilder.DropForeignKey(
                name: "FK_DungeonRun_Hero_HeroId",
                table: "DungeonRun");

            migrationBuilder.DropTable(
                name: "Dungeon");

            migrationBuilder.DropTable(
                name: "Hero");

            migrationBuilder.DropIndex(
                name: "IX_DungeonRun_DungeonId",
                table: "DungeonRun");

            migrationBuilder.DropIndex(
                name: "IX_DungeonRun_HeroId",
                table: "DungeonRun");

            migrationBuilder.DropColumn(
                name: "DungeonId",
                table: "DungeonRun");

            migrationBuilder.DropColumn(
                name: "HeroId",
                table: "DungeonRun");

            migrationBuilder.AddColumn<string>(
                name: "DungeonName",
                table: "DungeonRun",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "HeroClass",
                table: "DungeonRun",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
}
