using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using SampleMvc.Data;

namespace SampleMvc.Migrations.DocumentDb
{
    public partial class Modifyerror : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PostIp",
                table: "Documents",
                nullable: true,
                oldClrType: typeof(DateTime));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "PostIp",
                table: "Documents",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
