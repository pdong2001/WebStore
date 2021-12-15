using Data;
using Data.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Services
{
    public class FileService
    {
        private readonly WebStoreDbContext _context;

        public FileService(WebStoreDbContext context)
        {
            _context = context;
        }

        public async Task<int> Upload(IFormFile file)
        {
            var fileName = file.FileName;

            Blob toInsert;

            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                toInsert = new Blob
                {
                    Name = fileName,
                    Content = memoryStream.ToArray()
                };
            }
            _context.Blobs.Add(toInsert);
            _context.SaveChanges();
            return toInsert.Id;
        }
    }
}
