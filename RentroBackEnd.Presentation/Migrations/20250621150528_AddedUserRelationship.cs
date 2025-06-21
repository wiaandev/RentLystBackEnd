using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentOutBackEnd.Presentation.Migrations
{
    /// <inheritdoc />
    public partial class AddedUserRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_PropertyPosts_PropertyPost",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_PropertyPost",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "PropertyPost",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "PropertyPostId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<int>(
                name: "SellerId",
                table: "PropertyPosts",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PropertyPosts_SellerId",
                table: "PropertyPosts",
                column: "SellerId");

            migrationBuilder.AddForeignKey(
                name: "FK_PropertyPosts_AspNetUsers_SellerId",
                table: "PropertyPosts",
                column: "SellerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PropertyPosts_AspNetUsers_SellerId",
                table: "PropertyPosts");

            migrationBuilder.DropIndex(
                name: "IX_PropertyPosts_SellerId",
                table: "PropertyPosts");

            migrationBuilder.DropColumn(
                name: "SellerId",
                table: "PropertyPosts");

            migrationBuilder.AddColumn<int>(
                name: "PropertyPost",
                table: "AspNetUsers",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PropertyPostId",
                table: "AspNetUsers",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_PropertyPost",
                table: "AspNetUsers",
                column: "PropertyPost");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_PropertyPosts_PropertyPost",
                table: "AspNetUsers",
                column: "PropertyPost",
                principalTable: "PropertyPosts",
                principalColumn: "Id");
        }
    }
}
