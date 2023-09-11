﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Занятие_в_аудитории_1_29._08._2023__ADO.NET_.Migrations
{
    /// <inheritdoc />
    public partial class NavSecDep : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Managers_IdSecDep",
                table: "Managers",
                column: "IdSecDep");

            migrationBuilder.AddForeignKey(
                name: "FK_Managers_Departments_IdSecDep",
                table: "Managers",
                column: "IdSecDep",
                principalTable: "Departments",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Managers_Departments_IdSecDep",
                table: "Managers");

            migrationBuilder.DropIndex(
                name: "IX_Managers_IdSecDep",
                table: "Managers");
        }
    }
}
