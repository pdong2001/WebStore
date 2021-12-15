using Data;
using Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Web.Areas.Home.Models;

namespace Web.Areas.Home.Controllers
{
    [Area("Home")]
    public class HomeController : Controller
    {
        private readonly WebStoreDbContext _context;

        public HomeController(WebStoreDbContext context)
        {
            _context = context;
        }

        // GET: HomeController
        public ActionResult Index()
        {
            if (!_context.WebInfos.Any(wi => wi.IsActivated))
            {
                return Redirect(Url.Action("Index", "Home", new { area = "Admin" }));
            }
            if (User == null || User.Identity == null ||
                !User.Identity.IsAuthenticated)
            {
                ViewData["CartCount"] = 0;
                ViewData["Total"] = string.Format("{0:n0}đ", 0);
            }
            else
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                var data = _context.UserItems.Where(ui => ui.UserId == userId)
                    .Include(ui => ui.ChiTietSP)
                    .Include(ui => ui.ChiTietSP.SanPham).ToList();
                ViewData["CartItems"] = data;
                var dsGioHang = _context.UserItems.Where(ui => ui.UserId == userId).Include(ui => ui.ChiTietSP).ToList();
                var tongTien = 0.0;
                dsGioHang.ForEach(sp => tongTien += sp.ChiTietSP.Price * sp.SoLuong);
                ViewData["CartCount"] = _context.UserItems.Where(ui => ui.UserId == userId).Count();
                ViewData["Total"] = string.Format("{0:n0}đ", tongTien);
            }
            //Truyền thông tin trang chủ vào View
            ViewData["News"] = _context.SanPham.AsNoTracking().Where(sp => sp.OptionCount > 0).OrderByDescending(sp => sp.Id).Take(4).ToList();
            var thongTin = _context.WebInfos.FirstOrDefault(wi => wi.IsActivated);
            ViewData["Data"] = thongTin;
            var dsLoaiSP = _context.LoaiSP.Where(lsp => lsp.IsActivated);
            ViewData["DSLSP"] = dsLoaiSP.ToList();
            return View(thongTin);
        }

        // GET: HomeController/Details/5
        public ActionResult Details(int id)
        {
            if (!_context.WebInfos.Any(wi => wi.IsActivated))
            {
                return Redirect(Url.Action("Index", "Home", new { area = "Admin" }));
            }
            var dsLoaiSP = _context.LoaiSP.Where(lsp => lsp.IsActivated);
            ViewData["DSLSP"] = dsLoaiSP.ToList();
            var thongTin = _context.WebInfos.FirstOrDefault(wi => wi.IsActivated);
            ViewData["Data"] = thongTin;
            return View();
        }

        [Authorize]
        public ActionResult Cart()
        {
            if (!_context.WebInfos.Any(wi => wi.IsActivated))
            {
                return Redirect(Url.Action("Index", "Home", new { area = "Admin" }));
            }
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var data = _context.UserItems.Where(ui => ui.UserId == userId)
                .Include(ui => ui.ChiTietSP)
                .Include(ui => ui.ChiTietSP.SanPham).ToList();
            ViewData["CartItems"] = data;
            var dsGioHang = _context.UserItems.Where(ui => ui.UserId == userId).Include(ui => ui.ChiTietSP).ToList();
            var tongTien = 0.0;
            dsGioHang.ForEach(sp => tongTien += sp.ChiTietSP.Price * sp.SoLuong);
            ViewData["Total"] = string.Format("{0:n0}đ", tongTien);
            ViewData["Data"] = _context.WebInfos.FirstOrDefault(wi => wi.IsActivated);
            var dsLoaiSP = _context.LoaiSP.Where(lsp => lsp.IsActivated);
            ViewData["DSLSP"] = dsLoaiSP.ToList();
            return View();
        }

