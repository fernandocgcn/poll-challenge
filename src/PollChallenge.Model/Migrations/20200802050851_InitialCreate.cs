using Microsoft.EntityFrameworkCore.Migrations;

namespace PollChallenge.Model.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tb_poll",
                columns: table => new
                {
                    poll_id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    description = table.Column<string>(nullable: false),
                    views_qty = table.Column<long>(nullable: false, defaultValue: 0L)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_poll", x => x.poll_id);
                });

            migrationBuilder.CreateTable(
                name: "tb_option",
                columns: table => new
                {
                    option_id = table.Column<int>(nullable: false),
                    poll_id = table.Column<int>(nullable: false),
                    description = table.Column<string>(nullable: false),
                    votes_qty = table.Column<long>(nullable: false, defaultValue: 0L)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_option", x => new { x.poll_id, x.option_id });
                    table.ForeignKey(
                        name: "FK_tb_option_tb_poll_poll_id",
                        column: x => x.poll_id,
                        principalTable: "tb_poll",
                        principalColumn: "poll_id",
                        onDelete: ReferentialAction.Restrict);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_option");

            migrationBuilder.DropTable(
                name: "tb_poll");
        }
    }
}
