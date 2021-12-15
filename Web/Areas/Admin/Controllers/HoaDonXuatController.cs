using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Data;
using Data.Models;
using Microsoft.AspNetCore.Authorization;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class HoaDonXuatController : Controller
    {
        private readonly WebStoreDbContext _context;

        public HoaDonXuatController(WebStoreDbContext context)
        {
            _context = context;
        }

        // GET: Admin/HoaDonXuats
        public async Task<IActionResult> Index()
        {
            var webStoreDbContext = _context.HoaDonXuat.Include(h => h.User);
            return View(await webStoreDbContext.ToListAsync());
        }

        // GET: Admin/HoaDonXuats/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hoaDonXuat = await _context.HoaDonXuat
                .Include(h => h.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (hoaDonXuat == null)
            {
                return NotFound();
            }
            var chiTietHD = await _context.ChiTietHoaDonXuat
                .Include(c => c.ChiTietSP)
                .Include(c => c.ChiTietSP.SanPham)
                .Where(c => c.MaHDXuat == hoaDonXuat.Id).ToListAsync();
            hoaDonXuat.Lines = chiTietHD;

            return View(hoaDonXuat);
        }

        // GET: Admin/HoaDonXuats/Create
        public IActionResult Create()
        {
            ViewData["Status"] = new SelectList(Enum.GetValues(typeof(TrangThaiHoaDon)).Cast<TrangThaiHoaDon>().ToList());
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Name");
            return View();
        }

        // POST: Admin/HoaDonXuats/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,DiaChi,Tinh,Huyen,Xa,Status,TongTien,PhiShip,Note,SoDienThoai,Email,Id")] HoaDonXuat hoaDonXuat)
        {
            if (ModelState.IsValid)
            {
                _context.Add(hoaDonXuat);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Name", hoaDonXuat.UserId);
            return View(hoaDonXuat);
        }

        // GET: Admin/HoaDonXuats/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hoaDonXuat = await _context.HoaDonXuat.FindAsync(id);
            if (hoaDonXuat == null)
            {
                return NotFound();
            }
            ViewData["Status"] = new SelectList(Enum.GetValues(typeof(TrangThaiHoaDon)).Cast<TrangThaiHoaDon>().ToList());
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Name", hoaDonXuat.UserId);
            return View(hoaDonXuat);
        }

        // POST: Admin/HoaDonXuats/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserId,DiaChi,Tinh,Huyen,Xa,Status,TongTien,PhiShip,Note,SoDienThoai,Email,Id")] HoaDonXuat hoaDonXuat)
        {
            if (id != hoaDonXuat.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hoaDonXuat);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HoaDonXuatExists(hoaDonXuat.Id))
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
            ViewData["Status"] = new SelectList(Enum.GetValues(typeof(TrangThaiHoaDon)).Cast<TrangThaiHoaDon>().ToList());
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", hoaDonXuat.UserId);
            return View(hoaDonXuat);
        }

        // GET: Admin/HoaDonXuats/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hoaDonXuat = await _context.HoaDonXuat
                .Include(h => h.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (hoaDonXuat == null)
            {
                return NotFound();
            }

            return View(hoaDonXuat);
        }

        // POST: Admin/HoaDonXuats/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var hoaDonXuat = await _context.HoaDonXuat.FindAsync(id);
            _context.HoaDonXuat.Remove(hoaDonXuat);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HoaDonXuatExists(int id)
        {
            return _context.HoaDonXuat.Any(e => e.Id == id);
        }
    }
}
