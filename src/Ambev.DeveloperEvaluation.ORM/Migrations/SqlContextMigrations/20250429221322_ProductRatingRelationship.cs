using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ambev.DeveloperEvaluation.ORM.Migrations.SqlContextMigrations
{
    /// <inheritdoc />
    public partial class ProductRatingRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductRating_Products_ProductId",
                table: "ProductRating");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductRating",
                table: "ProductRating");

            migrationBuilder.RenameTable(
                name: "ProductRating",
                newName: "ProductRatings");

            migrationBuilder.RenameIndex(
                name: "IX_ProductRating_ProductId",
                table: "ProductRatings",
                newName: "IX_ProductRatings_ProductId");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "ProductRatings",
                type: "uuid",
                nullable: false,
                defaultValueSql: "gen_random_uuid()",
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductRatings",
                table: "ProductRatings",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ProductRatings_Id",
                table: "ProductRatings",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductRatings_ProductId_Id",
                table: "ProductRatings",
                columns: new[] { "ProductId", "Id" });

            migrationBuilder.AddForeignKey(
                name: "FK_ProductRatings_Products_ProductId",
                table: "ProductRatings",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductRatings_Products_ProductId",
                table: "ProductRatings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductRatings",
                table: "ProductRatings");

            migrationBuilder.DropIndex(
                name: "IX_ProductRatings_Id",
                table: "ProductRatings");

            migrationBuilder.DropIndex(
                name: "IX_ProductRatings_ProductId_Id",
                table: "ProductRatings");

            migrationBuilder.RenameTable(
                name: "ProductRatings",
                newName: "ProductRating");

            migrationBuilder.RenameIndex(
                name: "IX_ProductRatings_ProductId",
                table: "ProductRating",
                newName: "IX_ProductRating_ProductId");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "ProductRating",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldDefaultValueSql: "gen_random_uuid()");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductRating",
                table: "ProductRating",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductRating_Products_ProductId",
                table: "ProductRating",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
