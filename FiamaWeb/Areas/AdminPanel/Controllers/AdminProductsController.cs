using DataAccess;
using Entities;
using Microsoft.AspNetCore.Mvc;

namespace FiamaWeb.Areas.AdminPanel.Controllers
{
    [Area(nameof(AdminPanel))]
    public class AdminProductsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public AdminProductsController(ApplicationDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        public IActionResult Index()
        {
            return View(_context.Products.ToList());
        }
        public IActionResult Create()
        {
            ViewBag.CategoryList=_context.Categories.ToList();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Product product,IFormFile PhotoUrl)
        {
            string fileName=Guid.NewGuid()+PhotoUrl.FileName;
            string rootFile = Path.Combine(_environment.WebRootPath, "uploads");
            string mainFile=Path.Combine(rootFile,fileName);
            using FileStream stream = new(mainFile,FileMode.Create);
            PhotoUrl.CopyTo(stream);

            Product newProduct = new()
            {
           Name = product.Name,
           Description = product.Description,
           Price = product.Price,
           InStock = product.InStock,
           PhotoUrl ="/uploads/"+fileName,
           CategoryId = product.CategoryId 

            };
            _context.Products.Add(newProduct);  
            _context.SaveChanges();
            return RedirectToAction("Index"); 
        }
        public IActionResult Edit(int? id)
        {
            if (id == null) return NotFound();
      Product selectedProduct = _context.Products.FirstOrDefault(p =>p.ID == id);
            if (selectedProduct == null) return NotFound();
            ViewBag.CategoryList = _context.Categories.ToList();
            return View(selectedProduct);
            
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int? id,Product oldproduct,IFormFile NewPhoto)
        {
            if (NewPhoto!=null)
            {
                string fileName = Guid.NewGuid() + NewPhoto.FileName;
                string rootFile = Path.Combine(_environment.WebRootPath, "uploads");
                string mainFile = Path.Combine(rootFile, fileName);
                using FileStream stream = new(mainFile, FileMode.Create);
                NewPhoto.CopyTo(stream);
                oldproduct.PhotoUrl = "/uploads/" + fileName;
            }
            _context.Update(oldproduct);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
