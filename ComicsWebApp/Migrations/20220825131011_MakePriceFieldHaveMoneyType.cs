using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ComicsWebApp.Migrations
{
    public partial class MakePriceFieldHaveMoneyType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Comics",
                type: "money",
                maxLength: 8,
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float",
                oldMaxLength: 8);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Price",
                table: "Comics",
                type: "float",
                maxLength: 8,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "money",
                oldMaxLength: 8);
        }
    }
}
