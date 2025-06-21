using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentOutBackEnd.Presentation.Migrations
{
    /// <inheritdoc />
    public partial class AddedPropertyToUserEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsRenter",
                table: "AspNetUsers",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsRenter",
                table: "AspNetUsers");
        }
    }
}
