using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace FileUploadProject.WebAPI.DataAccess.Models
{
    public class FileInputParams
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public ICollection<IFormFile> Files { get; set; }
    }
}
