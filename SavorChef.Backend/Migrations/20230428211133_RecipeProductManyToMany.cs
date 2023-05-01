using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SavorChef.Backend.Migrations
{
    public partial class RecipeProductManyToMany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recipes_Products_ProductEntityId",
                table: "Recipes");

            migrationBuilder.DropIndex(
                name: "IX_Recipes_ProductEntityId",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "ProductEntityId",
                table: "Recipes");

            migrationBuilder.CreateTable(
                name: "ProductEntityRecipeEntity",
                columns: table => new
                {
                    AssociatedProductsId = table.Column<int>(type: "integer", nullable: false),
                    AssociatedRecipesId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductEntityRecipeEntity", x => new { x.AssociatedProductsId, x.AssociatedRecipesId });
                    table.ForeignKey(
                        name: "FK_ProductEntityRecipeEntity_Products_AssociatedProductsId",
                        column: x => x.AssociatedProductsId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductEntityRecipeEntity_Recipes_AssociatedRecipesId",
                        column: x => x.AssociatedRecipesId,
                        principalTable: "Recipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductEntityRecipeEntity_AssociatedRecipesId",
                table: "ProductEntityRecipeEntity",
                column: "AssociatedRecipesId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductEntityRecipeEntity");

            migrationBuilder.AddColumn<int>(
                name: "ProductEntityId",
                table: "Recipes",
                type: "integer",
                nullable: false,
                defaultValue: 0);

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
    }
}
