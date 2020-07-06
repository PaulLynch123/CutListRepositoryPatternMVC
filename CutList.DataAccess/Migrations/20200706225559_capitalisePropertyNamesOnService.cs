using Microsoft.EntityFrameworkCore.Migrations;

namespace CutList.DataAccess.Migrations
{
    public partial class capitalisePropertyNamesOnService : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "price",
                table: "Service",
                newName: "Price");

            migrationBuilder.RenameColumn(
                name: "longDescription",
                table: "Service",
                newName: "LongDescription");

            migrationBuilder.RenameColumn(
                name: "imageUrl",
                table: "Service",
                newName: "ImageUrl");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Service",
                newName: "price");

            migrationBuilder.RenameColumn(
                name: "LongDescription",
                table: "Service",
                newName: "longDescription");

            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "Service",
                newName: "imageUrl");
        }
    }
}
