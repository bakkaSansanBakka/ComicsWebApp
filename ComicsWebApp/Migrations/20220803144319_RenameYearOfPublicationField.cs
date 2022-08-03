using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ComicsWebApp.Migrations
{
    public partial class RenameYearOfPublicationField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "YearOfPublisihing",
                table: "Comics",
                newName: "YearOfPublication");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "YearOfPublication",
                table: "Comics",
                newName: "YearOfPublisihing");
        }
    }
}
