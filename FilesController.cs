using FileUploadDownloadAPI.ApplicatioDbContext;
using FileUploadDownloadAPI.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FileUploadDownloadAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public FilesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("upload")]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("Invalid file");

            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);

                var fileModel = new FileModel
                {
                    FileName = file.FileName,
                    FileContent = memoryStream.ToArray()
                };

                _context.Files.Add(fileModel);
                await _context.SaveChangesAsync();

                return Ok(new { fileModel.Id });
            }
        }

        [HttpGet]
       [Route("download/{id}")]
        public IActionResult Download(int id)
        {
            var file = _context.Files.FirstOrDefault(f => f.Id == id);

            if (file == null)
                return NotFound();

            return File(file.FileContent, "application/octet-stream", file.FileName);
        }
    }
}
    