using SavorChef.Backend.Data.Entities;

namespace SavorChef.Backend.Data;
using Microsoft.EntityFrameworkCore;

public class ApiContext: DbContext
{
    
    public DbSet<RecipeEntity> Recipes { get; set; }

    public ApiContext(DbContextOptions<ApiContext> options)
        : base(options)
    {

    }

}   