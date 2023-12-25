using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LapZone.Models;

namespace LapZone.Controllers
{
    public class Products1Controller : Controller
    {
        private readonly LapZoneContext _context;

        public Products1Controller(LapZoneContext context)
        {
            _context = context;
        }

        // GET: Products1
        //public async Task<IActionResult> Index()
        //{
        //    var lapZoneContext = _context.Products.Include(p => p.Category);
        //    return View(await lapZoneContext.ToListAsync());
        //}


        public IActionResult Index(string searchTerm)
        {
            // Retrieve all products initially (just an example, modify as needed)
            var products = _context.Products.ToList();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                // Filter products based on the search term
                products = products.Where(p =>
                    p.ProductName.Contains(searchTerm) ||
                    p.Description.Contains(searchTerm)
                // Add other properties you want to search here
                ).ToList();
            }

            // Pass the searchTerm to the view using ViewData or ViewBag
            ViewData["SearchTerm"] = searchTerm;

            return View(products);
        }


        private void UploadImage(Product model)
        {
            // here i get the file that has been sent
            var file = HttpContext.Request.Form.Files;

            if (file.Count() > 0)//check if file has data or image
            {
                //upload image and save it in specific folder
                string ImageName = Guid.NewGuid().ToString() + Path.GetExtension(file[0].FileName);//this make unique name for each image

                var fileStream = new FileStream(Path.Combine(@"wwwroot/", "Images1", ImageName), FileMode.Create);
                file[0].CopyTo(fileStream);

                //save image in db 
                model.ImagePath = ImageName;

            }
            else if (model.ImagePath == null && model.ProductId == null)
            {
                //not upload image and create new product
                model.ImagePath = "DefaultImage.png";
            }
            else
            {
                model.ImagePath = model.ImagePath;
            }
        }

        // GET: Products1/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products1/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName");
            return View();

        }

        // POST: Products1/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,ProductName,Description,Price,CategoryId,StockQuantity,ImagePath,Brand,CPU,Storage,RAM,GPU,ScreenSize")] Product product)
        {
            if (ModelState.IsValid)
            {
                UploadImage(product);
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName", product.CategoryId);
            return View(product);
        }

        private void UploadImage(Product model, IFormFile newImage)
        {
            if (newImage != null)
            {
                // Upload the new image and save it in the specific folder
                string imageName = Guid.NewGuid().ToString() + Path.GetExtension(newImage.FileName);
                var fileStream = new FileStream(Path.Combine("wwwroot", "Images1", imageName), FileMode.Create);
                newImage.CopyTo(fileStream);

                // Save the new image path in the model
                model.ImagePath = imageName;
            }
        }


        // GET: Products1/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName", product.CategoryId);
            return View(product);
        }

        // POST: Products1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,ProductName,Description,Price,CategoryId,StockQuantity,ImagePath,Brand,CPU,Storage,RAM,GPU,ScreenSize")] Product product, IFormFile? clientFile)
        {
            if (id != product.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Call the method to handle image upload
                    UploadImage(product);
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.ProductId))
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
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName", product.CategoryId);
            return View(product);
        }

        // GET: Products1/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Products == null)
            {
                return Problem("Entity set 'LapZoneContext.Products'  is null.");
            }
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return (_context.Products?.Any(e => e.ProductId == id)).GetValueOrDefault();
        }
    }
}