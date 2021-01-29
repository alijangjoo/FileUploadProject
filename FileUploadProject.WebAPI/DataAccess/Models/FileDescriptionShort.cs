using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace FileUploadProject.WebAPI.DataAccess.Models
{
    public class FileDescriptionShort
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public ICollection<IFormFile> File { get; set; }
    }
}
