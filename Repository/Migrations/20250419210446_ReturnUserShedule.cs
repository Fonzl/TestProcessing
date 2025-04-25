using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Migrations
{
    /// <inheritdoc />
    public partial class ReturnUserShedule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Groups_Cours_DirectionId",
                table: "Groups",
                columns: new[] { "Cours", "DirectionId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_Schedules_Cours_DirectionId",
                table: "Groups",
                columns: new[] { "Cours", "DirectionId" },
                principalTable: "Schedules",
                principalColumns: new[] { "Cours", "DirectionId" },
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Groups_Schedules_Cours_DirectionId",
                table: "Groups");

            migrationBuilder.DropIndex(
                name: "IX_Groups_Cours_DirectionId",
                table: "Groups");
        }
    }
}
