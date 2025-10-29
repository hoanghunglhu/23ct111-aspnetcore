using Microsoft.AspNetCore.Mvc;

namespace UploadimageApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UploadController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;

        public UploadController(IWebHostEnvironment env)
        {
            _env = env;
        }

        [HttpPost("image")]
        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("Không có file nào được tải lên.");

            // Tạo đường dẫn vật lý để lưu ảnh
            string uploadPath = Path.Combine(_env.ContentRootPath, "Uploads");

            // Nếu thư mục chưa tồn tại, tạo mới
            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);

            // Tạo tên file duy nhất
            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            string filePath = Path.Combine(uploadPath, fileName);

            // Lưu file lên server
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // Tạo URL để trả về
            string fileUrl = $"{Request.Scheme}://{Request.Host}/Uploads/{fileName}";

            return Ok(new { url = fileUrl });
        }
    }
}
