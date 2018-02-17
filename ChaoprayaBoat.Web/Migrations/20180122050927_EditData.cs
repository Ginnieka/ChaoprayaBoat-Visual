using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ChaoprayaBoat.Web.Migrations
{
    public partial class EditData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "Longtitude",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "Boats");

            migrationBuilder.DropColumn(
                name: "Longtitude",
                table: "Boats");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Members",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Detail",
                table: "Boats",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Boats",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Boats",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "Detail",
                table: "Boats");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Boats");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Boats");

            migrationBuilder.AddColumn<double>(
                name: "Latitude",
                table: "Members",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Longtitude",
                table: "Members",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Latitude",
                table: "Boats",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Longtitude",
                table: "Boats",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