        [HttpGet]
        public ActionResult Products()
        {
            if (!_context.WebInfos.Any(wi => wi.IsActivated))
            {
                return Redirect(Url.Action("Index", "Home", new { area = "Admin" }));
            }
            int id = 0, min = 0, max = 0, page = 1;
            List<string> Key = new();
            if (HttpContext.Request.Query.ContainsKey("categoryid")) int.TryParse(HttpContext.Request.Query["categoryid"], out id);
            if (HttpContext.Request.Query.ContainsKey("min")) int.TryParse(HttpContext.Request.Query["min"], out min);
            if (HttpContext.Request.Query.ContainsKey("max")) int.TryParse(HttpContext.Request.Query["max"], out max);
            if (HttpContext.Request.Query.ContainsKey("page")) int.TryParse(HttpContext.Request.Query["page"], out page);
            if (HttpContext.Request.Query.ContainsKey("search")) Key.AddRange(HttpContext.Request.Query["search"].ToString().Trim().Split(',',';',' ','-', '\\','/'));

            Key = Key.Select(k => k.RemoveVietnameseTone().ToLower()).ToList();

            if (User == null || User.Identity == null ||
                !User.Identity.IsAuthenticated)
            {
                ViewData["CartCount"] = 0;
                ViewData["Total"] = string.Format("{0:n0}đ", 0);
            }
            else
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                var data = _context.UserItems.Where(ui => ui.UserId == userId)
                    .Include(ui => ui.ChiTietSP)
                    .Include(ui => ui.ChiTietSP.SanPham).ToList();
                ViewData["CartItems"] = data;
                var dsGioHang = _context.UserItems.Where(ui => ui.UserId == userId).Include(ui => ui.ChiTietSP).ToList();
                var tongTien = 0.0;
                dsGioHang.ForEach(sp => tongTien += sp.ChiTietSP.Price * sp.SoLuong);
                ViewData["CartCount"] = _context.UserItems.Where(ui => ui.UserId == userId).Count();
                ViewData["Total"] = string.Format("{0:n0}đ", tongTien);
            }
            var dssp = _context.SanPham.Where(sp => sp.OptionCount > 0).Where(sp => id == 0 || sp.CategoryId == id)
                .Where(sp => sp.MinPrice >= min || min == 0)
                .Where(sp => sp.MaxPrice <= max || max == 0).ToList()
                .Where(sp => Key.Count == 0
                || (sp.KeyWord??"").Split(';').Any(k => Key.Contains(k.RemoveVietnameseTone().ToLower()))
                || sp.Name.Trim().ToLower().RemoveVietnameseTone().Contains(string.Join(' ', Key)));
                

