using Data;
using Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        /// <summary>
        /// WebStoreDbContext
        /// </summary>
        private readonly WebStoreDbContext _context;

        /// <summary>
        /// Phương thức khởi tạo
        /// </summary>
        /// <param name="context">Tiêm WebStoreDbContext vào controller</param>
        public FileController(WebStoreDbContext context)
        {
            //Gán giá trị được truyền vào biến _context
            _context = context;
        }

        /// <summary>
        /// Lấy một file ảnh có bằng id từ database
        /// </summary>
        /// <param name="id">ID ảnh</param>
        /// <returns>Trả về một file ảnh</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetFile(int id)
        {
            var file = await _context.Blobs.FirstOrDefaultAsync(f => f.Id == id);
            if (file != null)
            {
                return new FileContentResult(file.Content, "image/jpeg");
            }
            else
            {
                throw new Exception("Image not found");
            }
        }
    }
}
