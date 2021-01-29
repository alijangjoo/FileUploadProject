using System.Collections.Generic;

namespace FileUploadProject.WebAPI.DataAccess.Models
{
    public class AllUploadedFiles
    {
        public List<FileDescriptionShort> FileShortDescriptions { get; set; }
    }
}
