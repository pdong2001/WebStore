using Data;
using Data.Models;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize(Roles = "NormalUser")]
    public class CartController : Controller
    {
        private readonly WebStoreDbContext _context;

        public CartController(WebStoreDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<CartDto> AddCart(CreateUpdateCartDto request)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var item = _context.UserItems.FirstOrDefault(ci => ci.MaChiTietSP == request.MaCTSP && ci.UserId == userId);
            if (item != null)
            {
                item.SoLuong += request.Quantity;
            }
            else
            {
                await _context.UserItems.AddAsync(new UserItem()
                {
                    UserId = userId,
                    MaChiTietSP = request.MaCTSP,
                    SoLuong = request.Quantity
                });
            }
            await _context.SaveChangesAsync();
            var dssp = _context.UserItems.Where(ui => ui.UserId == userId).Include(ui => ui.ChiTietSP).ToList();
            var tongTien = 0.0;
            dssp.ForEach(sp => tongTien += sp.ChiTietSP.Price * sp.SoLuong);
            var newItem = _context.UserItems.Include(ui => ui.ChiTietSP).FirstOrDefault(ui => ui.UserId == userId && ui.MaChiTietSP == request.MaCTSP);
            var dto = new CartDto()
            {
                TienSanPhamMoi = string.Format("{0:n0}đ", (newItem.SoLuong * newItem.ChiTietSP.Price)),
                TongTien = string.Format("{0:n0}đ", tongTien),
                SoSanPham = dssp.Count()
            };
            return dto;
        }

        [HttpPost]
        public async Task<CartDto> AddOrUpdateCart(CreateUpdateCartDto request)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var item = _context.UserItems.FirstOrDefault(ci => ci.MaChiTietSP == request.MaCTSP && ci.UserId == userId);
            if (item != null)
            {
                item.SoLuong = request.Quantity;
            }
            else
            {
                await _context.UserItems.AddAsync(new UserItem()
                {
                    UserId = userId,
                    MaChiTietSP = request.MaCTSP,
                    SoLuong = request.Quantity
                });
            }
            await _context.SaveChangesAsync();
            var dssp = _context.UserItems.Where(ui => ui.UserId == userId).Include(ui => ui.ChiTietSP).ToList();
            var tongTien = 0.0;
            dssp.ForEach(sp => tongTien += sp.ChiTietSP.Price * sp.SoLuong);
            var newItem = _context.UserItems.Include(ui => ui.ChiTietSP).FirstOrDefault(ui => ui.UserId == userId && ui.MaChiTietSP == request.MaCTSP);
            var dto = new CartDto()
            {
                TienSanPhamMoi = string.Format("{0:n0}đ", (newItem.SoLuong * newItem.ChiTietSP.Price)),
                TongTien = string.Format("{0:n0}đ", tongTien),
                SoSanPham = dssp.Count()
            };
            return dto;
        }

        [HttpPost]
        public async Task<CartDto> DeleteCart(int id)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var item = await _context.UserItems.FirstOrDefaultAsync(ui => ui.MaChiTietSP == id && ui.UserId == userId);
            if (item != null)
            {
                _context.UserItems.Remove(item);
                await _context.SaveChangesAsync();
            }


            var dssp = _context.UserItems.Where(ui => ui.UserId == userId).Include(ui => ui.ChiTietSP).ToList();
            var tongTien = 0.0;
            dssp.ForEach(sp => tongTien += sp.ChiTietSP.Price * sp.SoLuong);
            var dto = new CartDto()
            {
                TienSanPhamMoi = "",
                TongTien = string.Format("{0:n0}đ", tongTien),
                SoSanPham = dssp.Count()
            };
            return dto;
        }
    }
}

