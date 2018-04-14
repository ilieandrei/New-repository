using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DataLayer.Migrations
{
    public partial class timetableMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Timetables",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Day = table.Column<string>(nullable: true),
                    From = table.Column<string>(nullable: true),
                    Group = table.Column<string>(nullable: true),
                    Hall = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Pack = table.Column<string>(nullable: true),
                    Teacher = table.Column<string>(nullable: true),
                    To = table.Column<string>(nullable: true),
                    Week = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Timetables", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Timetables");
        }
    }
}
