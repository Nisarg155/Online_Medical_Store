using hospital.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace hospital.Models;

public class AppDbContext : IdentityDbContext<hospitalUser>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }

    public DbSet<medicine> Medicines { get; set; }
    public DbSet<cart> Carts { get; set; }
   /* protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Server=localhost,1433;Database=Online_Store_Database");
    }*/
}

