using Microsoft.EntityFrameworkCore;


namespace hospital.Models;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }

    public DbSet<medicine> medicines { get; set; }
    public DbSet<cart> carts { get; set; }
   /* protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Server=localhost,1433;Database=Online_Store_Database");
    }*/
}

