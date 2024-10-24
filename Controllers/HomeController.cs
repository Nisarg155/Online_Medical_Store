using System.Diagnostics;
using System.Dynamic;
using hospital.Areas.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using hospital.Models;
using hospital.ViewModel;
using Microsoft.AspNetCore.Authorization;

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
    [HttpPost]
    [HttpGet]
    public IActionResult AddtoCart(int id)
    {
        string email = User.Identity?.Name;
        if (email != null)
        {
            medicine med = _context.Medicines.Where(medicine =>  medicine.EId == id).FirstOrDefault();
            cart c = new cart();
            c.Cemail = email;
            c.name = med.Ename;
            c.price = Convert.ToInt32(med.Eprize);
            c.image = med.ImageUrl3;
            c.bill = c.price;
            c.qty = 1;

            _context.Carts.Add(c);
            _context.SaveChanges();

        }
        else
        {
            TempData["error"] = "You are not logged in!";
        }

        return RedirectToAction("Cart");

    }

    [HttpGet]
    [HttpPost]
    [Authorize]
    public IActionResult Cart()
    {
        string email = User.Identity?.Name;

        // Get the list of items in the cart for the logged-in user
        var list = _context.Carts.Where(medicine => medicine.Cemail == email).ToList();

        // Calculate the total bill for the cart items
        var total = list.Sum(item => item.bill);

        // Use ViewBag to pass the total to the view
        ViewBag.Total = total;

        return View(list);
    }

    [ValidateAntiForgeryToken]
    [HttpPost]
    public ActionResult Quantity_Change(int id,int quantity)
    {
        Console.WriteLine(id);
        Console.WriteLine(quantity);

        var item = _context.Carts.Find(id);

        if (item != null)
        {
            item.qty = quantity;
            item.bill = item.price * item.qty; // Recalculate the bill
            _context.SaveChanges();
        }
        return RedirectToAction("Cart");
    }

  
    public ActionResult Delete(int id)
    {
        var item  = _context.Carts.Find(id);
        _context.Carts.Remove(item);
        _context.SaveChanges();
        return RedirectToAction("Cart");
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