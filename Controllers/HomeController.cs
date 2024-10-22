using System.Diagnostics;
using System.Dynamic;
using Microsoft.AspNetCore.Mvc;
using hospital.Models;
using hospital.ViewModel;

namespace hospital.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly AppDbContext _context;

    public HomeController(ILogger<HomeController> logger , AppDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Details(int id)
    {

        var med = _context.Medicines.Where(medicine => medicine.EId == id).FirstOrDefault();
        if (med == null)
        {
            Console.WriteLine("Medicine not found.");
            return NotFound(); // Return 404 or handle the error gracefully.
        }

        DetailsViewModel detailsViewModel = new DetailsViewModel
        {
            Medicine = med,
        };
        return View(detailsViewModel);
    }

    public IActionResult Index()
    {
        return View();
    }
    //-------------
    public IActionResult Medico()
    {
        return View();
    }
    //-------------
    public IActionResult Privacy()
    {
        return View();
    }

    public IActionResult Medicines()
    {
        return View();
    }




    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }


}