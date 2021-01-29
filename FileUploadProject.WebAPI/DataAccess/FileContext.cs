using FileUploadProject.WebAPI.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace FileUploadProject.WebAPI.DataAccess
{
    public class FileContext : DbContext
    {
        public FileContext(DbContextOptions<FileContext> dbContextOptions):base(dbContextOptions)
        {

        }
        public DbSet<FileDescription> FileDescriptions { get; set; }
    }
}