            var dsLoaiSP = _context.LoaiSP.Where(lsp => lsp.IsActivated);
            ViewData["DSLSP"] = dsLoaiSP.ToList();
            ViewData["SoSPToiDa"] = dssp.Count();
            ViewData["DSSP"] = dssp.Skip((page - 1) * 12).Take(12).ToList();
            ViewData["Data"] = _context.WebInfos.FirstOrDefault(wi => wi.IsActivated); return View();
        }

        [HttpGet("{id}")]
        public ActionResult ProductDetail(int id)
        {
            if (!_context.WebInfos.Any(wi => wi.IsActivated))
            {
                return Redirect(Url.Action("Home", "Home", new { area = "Admin" }));
            }
            if (User == null || User.Identity == null || !User.Identity.IsAuthenticated)
            {
                ViewData["CartCount"] = 0;
                ViewData["Total"] = string.Format("{0:n0}đ", 0);
            }
            else
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                var data = _context.UserItems.Where(ui => ui.UserId == userId)
                    .Include(ui => ui.ChiTietSP)
                    .Include(ui => ui.ChiTietSP.SanPham).ToList();
                var dsGioHang = _context.UserItems.Where(ui => ui.UserId == userId).Include(ui => ui.ChiTietSP).ToList();
                var tongTien = 0.0;
                dsGioHang.ForEach(sp => tongTien += sp.ChiTietSP.Price * sp.SoLuong);
                ViewData["CartCount"] = _context.UserItems.Where(ui => ui.UserId == userId).Count();
                ViewData["Total"] = string.Format("{0:n0}đ", tongTien);
            }
            var sp = _context.SanPham.Include(sp => sp.Details).Include(sp => sp.Category).FirstOrDefault(sp => sp.Id == id);
            if (sp == null)
            {
                return NotFound();
            }
            var dsLoaiSP = _context.LoaiSP.Where(lsp => lsp.IsActivated);
            ViewData["DSLSP"] = dsLoaiSP.ToList();
            ViewData["SanPham"] = sp;
            ViewData["Data"] = _context.WebInfos.FirstOrDefault(wi => wi.IsActivated); return View();
        }

        [Authorize]
        public ActionResult CheckOut()
        {
            if (!_context.WebInfos.Any(wi => wi.IsActivated))
            {
                return Redirect(Url.Action("Home", "Home", new { area = "Admin" }));
            }
            if (User == null || User.Identity == null || !User.Identity.IsAuthenticated)
            {
                ViewData["CartCount"] = 0;
                ViewData["Total"] = string.Format("{0:n0}đ", 0);
                ViewData["CartItems"] = new List<UserItem>();
            }
            else
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                var data = _context.UserItems.Where(ui => ui.UserId == userId)
                    .Include(ui => ui.ChiTietSP)
                    .Include(ui => ui.ChiTietSP.SanPham).ToList();
                ViewData["CartItems"] = data;
                var dsGioHang = _context.UserItems.Where(ui => ui.UserId == userId).Include(ui => ui.ChiTietSP).ToList();
                var tongTien = 0.0;
                dsGioHang.ForEach(sp => tongTien += sp.ChiTietSP.Price * sp.SoLuong);
                ViewData["CartCount"] = _context.UserItems.Where(ui => ui.UserId == userId).Count();
                ViewData["Total"] = string.Format("{0:n0}đ", tongTien);
            }
            var dsLoaiSP = _context.LoaiSP.Where(lsp => lsp.IsActivated);
            ViewData["DSLSP"] = dsLoaiSP.ToList();
            ViewData["Data"] = _context.WebInfos.FirstOrDefault(wi => wi.IsActivated); return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult CheckOut([Bind("DiaChi,Tinh,Huyen,Xa,Note, SoDienThoai, Email")] HoaDonXuat hdx)
        {
            if (!_context.WebInfos.Any(wi => wi.IsActivated))
            {
                return Redirect(Url.Action("Home", "Home", new { area = "Admin" }));
            }
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (_context.UserItems.Any(ui => ui.UserId == userId && ui.SoLuong > 0))
            {
                hdx.UserId = userId;
                hdx.Status = TrangThaiHoaDon.Ordering;
                _context.HoaDonXuat.Add(hdx);
                _context.SaveChanges();
                var gioHang = _context.UserItems.Where(ui => ui.UserId == userId).Include(ui => ui.ChiTietSP).ToList();
                double tong = 0;
                gioHang.ForEach(sp =>
                {
                    _context.ChiTietHoaDonXuat.Add(new ChiTietHDXuat
                    {
                        MaHDXuat = hdx.Id,
                        MaChiTietSP = sp.MaChiTietSP,
                        SoLuong = sp.SoLuong,
                        Gia = sp.ChiTietSP.Price
                    });
                    tong += sp.SoLuong * sp.ChiTietSP.Price;
                });
                hdx.TongTien = tong;
                hdx.PhiShip = tong * 0.05;

                if (_context.SaveChanges() > 0)
                {
                    _context.UserItems.RemoveRange(gioHang);
                    _context.SaveChanges();
                    return RedirectToAction(nameof(Contact));
                }
            }

            return RedirectToAction(nameof(CheckOut));
        }

        public ActionResult Contact()
        {
            if (User == null || User.Identity == null || !User.Identity.IsAuthenticated)
            {
                ViewData["CartCount"] = 0;
                ViewData["Total"] = string.Format("{0:n0}đ", 0);
                ViewData["CartItems"] = new List<UserItem>();
            }
            else
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                var data = _context.UserItems.Where(ui => ui.UserId == userId)
                    .Include(ui => ui.ChiTietSP)
                    .Include(ui => ui.ChiTietSP.SanPham).ToList();
                ViewData["CartItems"] = data;
                var dsGioHang = _context.UserItems.Where(ui => ui.UserId == userId).Include(ui => ui.ChiTietSP).ToList();
                var tongTien = 0.0;
                dsGioHang.ForEach(sp => tongTien += sp.ChiTietSP.Price * sp.SoLuong);
                ViewData["CartCount"] = _context.UserItems.Where(ui => ui.UserId == userId).Count();
                ViewData["Total"] = string.Format("{0:n0}đ", tongTien);
            }
            var dsLoaiSP = _context.LoaiSP.Where(lsp => lsp.IsActivated);
            ViewData["DSLSP"] = dsLoaiSP.ToList();
            ViewData["Data"] = _context.WebInfos.FirstOrDefault(wi => wi.IsActivated);

            return View();
        }
    }
}
