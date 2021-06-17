using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OPUSERP.Migrations
{
    public partial class Hallpaymentupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "hallRentalPayments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    isDelete = table.Column<int>(nullable: true),
                    createdAt = table.Column<DateTime>(nullable: true),
                    updatedAt = table.Column<DateTime>(nullable: true),
                    createdBy = table.Column<string>(maxLength: 250, nullable: true),
                    updatedBy = table.Column<string>(maxLength: 250, nullable: true),
                    hallRentalApplicationInfoId = table.Column<int>(nullable: true),
                    paymentMode = table.Column<int>(nullable: false),
                    bankName = table.Column<string>(nullable: true),
                    branchName = table.Column<string>(nullable: true),
                    chequeNo = table.Column<string>(nullable: true),
                    paymentDate = table.Column<DateTime>(nullable: true),
                    amount = table.Column<decimal>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_hallRentalPayments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_hallRentalPayments_hallRentalApplicationInfos_hallRentalApplicationInfoId",
                        column: x => x.hallRentalApplicationInfoId,
                        principalTable: "hallRentalApplicationInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_hallRentalPayments_hallRentalApplicationInfoId",
                table: "hallRentalPayments",
                column: "hallRentalApplicationInfoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "hallRentalPayments");
        }
    }
}
