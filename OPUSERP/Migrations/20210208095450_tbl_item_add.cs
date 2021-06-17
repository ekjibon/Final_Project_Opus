using Microsoft.EntityFrameworkCore.Migrations;

namespace OPUSERP.Migrations
{
    public partial class tbl_item_add : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "description",
                schema: "SCM",
                table: "ItemSpecification",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "isBuild",
                schema: "SCM",
                table: "ItemSpecification",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "isMostUsed",
                schema: "SCM",
                table: "ItemSpecification",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "isQR",
                schema: "SCM",
                table: "ItemSpecification",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "reOrderLevel",
                schema: "SCM",
                table: "ItemSpecification",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "storeId",
                schema: "SCM",
                table: "ItemSpecification",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "unitId",
                schema: "SCM",
                table: "ItemSpecification",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "isBuild",
                schema: "SCM",
                table: "Item",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "isMostUsed",
                schema: "SCM",
                table: "Item",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ItemSpecification_storeId",
                schema: "SCM",
                table: "ItemSpecification",
                column: "storeId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemSpecification_unitId",
                schema: "SCM",
                table: "ItemSpecification",
                column: "unitId");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemSpecification_Store_storeId",
                schema: "SCM",
                table: "ItemSpecification",
                column: "storeId",
                principalSchema: "POS",
                principalTable: "Store",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ItemSpecification_Unit_unitId",
                schema: "SCM",
                table: "ItemSpecification",
                column: "unitId",
                principalSchema: "SCM",
                principalTable: "Unit",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemSpecification_Store_storeId",
                schema: "SCM",
                table: "ItemSpecification");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemSpecification_Unit_unitId",
                schema: "SCM",
                table: "ItemSpecification");

            migrationBuilder.DropIndex(
                name: "IX_ItemSpecification_storeId",
                schema: "SCM",
                table: "ItemSpecification");

            migrationBuilder.DropIndex(
                name: "IX_ItemSpecification_unitId",
                schema: "SCM",
                table: "ItemSpecification");

            migrationBuilder.DropColumn(
                name: "description",
                schema: "SCM",
                table: "ItemSpecification");

            migrationBuilder.DropColumn(
                name: "isBuild",
                schema: "SCM",
                table: "ItemSpecification");

            migrationBuilder.DropColumn(
                name: "isMostUsed",
                schema: "SCM",
                table: "ItemSpecification");

            migrationBuilder.DropColumn(
                name: "isQR",
                schema: "SCM",
                table: "ItemSpecification");

            migrationBuilder.DropColumn(
                name: "reOrderLevel",
                schema: "SCM",
                table: "ItemSpecification");

            migrationBuilder.DropColumn(
                name: "storeId",
                schema: "SCM",
                table: "ItemSpecification");

            migrationBuilder.DropColumn(
                name: "unitId",
                schema: "SCM",
                table: "ItemSpecification");

            migrationBuilder.DropColumn(
                name: "isBuild",
                schema: "SCM",
                table: "Item");

            migrationBuilder.DropColumn(
                name: "isMostUsed",
                schema: "SCM",
                table: "Item");
        }
    }
}
