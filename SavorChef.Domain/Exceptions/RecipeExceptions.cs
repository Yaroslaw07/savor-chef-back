namespace SavorChef.Domain.Exceptions;

public class RecipeExceptions
{
    /// <summary>
    /// Errors related to recipe not being found
    /// </summary>
    public static class NotFound
    {
        public static DomainException ById(int id, Exception? e = null) =>
            new(
                "RECIPE_NOT_FOUND",
                $"Recipe with id {id} not found",
                e);
    }
    
    /// <summary>
    /// Errors related to multiple recipes not being found
    /// </summary>
    public static class NotFoundMultiple
    {
        public static DomainException ByIds(IEnumerable<int> ids, Exception? e = null) =>
            new(
                "RECIPES_NOT_FOUND",
                $"Recipes with ids {string.Join(", ", ids)} not found",
                e);
    }

    /// <summary>
    /// Errors related to recipe update operations
    /// </summary>
    public static class Update
    {
        public static DomainException Failed(int id, Exception? e = null) =>
            new(
                "RECIPE_UPDATE_FAILED",
                $"Failed to update recipe with id {id}",
                e);
    }
    
    /// <summary>
    /// Errors related to recipe creation operations
    /// </summary>
    public static class Create
    {
        public static DomainException Failed(Exception? e = null) =>
            new(
                "RECIPE_CREATION_FAILED",
                "Failed to create recipe",
                e);
    }
    
    /// <summary>
    /// Errors related to recipe deletion operations
    /// </summary>
    public static class Delete
    {
        public static DomainException Failed(Exception? e = null) =>
            new(
                "RECIPE_DELETION_FAILED",
                "Failed to delete recipe",
                e);
    }
}