using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ChaoprayaBoat.Web.Migrations
{
    public partial class DataNew2602 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FacebookId",
                table: "Members",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FacebookPicture",
                table: "Members",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FacebookToken",
                table: "Members",
                maxLength: 100,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FacebookId",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "FacebookPicture",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "FacebookToken",
                table: "Members");
        }
    }
}
