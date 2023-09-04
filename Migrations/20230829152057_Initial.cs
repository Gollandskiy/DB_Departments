﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Занятие_в_аудитории_1_29._08._2023__ADO.NET_.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Managers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Secname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Login = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PassSalt = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PassDk = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdMainDep = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdSecDep = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IdChief = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreateDt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeleteDt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Avatar = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Managers", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "Managers");
        }
    }
}
