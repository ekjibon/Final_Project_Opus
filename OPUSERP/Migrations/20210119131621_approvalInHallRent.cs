using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OPUSERP.Migrations
{
    public partial class approvalInHallRent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "chiefGuest",
                table: "hallRentalApplicationInfos",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "isPaid",
                table: "hallRentalApplicationInfos",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "paymentDate",
                table: "hallRentalApplicationInfos",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "remarks",
                table: "hallRentalApplicationInfos",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "specialGuest",
                table: "hallRentalApplicationInfos",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "status",
                table: "hallRentalApplicationInfos",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "chiefGuest",
                table: "hallRentalApplicationInfos");

            migrationBuilder.DropColumn(
                name: "isPaid",
                table: "hallRentalApplicationInfos");

            migrationBuilder.DropColumn(
                name: "paymentDate",
                table: "hallRentalApplicationInfos");

            migrationBuilder.DropColumn(
                name: "remarks",
                table: "hallRentalApplicationInfos");

            migrationBuilder.DropColumn(
                name: "specialGuest",
                table: "hallRentalApplicationInfos");

            migrationBuilder.DropColumn(
                name: "status",
                table: "hallRentalApplicationInfos");
        }
    }
}
