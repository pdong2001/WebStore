using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Data;
using Data.Models;
using Web.Areas.Admin.Models;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ChiTietHDXuatsController : Controller
    {
        private readonly WebStoreDbContext _context;

        public ChiTietHDXuatsController(WebStoreDbContext context)
        {
            _context = context;
        }

        // GET: Admin/ChiTietHDXuats
        public async Task<IActionResult> Index()
        {
            var webStoreDbContext = _context.ChiTietHoaDonXuat.Include(c => c.ChiTietSP);
            return View(await webStoreDbContext.ToListAsync());
        }

        // GET: Admin/ChiTietHDXuats/Details/5
        public async Task<IActionResult> Details(ChiTietHDXLookUpDto id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chiTietHDXuat = await _context.ChiTietHoaDonXuat
                .Include(c => c.ChiTietSP)
                .FirstOrDefaultAsync(m => m.MaHDXuat == id.MaHD && m.MaChiTietSP == id.MaSP);
            if (chiTietHDXuat == null)
            {
                return NotFound();
            }

            return View(chiTietHDXuat);
        }

        // GET: Admin/ChiTietHDXuats/Create
        public IActionResult Create()
        {
            ViewData["MaChiTietSP"] = new SelectList(_context.ChiTietSP, "Id", "Name");
            return View();
        }

        // POST: Admin/ChiTietHDXuats/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaHDXuat,MaChiTietSP,SoLuong,Gia")] ChiTietHDXuat chiTietHDXuat)
        {
            if (ModelState.IsValid)
            {
                _context.Add(chiTietHDXuat);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaChiTietSP"] = new SelectList(_context.ChiTietSP, "Id", "Name", chiTietHDXuat.MaChiTietSP);
            return View(chiTietHDXuat);
        }

        // GET: Admin/ChiTietHDXuats/Edit/5
        public async Task<IActionResult> Edit(ChiTietHDXLookUpDto id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chiTietHDXuat = await _context.ChiTietHoaDonXuat
                .FirstOrDefaultAsync(m => m.MaHDXuat == id.MaHD && m.MaChiTietSP == id.MaSP);
            if (chiTietHDXuat == null)
            {
                return NotFound();
            }
            ViewData["MaChiTietSP"] = new SelectList(_context.ChiTietSP, "Id", "Name", chiTietHDXuat.MaChiTietSP);
            return View(chiTietHDXuat);
        }

        // POST: Admin/ChiTietHDXuats/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ChiTietHDXLookUpDto id, [Bind("MaHDXuat,MaChiTietSP,SoLuong,Gia")] ChiTietHDXuat chiTietHDXuat)
        {
            if (!_context.ChiTietHoaDonXuat.Any(m => m.MaHDXuat == id.MaHD && m.MaChiTietSP == id.MaSP))
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(chiTietHDXuat);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;

                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaChiTietSP"] = new SelectList(_context.ChiTietSP, "Id", "Name", chiTietHDXuat.MaChiTietSP);
            return View(chiTietHDXuat);
        }

        // GET: Admin/ChiTietHDXuats/Delete/5
        public async Task<IActionResult> Delete(ChiTietHDXLookUpDto id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chiTietHDXuat = await _context.ChiTietHoaDonXuat
                .Include(c => c.ChiTietSP)
                .FirstOrDefaultAsync(m => m.MaHDXuat == id.MaHD && m.MaChiTietSP == id.MaSP);
            if (chiTietHDXuat == null)
            {
                return NotFound();
            }

            return View(chiTietHDXuat);
        }

        // POST: Admin/ChiTietHDXuats/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(ChiTietHDXLookUpDto id)
        {
            var chiTietHDXuat = await _context.ChiTietHoaDonXuat
                .FirstOrDefaultAsync(m => m.MaHDXuat == id.MaHD && m.MaChiTietSP == id.MaSP); ;
            _context.ChiTietHoaDonXuat.Remove(chiTietHDXuat);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ChiTietHDXuatExists(ChiTietHDXLookUpDto id)
        {
            return _context.ChiTietHoaDonXuat.Any(m => m.MaHDXuat == id.MaHD && m.MaChiTietSP == id.MaSP);
        }
    }
}
