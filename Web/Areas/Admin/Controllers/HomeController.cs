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
    public class HomeController : Controller
    {
        private readonly WebStoreDbContext _context;
        private readonly FileService _fileService;

        public HomeController(WebStoreDbContext context, FileService fileService)
        {
            _context = context;
            _fileService = fileService;
        }

        // GET: Admin/WebInfoes
        public async Task<IActionResult> Index()
        {
            return View(await _context.WebInfos.ToListAsync());
        }

        // GET: Admin/WebInfoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var webInfo = await _context.WebInfos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (webInfo == null)
            {
                return NotFound();
            }

            return View(webInfo);
        }

        // GET: Admin/WebInfoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/WebInfoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BrandName,HeroContent,SliderFiles,BannerFiles,BannerOneContent,BannerTwoContent,BannerThreeContent,LastestCaption,GalleryContent,Letters,PinterestLink,FacebookLink,InstagramLink,TwitterLink,IsActivated,Id")] ThongTinWeb webInfo)
        {
            if (webInfo.SliderFiles != null)
            {
                if (webInfo.SliderFiles.Count >= 1)
                {
                    webInfo.HeroImageOneName = await _fileService.Upload(webInfo.SliderFiles[0]);
                }
                if (webInfo.SliderFiles.Count >= 2)
                {
                    webInfo.HeroImageTwoName = await _fileService.Upload(webInfo.SliderFiles[1]);
                }
            }
            if (webInfo.BannerFiles != null)
            {
                if (webInfo.BannerFiles.Count >= 1)
                {
                    webInfo.BannerOneImageName = await _fileService.Upload(webInfo.BannerFiles[0]);
                }
                if (webInfo.BannerFiles.Count >= 2)
                {
                    webInfo.BannerTwoImageName = await _fileService.Upload(webInfo.BannerFiles[1]);
                }
                if (webInfo.BannerFiles.Count >= 3)
                {
                    webInfo.BannerThreeImageName = await _fileService.Upload(webInfo.BannerFiles[2]);
                }
            }
            if (ModelState.IsValid)
            {
                if (webInfo.IsActivated)
                {
                    _context.WebInfos.Where(wi => wi.IsActivated).ToList().ForEach(wi => wi.IsActivated = false);
                }
                _context.Add(webInfo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(webInfo);
        }

        // GET: Admin/WebInfoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var webInfo = await _context.WebInfos.FindAsync(id);
            if (webInfo == null)
            {
                return NotFound();
            }
            return View(webInfo);
        }

        // POST: Admin/WebInfoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BrandName,HeroContent,SliderFiles,BannerFiles,BannerOneContent,BannerTwoContent,BannerThreeContent,LastestCaption,GalleryContent,Letters,PinterestLink,FacebookLink,InstagramLink,TwitterLink,IsActivated,Id")] ThongTinWeb webInfo)
        {
            if (id != webInfo.Id)
            {
                return NotFound();
            }
            var old = await _context.WebInfos.AsNoTracking().FirstOrDefaultAsync(wi => wi.Id == id);
            webInfo.HeroImageOneName = old.HeroImageOneName;
            webInfo.HeroImageTwoName = old.HeroImageTwoName;
            webInfo.BannerOneImageName = old.BannerOneImageName;
            webInfo.BannerTwoImageName = old.BannerTwoImageName;
            webInfo.BannerThreeImageName = old.BannerThreeImageName;
            if (webInfo.SliderFiles != null)
            {
                if (webInfo.SliderFiles.Count >= 1)
                {
                    webInfo.HeroImageOneName = await _fileService.Upload(webInfo.SliderFiles[0]);
                }
                if (webInfo.SliderFiles.Count >= 2)
                {
                    webInfo.HeroImageTwoName = await _fileService.Upload(webInfo.SliderFiles[1]);
                }
            }
            if (webInfo.BannerFiles != null)
            {
                if (webInfo.BannerFiles.Count >= 1)
                {
                    webInfo.BannerOneImageName = await _fileService.Upload(webInfo.BannerFiles[0]);
                }
                if (webInfo.BannerFiles.Count >= 2)
                {
                    webInfo.BannerTwoImageName = await _fileService.Upload(webInfo.BannerFiles[1]);
                }
                if (webInfo.BannerFiles.Count >= 3)
                {
                    webInfo.BannerThreeImageName = await _fileService.Upload(webInfo.BannerFiles[2]);
                }
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (webInfo.IsActivated)
                    {
                        _context.WebInfos.Where(wi => wi.IsActivated).ToList().ForEach(wi => wi.IsActivated = false);
                    }
                    _context.Update(webInfo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WebInfoExists(webInfo.Id))
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
            return View(webInfo);
        }

        // GET: Admin/WebInfoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var webInfo = await _context.WebInfos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (webInfo == null)
            {
                return NotFound();
            }

            return View(webInfo);
        }

        // POST: Admin/WebInfoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var webInfo = await _context.WebInfos.FindAsync(id);
            _context.WebInfos.Remove(webInfo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WebInfoExists(int id)
        {
            return _context.WebInfos.Any(e => e.Id == id);
        }
    }
}
