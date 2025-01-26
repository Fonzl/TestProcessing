using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Migrations
{
    /// <inheritdoc />
    public partial class CreateDb3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Directions_Directions_DirectionId",
                table: "Directions");

            migrationBuilder.DropIndex(
                name: "IX_Directions_DirectionId",
                table: "Directions");

            migrationBuilder.DropColumn(
                name: "DirectionId",
                table: "Directions");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DirectionId",
                table: "Directions",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Directions_DirectionId",
                table: "Directions",
                column: "DirectionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Directions_Directions_DirectionId",
                table: "Directions",
                column: "DirectionId",
                principalTable: "Directions",
                principalColumn: "Id");
        }
    }
}
