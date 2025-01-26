using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Migrations
{
    /// <inheritdoc />
    public partial class Initial2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Groups_Schedules_(int)Direction_Cours",
                table: "Groups");

            migrationBuilder.DropIndex(
                name: "IX_Groups_(int)Direction_Cours",
                table: "Groups");

            migrationBuilder.CreateIndex(
                name: "IX_Groups_Cours_(int)Direction",
                table: "Groups",
                columns: new[] { "Cours", "(int)Direction" });

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_Schedules_Cours_(int)Direction",
                table: "Groups",
                columns: new[] { "Cours", "(int)Direction" },
                principalTable: "Schedules",
                principalColumns: new[] { "Cours", "Direction" },
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Groups_Schedules_Cours_(int)Direction",
                table: "Groups");

            migrationBuilder.DropIndex(
                name: "IX_Groups_Cours_(int)Direction",
                table: "Groups");

            migrationBuilder.CreateIndex(
                name: "IX_Groups_(int)Direction_Cours",
                table: "Groups",
                columns: new[] { "(int)Direction", "Cours" });

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_Schedules_(int)Direction_Cours",
                table: "Groups",
                columns: new[] { "(int)Direction", "Cours" },
                principalTable: "Schedules",
                principalColumns: new[] { "Cours", "Direction" },
                onDelete: ReferentialAction.Cascade);
        }
    }
}
