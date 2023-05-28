    namespace SavorChef.Backend.Data.Entities;

    public class UserEntity
    {
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public ICollection<RecipeEntity> RecipesEntities { get; set; } = new List<RecipeEntity>();

        public ICollection<RecipeEntity> AssociatedRecipesEntities { get; set; } = new List<RecipeEntity>();
    }