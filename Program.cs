using hospital.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using hospital.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

var connectionString = builder.Configuration.GetConnectionString("hospitalContextConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

//builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
//  .AddEntityFrameworkStores<AppDbContext>();
//builder.Services.AddIdentity<hospitalUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false)
  //  .AddEntityFrameworkStores<AppDbContext>()
    //.AddDefaultTokenProviders();

// Add identity services and configure it to use hospitalUser
//builder.Services.AddDefaultIdentity<hospitalUser>(options => options.SignIn.RequireConfirmedAccount = false)
  //  .AddRoles<IdentityRole>() // If you're using roles, add this line
    //.AddEntityFrameworkStores<AppDbContext>(); // Make sure this uses the correct DbContext
// This registers Identity with your custom user class (hospitalUser)

builder.Services.AddDefaultIdentity<hospitalUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<AppDbContext>();

builder.Services.AddScoped<SignInManager<hospitalUser>>();
builder.Services.AddScoped<UserManager<hospitalUser>>();

// Add controllers with views
builder.Services.AddControllersWithViews();

// Add Razor Pages
builder.Services.AddRazorPages();

// Build the application after all services are added
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=medico}/{id?}");

app.MapRazorPages(); // Ensure this is still present

app.Run();