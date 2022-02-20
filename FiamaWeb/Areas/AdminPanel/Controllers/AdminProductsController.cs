using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Entities;

using DataAccess;
using Services;
using Microsoft.AspNetCore.Authorization;

namespace FiamaWeb.Areas.AdminPanel.Controllers
{
    [Area(nameof(AdminPanel))]
    //[Authorize(Roles = "Admin")]
    public class AdminProductsController : Controller
    {
        private readonly ProductManager _productManager;
        private readonly CategoryManager _categoryManager;
        private readonly IWebHostEnvironment _environment;

        public AdminProductsController(ProductManager productManager, IWebHostEnvironment environment, CategoryManager categoryManager)
        {
            _productManager = productManager;
            _environment = environment;
            _categoryManager = categoryManager;
        }

        // GET: AdminPanel/AdminProducts

        public IActionResult Index()
        {
            return View(_productManager.GetProducts());
        }
        // GET: AdminPanel/AdminProducts/Details/5
        public IActionResult Details(int? id)
        {
            if(id == null)
                return NotFound();
            var selectedProduct=_productManager.GetById(id.Value);
            if (selectedProduct == null)
                return NotFound();
            return View(selectedProduct);
        }
        // GET: AdminPanel/AdminProducts/Create

        public IActionResult Create()
        {
            ViewBag.categoryList=_categoryManager.GetCategories();
            return View();
        }
        // POST: AdminPanel/AdminProducts/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Product product, IFormFile PhotoUrl)
        {
            ViewBag.categoryList = _categoryManager.GetCategories();
            if (ModelState.IsValid)
            {
                string fileName=Guid.NewGuid()+PhotoUrl.FileName;  
                string rootFile=Path.Combine(_environment.WebRootPath, "uploads");
                string mainFile = Path.Combine(rootFile, fileName);
                using FileStream stream = new(mainFile, FileMode.Create);
                PhotoUrl.CopyTo(stream);
                product.PhotoUrl = "/uploads/" + fileName;
                product.PublishDate = DateTime.Now;
                _productManager.Add(product);
                return RedirectToAction("Index");
            }
            return View(product);
        }
        // GET: AdminPanel/AdminProducts/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
                return NotFound();
            var selectedProduct=_productManager.GetById(id);
            if (selectedProduct == null)
                return NotFound();
            ViewBag.categoryList = _categoryManager.GetCategories();
            return View(selectedProduct);
        }
        // POST: AdminPanel/AdminProducts/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Name,Description,Price,InStock,Discount,PhotoUrl,PublishDate,ModifiedOn,SKU,Barcode,CategoryId,IsSlider,ID")] Product product, IFormFile PhotoUrl)
        {
            if (id != product.ID)
                return NotFound();
            if (ModelState.IsValid)
            {
                try
                {
                    if (PhotoUrl != null)
                    {
                        string fileName = Guid.NewGuid() + PhotoUrl.FileName;
                        string rootFile = Path.Combine(_environment.WebRootPath, "uploads");
                        string mainFile = Path.Combine(rootFile, fileName);
                        using FileStream str = new(mainFile, FileMode.Create);
                        PhotoUrl.CopyTo(str);
                        product.PhotoUrl = "/uploads/" + fileName;
                    }

                    product.ModifiedOn = DateTime.Now;
                    _productManager.Update(product);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_productManager.ProductExists(product.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewBag.categoryList = _categoryManager.GetCategories();
            return View(product);
        }

        // GET: AdminPanel/AdminProducts/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
                return NotFound();
            var selectedProduct = _productManager.GetById(id);
            if (selectedProduct == null)
                return NotFound();

            return View(selectedProduct);
        }

        // POST: AdminPanel/AdminProducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var selectedProduct = _productManager.GetById(id);
            if (selectedProduct == null) return NotFound();
            _productManager.Delete(selectedProduct);
            return RedirectToAction(nameof(Index));
        }



    }
}
