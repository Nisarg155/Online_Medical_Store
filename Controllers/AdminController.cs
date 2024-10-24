using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using hospital.Models;
using hospital.ViewModel;
using hospital.Areas.Identity.Data;

namespace hospital.Controllers
{
    
    public class AdminController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly UserManager<hospitalUser> _userManager;

        public AdminController(AppDbContext context, IWebHostEnvironment hostEnvironment, UserManager<hospitalUser> userManager)
        {
            _context = context;
            _userManager = userManager;
            webHostEnvironment = hostEnvironment;
        }

        // GET: Admin
        public IActionResult Index()
        {
            // return View(await _context.Medicines.ToListAsync());
            return View();
        }
        public IActionResult dashboard()
        {
            var users = _userManager.Users;
            return View(users);
        }
        public IActionResult Addmedicine()
        {
            return View();
        }
        public IActionResult show()
        {
            var users = _userManager.Users;
            return View(users);
        }
        public IActionResult showmedicine()
        {
            return View();
        }
        public IActionResult editmedicine(int id)
        {
            var edit = _context.Medicines.Where(x => x.EId == id).FirstOrDefault();
            return View(edit);
        }
        [HttpPost]
        public IActionResult editmedicine(medicine edit)
        {
            var itemEdit = _context.Medicines.Where(x => x.EId == edit.EId).FirstOrDefault();
            itemEdit.Eprize = edit.Eprize;
            itemEdit.Ename = edit.Ename;
            itemEdit.Eavailability = edit.Eavailability;
            _context.Entry(itemEdit).State = EntityState.Modified;
            _context.SaveChanges();
            return RedirectToAction("showmedicine");
        }
        public IActionResult Deletemedicine(int id)
        {

            var del = _context.Medicines.Find(id);
            _context.Medicines.Remove(del);
            _context.SaveChanges();
            return RedirectToAction("showmedicine");
        }

        public IActionResult FileUpload(imgviewmodel imv)
        {
            string stringFileName = UploadFile(imv);

            var drug = new medicine
            {
                Ename = imv.Ename,
                Eprize = imv.Eprize,
                ImageUrl3 = stringFileName,
                Eavailability = imv.Eavailability,
                Edescription = "This is for humans",
                Edetails = "This is best medicine for health"

            };
            _context.Medicines.Add(drug);
            _context.SaveChanges();
            return RedirectToAction("dashboard", "Admin");
        }


        public string UploadFile(imgviewmodel imv)
        {
            string fileName = null;
            if (imv.ImageUrl3 != null)
            {
                string uploadDir = Path.Combine(webHostEnvironment.WebRootPath, "Images");
                fileName = Guid.NewGuid().ToString() + "-" + imv.ImageUrl3.FileName;
                string filePath = Path.Combine(uploadDir, fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    imv.ImageUrl3.CopyTo(fileStream);
                }
            }
            return fileName;
        }
        [HttpPost]
        public async Task<IActionResult> userdelete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {id} not found";
                return View("NotFound");
            }
            else
            {
                var result = await _userManager.DeleteAsync(user);

                if (result.Succeeded)
                {
                    return RedirectToAction("show");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return View("show");
            }
        }


        // ---------------------------------

        private bool medicineExists(int id)
        {
            return _context.Medicines.Any(e => e.EId == id);
        }
    }
}
