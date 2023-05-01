using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace SavorChef.Backend.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductEntityId",
                table: "Recipes",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_ProductEntityId",
                table: "Recipes",
                column: "ProductEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Recipes_Products_ProductEntityId",
                table: "Recipes",
                column: "ProductEntityId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recipes_Products_ProductEntityId",
                table: "Recipes");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Recipes_ProductEntityId",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "ProductEntityId",
                table: "Recipes");
        }
    }
}
