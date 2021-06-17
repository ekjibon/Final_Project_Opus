using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OPUSERP.Migrations
{
    public partial class tbl_hall_rent_added : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "hallInfos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    isDelete = table.Column<int>(nullable: true),
                    createdAt = table.Column<DateTime>(nullable: true),
                    updatedAt = table.Column<DateTime>(nullable: true),
                    createdBy = table.Column<string>(maxLength: 250, nullable: true),
                    updatedBy = table.Column<string>(maxLength: 250, nullable: true),
                    hallName = table.Column<string>(nullable: true),
                    hallNameBn = table.Column<string>(nullable: true),
                    floor = table.Column<string>(nullable: true),
                    seatCapacity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_hallInfos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "hallRentalShifts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    isDelete = table.Column<int>(nullable: true),
                    createdAt = table.Column<DateTime>(nullable: true),
                    updatedAt = table.Column<DateTime>(nullable: true),
                    createdBy = table.Column<string>(maxLength: 250, nullable: true),
                    updatedBy = table.Column<string>(maxLength: 250, nullable: true),
                    shiftName = table.Column<string>(nullable: true),
                    shiftNameBn = table.Column<string>(nullable: true),
                    startTime = table.Column<string>(nullable: true),
                    endTime = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_hallRentalShifts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "hallRentalApplicationInfos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    isDelete = table.Column<int>(nullable: true),
                    createdAt = table.Column<DateTime>(nullable: true),
                    updatedAt = table.Column<DateTime>(nullable: true),
                    createdBy = table.Column<string>(maxLength: 250, nullable: true),
                    updatedBy = table.Column<string>(maxLength: 250, nullable: true),
                    hallInfoId = table.Column<int>(nullable: true),
                    hallRentalShiftId = table.Column<int>(nullable: true),
                    hallRent = table.Column<decimal>(nullable: true),
                    rentalDate = table.Column<DateTime>(nullable: true),
                    rentalTime = table.Column<string>(nullable: true),
                    applicantName = table.Column<string>(nullable: true),
                    applicantOrganization = table.Column<string>(nullable: true),
                    applicantAddress = table.Column<string>(nullable: true),
                    mobileNo = table.Column<string>(nullable: true),
                    applicantSignUrl = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_hallRentalApplicationInfos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_hallRentalApplicationInfos_hallInfos_hallInfoId",
                        column: x => x.hallInfoId,
                        principalTable: "hallInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_hallRentalApplicationInfos_hallRentalShifts_hallRentalShiftId",
                        column: x => x.hallRentalShiftId,
                        principalTable: "hallRentalShifts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_hallRentalApplicationInfos_hallInfoId",
                table: "hallRentalApplicationInfos",
                column: "hallInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_hallRentalApplicationInfos_hallRentalShiftId",
                table: "hallRentalApplicationInfos",
                column: "hallRentalShiftId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "hallRentalApplicationInfos");

            migrationBuilder.DropTable(
                name: "hallInfos");

            migrationBuilder.DropTable(
                name: "hallRentalShifts");
        }
    }
}
