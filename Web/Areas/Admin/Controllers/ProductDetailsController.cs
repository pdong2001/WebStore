using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Data;
using Data.Models;
using Services;
using Microsoft.AspNetCore.Authorization;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ProductDetailsController : Controller
    {
        private readonly WebStoreDbContext _context;
        private readonly FileService _fileService;

        public ProductDetailsController(WebStoreDbContext context, FileService fileService)
        {
            _context = context;
            _fileService = fileService;
        }

        // GET: Admin/ProductDetails
        public async Task<IActionResult> Index()
        {
            var webStoreDbContext = _context.ChiTietSP.Include(c => c.SanPham);
            return View(await webStoreDbContext.ToListAsync());
        }

        // GET: Admin/ProductDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chiTietSP = await _context.ChiTietSP
                .Include(c => c.SanPham)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (chiTietSP == null)
            {
                return NotFound();
            }

            return View(chiTietSP);
        }

        // GET: Admin/ProductDetails/Create
        public IActionResult Create(int id)
        {
            if (_context.SanPham.Any(sp => sp.Id == id))
            {
                ViewData["ItemId"] = new SelectList(_context.SanPham.Where(sp => sp.Id == id), "Id", "Name");
                ViewData["MaSP"] = id;
            }
            else
            {
                return NotFound();
            }
            return View();
        }

        // POST: Admin/ProductDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ItemId,Quantity,Unit,Price,Name,Note,ImageFile")] ChiTietSP chiTietSP)
        {
            if (chiTietSP.ImageFile != null)
            {
                chiTietSP.ImageName = await _fileService.Upload(chiTietSP.ImageFile);
            }

            if (ModelState.IsValid)
            {
                _context.Add(chiTietSP);
                await _context.SaveChangesAsync();
                var sanPham = _context.SanPham.FirstOrDefault(sp => sp.Id == chiTietSP.ItemId);
                var priceQuery = from detail in _context.ChiTietSP
                                 where detail.ItemId == chiTietSP.ItemId
                                 select detail.Price;
                sanPham.Quantity += chiTietSP.Quantity;
                var price = priceQuery.ToList();
                sanPham.OptionCount++;
                sanPham.MinPrice = 0;
                price.GetMinMax((min, max) =>
                {
                    sanPham.MinPrice = min;
                    sanPham.MaxPrice = max;
                });
                sanPham.DefaultDetailId = _context.ChiTietSP.FirstOrDefault(sp => sp.Price == sanPham.MinPrice && sp.ItemId == sanPham.Id)?.Id;
                await _context.SaveChangesAsync();
                return Redirect(Url.Action(nameof(ProductsController.Details), "Products", new { id = chiTietSP.ItemId }));
            }
            ViewData["ItemId"] = new SelectList(_context.SanPham, "Id", "Name", chiTietSP.ItemId);
            return View(chiTietSP);
        }

        // GET: Admin/ProductDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chiTietSP = await _context.ChiTietSP.FindAsync(id);
            if (chiTietSP == null)
            {
                return NotFound();
            }
            ViewData["ItemId"] = new SelectList(_context.SanPham, "Id", "Name", chiTietSP.ItemId);
            return View(chiTietSP);
        }

        // POST: Admin/ProductDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ItemId,Quantity,Unit,AverateReceiptPrice,Sold,Price,Name,Note,ImageFile,Id")] ChiTietSP chiTietSP)
        {
            if (id != chiTietSP.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var old = _context.ChiTietSP.AsNoTracking().SingleOrDefault(ct => ct.Id == id);
                    chiTietSP.ImageName = old.ImageName;
                    var sanPham = _context.SanPham.FirstOrDefault(sp => sp.Id == chiTietSP.ItemId);
                    sanPham.Quantity -= old.Quantity;
                    sanPham.Quantity += chiTietSP.Quantity;
                    chiTietSP.SanPham = sanPham;
                    _context.Update(chiTietSP);
                    await _context.SaveChangesAsync();
                    var priceQuery = from detail in _context.ChiTietSP
                                     where detail.ItemId == chiTietSP.ItemId
                                     select detail.Price;
                    var price = priceQuery.ToList();
                    sanPham.MinPrice = 0;
                    price.GetMinMax((min, max) =>
                    {
                        sanPham.MinPrice = min;
                        sanPham.MaxPrice = max;
                    });
                    sanPham.DefaultDetailId = _context.ChiTietSP.FirstOrDefault(sp => sp.Price == sanPham.MinPrice && sp.ItemId == sanPham.Id)?.Id;
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ChiTietSPExists(chiTietSP.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return Redirect(Url.Action(nameof(ProductsController.Details), "Products", new { id = chiTietSP.ItemId }));
            }
            ViewData["ItemId"] = new SelectList(_context.SanPham, "Id", "Name", chiTietSP.ItemId);
            return View(chiTietSP);
        }

        // GET: Admin/ProductDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chiTietSP = await _context.ChiTietSP
                .Include(c => c.SanPham)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (chiTietSP == null)
            {
                return NotFound();
            }

            return View(chiTietSP);
        }

        // POST: Admin/ProductDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var chiTietSP = await _context.ChiTietSP.FindAsync(id);
            _context.ChiTietSP.Remove(chiTietSP);
            await _context.SaveChangesAsync();
            var sanPham = _context.SanPham.FirstOrDefault(sp => sp.Id == chiTietSP.ItemId);
            var priceQuery = from detail in _context.ChiTietSP
                             where detail.ItemId == chiTietSP.ItemId
                             select detail.Price;
            var price = priceQuery.ToList();
            sanPham.MinPrice = 0;
            sanPham.OptionCount--;
            price.GetMinMax((min, max) =>
            {
                sanPham.MinPrice = min;
                sanPham.MaxPrice = max;
            });
            sanPham.Quantity -= chiTietSP.Quantity;
            sanPham.DefaultDetailId = _context.ChiTietSP.FirstOrDefault(sp => sp.Price == sanPham.MinPrice && sp.ItemId == sanPham.Id)?.Id;
            await _context.SaveChangesAsync();
            return Redirect(Url.Action(nameof(ProductsController.Details), "Products", new { id = chiTietSP.ItemId }));
        }

        private bool ChiTietSPExists(int id)
        {
            return _context.ChiTietSP.Any(e => e.Id == id);
        }
    }
}
