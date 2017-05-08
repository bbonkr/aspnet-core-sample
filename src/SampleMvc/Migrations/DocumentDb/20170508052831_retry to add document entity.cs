using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SampleMvc.Migrations.DocumentDb
{
    public partial class retrytoadddocumententity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Documents",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AnswerNum = table.Column<int>(nullable: false),
                    CommentCount = table.Column<int>(nullable: false),
                    Content = table.Column<string>(maxLength: 4000, nullable: false),
                    ModifyDate = table.Column<DateTime>(nullable: false),
                    ModifyIp = table.Column<string>(nullable: true),
                    Name = table.Column<string>(maxLength: 30, nullable: false),
                    ParentNum = table.Column<int>(nullable: false),
                    PostDate = table.Column<DateTime>(nullable: false),
                    PostIp = table.Column<DateTime>(nullable: false),
                    ReadCount = table.Column<int>(nullable: false),
                    Ref = table.Column<int>(nullable: false),
                    RefOrder = table.Column<int>(nullable: false),
                    Step = table.Column<int>(nullable: false),
                    Title = table.Column<string>(maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documents", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Documents");
        }
    }
}
