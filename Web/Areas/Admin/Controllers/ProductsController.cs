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
    public class ProductsController : Controller
    {
        private readonly WebStoreDbContext _context;
        private readonly FileService _fileService;

        public ProductsController(WebStoreDbContext context, FileService fileService)
        {
            _context = context;
            _fileService = fileService;
        }

        // GET: Admin/Products
        public async Task<IActionResult> Index()
        {
            var webStoreDbContext = _context.SanPham.Include(s => s.Category);
            return View(await webStoreDbContext.ToListAsync());
        }

        // GET: Admin/Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sanPham = await _context.SanPham
                .Include(s => s.Category)
                .Include(s => s.Details)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sanPham == null)
            {
                return NotFound();
            }
            ViewData["MaSP"] = sanPham.Id;
            return View(sanPham);
        }

        // GET: Admin/Products/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.LoaiSP, "Id", "Name");
            return View();
        }

        // POST: Admin/Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,CategoryId,ShowAtHome,OptionName,MainImageFile,MinPrice,MaxPrice,OptionCount,Sold,Description,KeyWord,DefaultDetailId,Id")] SanPham sanPham)
        {
            if (sanPham.MainImageFile != null)
            {
                sanPham.MainImageName = await _fileService.Upload(sanPham.MainImageFile);
            }
            if (ModelState.IsValid)
            {
                _context.Add(sanPham);
                await _context.SaveChangesAsync();
                var loaiSP = await _context.LoaiSP.FirstOrDefaultAsync(lsp => lsp.Id == sanPham.CategoryId);
                if (loaiSP != null)
                {
                    loaiSP.ItemsCount = _context.SanPham.Count(sp => sp.CategoryId == loaiSP.Id);
                    await _context.SaveChangesAsync();
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.LoaiSP, "Id", "Name", sanPham.CategoryId);
            return View(sanPham);
        }

        // GET: Admin/Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sanPham = await _context.SanPham.FindAsync(id);
            if (sanPham == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.LoaiSP, "Id", "Name", sanPham.CategoryId);
            return View(sanPham);
        }

        // POST: Admin/Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,CategoryId,ShowAtHome,OptionName,MainImageFile,MinPrice,MaxPrice,OptionCount,Sold,Description,KeyWord,DefaultDetailId,Id")] SanPham sanPham)
        {
            var old = await _context.SanPham.AsNoTracking().FirstOrDefaultAsync(sp => sp.Id == id);
            sanPham.MainImageName = old.MainImageName;
            if (id != sanPham.Id)
            {
                return NotFound();
            }
            if (sanPham.MainImageFile != null)
            {
                sanPham.MainImageName = await _fileService.Upload(sanPham.MainImageFile);
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sanPham);
                    await _context.SaveChangesAsync();
                    var loaiSP = await _context.LoaiSP.FirstOrDefaultAsync(lsp => lsp.Id == sanPham.CategoryId);
                    if (loaiSP != null)
                    {
                        loaiSP.ItemsCount = _context.SanPham.Count(sp => sp.CategoryId == loaiSP.Id);
                        await _context.SaveChangesAsync();
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SanPhamExists(sanPham.Id))
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
            ViewData["CategoryId"] = new SelectList(_context.LoaiSP, "Id", "Name", sanPham.CategoryId);
            return View(sanPham);
        }

        // GET: Admin/Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sanPham = await _context.SanPham
                .Include(s => s.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sanPham == null)
            {
                return NotFound();
            }

            return View(sanPham);
        }

        // POST: Admin/Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sanPham = await _context.SanPham.FindAsync(id);
            _context.SanPham.Remove(sanPham);
            await _context.SaveChangesAsync();
            var loaiSP = await _context.LoaiSP.FirstOrDefaultAsync(lsp => lsp.Id == sanPham.CategoryId);
            if (loaiSP != null)
            {
                loaiSP.ItemsCount = _context.SanPham.Count(sp => sp.CategoryId == loaiSP.Id);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool SanPhamExists(int id)
        {
            return _context.SanPham.Any(e => e.Id == id);
        }
    }
}
