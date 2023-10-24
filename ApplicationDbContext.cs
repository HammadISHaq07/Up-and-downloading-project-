using FileUploadDownloadAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace FileUploadDownloadAPI.ApplicatioDbContext
{
    public class ApplicationDbContext : DbContext
    {
      

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<FileModel> Files { get; set; }
    }
}