using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SavorChef.Backend.Migrations
{
    public partial class MakeEmailUniqueAndAddUsername : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "RecipeEntityUserEntity",
                columns: table => new
                {
                    AssociatedRecipesEntitiesId = table.Column<int>(type: "integer", nullable: false),
                    AssociatedUserEntitiesId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeEntityUserEntity", x => new { x.AssociatedRecipesEntitiesId, x.AssociatedUserEntitiesId });
                    table.ForeignKey(
                        name: "FK_RecipeEntityUserEntity_Recipes_AssociatedRecipesEntitiesId",
                        column: x => x.AssociatedRecipesEntitiesId,
                        principalTable: "Recipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecipeEntityUserEntity_Users_AssociatedUserEntitiesId",
                        column: x => x.AssociatedUserEntitiesId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserName",
                table: "Users",
                column: "UserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RecipeEntityUserEntity_AssociatedUserEntitiesId",
                table: "RecipeEntityUserEntity",
                column: "AssociatedUserEntitiesId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RecipeEntityUserEntity");

            migrationBuilder.DropIndex(
                name: "IX_Users_Email",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_UserName",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Users");
        }
    }
}
