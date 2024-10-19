using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

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
public class medicine
{
    [Key]
    public int EId { get; set; }

    public string Ename { get; set; }

    public string Eprize { get; set; }

    public string ImageUrl3 { get; set; }
    public Nullable<int> flag { get; set; }
    public string Eavailability { get; set; }
    public string Edescription { get; set; }
    public string Edetails { get; set; }
}
public class cart
{
    [Key]
    public int Cid { get; set; }
    public string name { get; set; }
    public string image { get; set; }
    public Nullable<int> qty { get; set; }
    public Nullable<int> price { get; set; }
    public Nullable<int> bill { get; set; }
    public string Cemail { get; set; }
}