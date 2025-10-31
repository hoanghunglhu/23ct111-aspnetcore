using Microsoft.AspNetCore.Mvc;         
using Microsoft.AspNetCore.Http;          
using System.IO;                          
using System.Threading.Tasks;             
using System.Linq;                       

namespace Upload.Controllers
{
    [ApiController]
    [Route("upload")]
    public class UploadController : ControllerBase
    {
        [HttpPost]
        [RequestSizeLimit(10_000_000)] // 10 MB
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
            return BadRequest("No file uploaded.");

            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".pdf", ".docx", ".txt", ".zip" };
            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();

            if (!allowedExtensions.Contains(extension))
                return BadRequest("File type not allowed.");

            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");
            Directory.CreateDirectory(uploadsFolder);

            var safeFileName = Path.GetFileName(file.FileName);
            var filePath = Path.Combine(uploadsFolder, safeFileName);

            if (System.IO.File.Exists(filePath))
            {
                var uniqueName = $"{Path.GetFileNameWithoutExtension(safeFileName)}_{Guid.NewGuid()}{extension}";
                filePath = Path.Combine(uploadsFolder, uniqueName);
            }

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return Ok(new { fileName = Path.GetFileName(filePath), file.Length });
        }
    }
}