using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentOutBackEnd.Presentation.Migrations
{
    /// <inheritdoc />
    public partial class AddedAddressAndPropertyLink : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_PropertyPosts_PropertyPost",
                table: "Addresses");

            migrationBuilder.DropIndex(
                name: "IX_Addresses_PropertyPost",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "PropertyPost",
                table: "Addresses");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_PropertyPostId",
                table: "Addresses",
                column: "PropertyPostId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_PropertyPosts_PropertyPostId",
                table: "Addresses",
                column: "PropertyPostId",
                principalTable: "PropertyPosts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_PropertyPosts_PropertyPostId",
                table: "Addresses");

            migrationBuilder.DropIndex(
                name: "IX_Addresses_PropertyPostId",
                table: "Addresses");

            migrationBuilder.AddColumn<int>(
                name: "PropertyPost",
                table: "Addresses",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_PropertyPost",
                table: "Addresses",
                column: "PropertyPost");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_PropertyPosts_PropertyPost",
                table: "Addresses",
                column: "PropertyPost",
                principalTable: "PropertyPosts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
