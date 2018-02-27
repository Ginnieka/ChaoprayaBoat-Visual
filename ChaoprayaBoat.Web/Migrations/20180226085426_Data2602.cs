using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ChaoprayaBoat.Web.Migrations
{
    public partial class Data2602 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "Members",
                maxLength: 45,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 45);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Members",
                maxLength: 45,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 45);

            migrationBuilder.CreateTable(
                name: "MemberHistories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Datetime = table.Column<DateTime>(nullable: false),
                    DestinationCoordinteId = table.Column<double>(nullable: false),
                    MemberId = table.Column<int>(nullable: false),
                    SourceCoordinateId = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MemberHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MemberHistories_Members_MemberId",
                        column: x => x.MemberId,
                        principalTable: "Members",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MemberHistories_MemberId",
                table: "MemberHistories",
                column: "MemberId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MemberHistories");

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "Members",
                maxLength: 45,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 45,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Members",
                maxLength: 45,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 45,
                oldNullable: true);
        }
    }
}
