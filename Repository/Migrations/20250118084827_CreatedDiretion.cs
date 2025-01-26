using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Repository.Migrations
{
    /// <inheritdoc />
    public partial class CreatedDiretion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Groups_Schedules_Cours_(int)Direction",
                table: "Groups");

            migrationBuilder.DropIndex(
                name: "IX_Groups_Cours_(int)Direction",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "(int)Direction",
                table: "Groups");

            migrationBuilder.RenameColumn(
                name: "Direction",
                table: "Schedules",
                newName: "DirectionId");

            migrationBuilder.RenameColumn(
                name: "Direction",
                table: "Groups",
                newName: "DirectionId");

            migrationBuilder.RenameColumn(
                name: "SchedulesDirection",
                table: "DisciplineSchedule",
                newName: "SchedulesDirectionId");

            migrationBuilder.RenameIndex(
                name: "IX_DisciplineSchedule_SchedulesCours_SchedulesDirection",
                table: "DisciplineSchedule",
                newName: "IX_DisciplineSchedule_SchedulesCours_SchedulesDirectionId");

            migrationBuilder.CreateTable(
                name: "Directions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    DirectionId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Directions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Directions_Directions_DirectionId",
                        column: x => x.DirectionId,
                        principalTable: "Directions",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_DirectionId",
                table: "Schedules",
                column: "DirectionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Groups_Cours_DirectionId",
                table: "Groups",
                columns: new[] { "Cours", "DirectionId" });

            migrationBuilder.CreateIndex(
                name: "IX_Groups_DirectionId",
                table: "Groups",
                column: "DirectionId");

            migrationBuilder.CreateIndex(
                name: "IX_Directions_DirectionId",
                table: "Directions",
                column: "DirectionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_Directions_DirectionId",
                table: "Groups",
                column: "DirectionId",
                principalTable: "Directions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_Schedules_Cours_DirectionId",
                table: "Groups",
                columns: new[] { "Cours", "DirectionId" },
                principalTable: "Schedules",
                principalColumns: new[] { "Cours", "DirectionId" },
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_Directions_DirectionId",
                table: "Schedules",
                column: "DirectionId",
                principalTable: "Directions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Groups_Directions_DirectionId",
                table: "Groups");

            migrationBuilder.DropForeignKey(
                name: "FK_Groups_Schedules_Cours_DirectionId",
                table: "Groups");

            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_Directions_DirectionId",
                table: "Schedules");

            migrationBuilder.DropTable(
                name: "Directions");

            migrationBuilder.DropIndex(
                name: "IX_Schedules_DirectionId",
                table: "Schedules");

            migrationBuilder.DropIndex(
                name: "IX_Groups_Cours_DirectionId",
                table: "Groups");

            migrationBuilder.DropIndex(
                name: "IX_Groups_DirectionId",
                table: "Groups");

            migrationBuilder.RenameColumn(
                name: "DirectionId",
                table: "Schedules",
                newName: "Direction");

            migrationBuilder.RenameColumn(
                name: "DirectionId",
                table: "Groups",
                newName: "Direction");

            migrationBuilder.RenameColumn(
                name: "SchedulesDirectionId",
                table: "DisciplineSchedule",
                newName: "SchedulesDirection");

            migrationBuilder.RenameIndex(
                name: "IX_DisciplineSchedule_SchedulesCours_SchedulesDirectionId",
                table: "DisciplineSchedule",
                newName: "IX_DisciplineSchedule_SchedulesCours_SchedulesDirection");

            migrationBuilder.AddColumn<int>(
                name: "(int)Direction",
                table: "Groups",
                type: "integer",
                nullable: false,
                defaultValue: 0);

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
    }
}
