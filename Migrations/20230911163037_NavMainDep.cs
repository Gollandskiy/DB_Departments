using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Занятие_в_аудитории_1_29._08._2023__ADO.NET_.Migrations
{
    /// <inheritdoc />
    public partial class NavMainDep : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Managers_IdMainDep",
                table: "Managers",
                column: "IdMainDep");

            migrationBuilder.AddForeignKey(
                name: "FK_Managers_Departments_IdMainDep",
                table: "Managers",
                column: "IdMainDep",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Managers_Departments_IdMainDep",
                table: "Managers");

            migrationBuilder.DropIndex(
                name: "IX_Managers_IdMainDep",
                table: "Managers");
        }
    }
}
