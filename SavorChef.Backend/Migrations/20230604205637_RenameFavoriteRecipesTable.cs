using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SavorChef.Backend.Migrations
{
    public partial class RenameFavoriteRecipesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecipeEntityUserEntity_Recipes_AssociatedRecipesEntitiesId",
                table: "RecipeEntityUserEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_RecipeEntityUserEntity_Users_AssociatedUserEntitiesId",
                table: "RecipeEntityUserEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RecipeEntityUserEntity",
                table: "RecipeEntityUserEntity");

            migrationBuilder.RenameTable(
                name: "RecipeEntityUserEntity",
                newName: "FavoriteRecipes");

            migrationBuilder.RenameColumn(
                name: "AssociatedUserEntitiesId",
                table: "FavoriteRecipes",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "AssociatedRecipesEntitiesId",
                table: "FavoriteRecipes",
                newName: "RecipeId");

            migrationBuilder.RenameIndex(
                name: "IX_RecipeEntityUserEntity_AssociatedUserEntitiesId",
                table: "FavoriteRecipes",
                newName: "IX_FavoriteRecipes_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FavoriteRecipes",
                table: "FavoriteRecipes",
                columns: new[] { "RecipeId", "UserId" });

            migrationBuilder.AddForeignKey(
                name: "FK_FavoriteRecipes_Recipes_RecipeId",
                table: "FavoriteRecipes",
                column: "RecipeId",
                principalTable: "Recipes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FavoriteRecipes_Users_UserId",
                table: "FavoriteRecipes",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FavoriteRecipes_Recipes_RecipeId",
                table: "FavoriteRecipes");

            migrationBuilder.DropForeignKey(
                name: "FK_FavoriteRecipes_Users_UserId",
                table: "FavoriteRecipes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FavoriteRecipes",
                table: "FavoriteRecipes");

            migrationBuilder.RenameTable(
                name: "FavoriteRecipes",
                newName: "RecipeEntityUserEntity");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "RecipeEntityUserEntity",
                newName: "AssociatedUserEntitiesId");

            migrationBuilder.RenameColumn(
                name: "RecipeId",
                table: "RecipeEntityUserEntity",
                newName: "AssociatedRecipesEntitiesId");

            migrationBuilder.RenameIndex(
                name: "IX_FavoriteRecipes_UserId",
                table: "RecipeEntityUserEntity",
                newName: "IX_RecipeEntityUserEntity_AssociatedUserEntitiesId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RecipeEntityUserEntity",
                table: "RecipeEntityUserEntity",
                columns: new[] { "AssociatedRecipesEntitiesId", "AssociatedUserEntitiesId" });

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeEntityUserEntity_Recipes_AssociatedRecipesEntitiesId",
                table: "RecipeEntityUserEntity",
                column: "AssociatedRecipesEntitiesId",
                principalTable: "Recipes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeEntityUserEntity_Users_AssociatedUserEntitiesId",
                table: "RecipeEntityUserEntity",
                column: "AssociatedUserEntitiesId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
