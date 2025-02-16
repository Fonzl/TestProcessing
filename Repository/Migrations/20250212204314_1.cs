using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Repository.Migrations
{
    /// <inheritdoc />
    public partial class _1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Results_UserResponses_Id",
                table: "Results");

            migrationBuilder.AddColumn<long>(
                name: "ResultTestId",
                table: "UserResponses",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "Results",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.InsertData(
                table: "CategoryTasks",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Обычный вопрос" },
                    { 2, "Вопрос с множеством ответов" },
                    { 3, "Вопрос с ответом пользователя" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserResponses_ResultTestId",
                table: "UserResponses",
                column: "ResultTestId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserResponses_Results_ResultTestId",
                table: "UserResponses",
                column: "ResultTestId",
                principalTable: "Results",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserResponses_Results_ResultTestId",
                table: "UserResponses");

            migrationBuilder.DropIndex(
                name: "IX_UserResponses_ResultTestId",
                table: "UserResponses");

            migrationBuilder.DeleteData(
                table: "CategoryTasks",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "CategoryTasks",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "CategoryTasks",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DropColumn(
                name: "ResultTestId",
                table: "UserResponses");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "Results",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddForeignKey(
                name: "FK_Results_UserResponses_Id",
                table: "Results",
                column: "Id",
                principalTable: "UserResponses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
