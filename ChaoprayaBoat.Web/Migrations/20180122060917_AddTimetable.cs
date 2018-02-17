using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ChaoprayaBoat.Web.Migrations
{
    public partial class AddTimetable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Destination",
                table: "TimeTables",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Coordinates",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Destination",
                table: "TimeTables");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Coordinates");
        }
    }
}
