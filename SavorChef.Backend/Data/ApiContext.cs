namespace SavorChef.Backend.Data;
using Microsoft.EntityFrameworkCore;
using SavorChef.Backend.Models;

public class ApiContext: DbContext
{
    
    public DbSet<Recipe> Recipes { get; set; }

    public ApiContext(DbContextOptions<ApiContext> options)
        : base(options)
    {

    }

}   