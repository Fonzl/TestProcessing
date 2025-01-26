using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Migrations
{
    /// <inheritdoc />
    public partial class CreateDb4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Schedules_DirectionId",
                table: "Schedules");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_DirectionId",
                table: "Schedules",
                column: "DirectionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Schedules_DirectionId",
                table: "Schedules");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_DirectionId",
                table: "Schedules",
                column: "DirectionId",
                unique: true);
        }
    }
}
