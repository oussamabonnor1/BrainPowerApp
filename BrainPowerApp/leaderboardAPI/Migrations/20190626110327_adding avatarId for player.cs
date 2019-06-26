using Microsoft.EntityFrameworkCore.Migrations;

namespace leaderboardAPI.Migrations
{
    public partial class addingavatarIdforplayer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "avatarId",
                table: "players",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "avatarId",
                table: "players");
        }
    }
}
