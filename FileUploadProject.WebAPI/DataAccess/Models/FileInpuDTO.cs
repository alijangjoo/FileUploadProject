using System;
using System.Collections.Generic;

namespace FileUploadProject.WebAPI.DataAccess.Models
{
    public class FileInpuDTO {
        public DateTime CreatedTimeStamp { get; set; }
        public DateTime UpdatedTimeStamp { get; set; }
        public List<InputFileInfo> InputFiles { get; set; } = new List<InputFileInfo>();
    }
}
