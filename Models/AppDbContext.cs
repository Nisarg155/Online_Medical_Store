using Microsoft.EntityFrameworkCore;

namespace hospital.Models;

public class AppDbContext : DbContext
{
    public AppDbContext()
    {
        
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Server=localhost,1433;Database=Online_Store_Database");
    }
}