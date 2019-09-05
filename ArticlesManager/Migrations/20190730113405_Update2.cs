using Microsoft.EntityFrameworkCore.Migrations;

namespace ArticlesManager.Migrations
{
    public partial class Update2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PublisherName",
                table: "Publishers",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Publishers_PublisherName",
                table: "Publishers",
                column: "PublisherName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Publishers_PublisherName",
                table: "Publishers");

            migrationBuilder.AlterColumn<string>(
                name: "PublisherName",
                table: "Publishers",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
