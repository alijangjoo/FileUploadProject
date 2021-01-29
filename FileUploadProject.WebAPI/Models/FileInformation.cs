using FileUploadProject.WebAPI.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileUploadProject.WebAPI.Models
{
    public class FileInformation
    {
        public List<FileDescriptionShort> FileNames { get; set; }
    }
}
