using System;
using System.ComponentModel.DataAnnotations;

namespace FileUploadProject.WebAPI.DataAccess.Models
{
    public class FileDescription
    {
        [Key]
        public int Id { get; set; }
        public string FileName { get; set; }
        public string Description { get; set; }
        public DateTime CreatedTimestamp { get; set; }
        public DateTime UpdatedTimestamp { get; set; }
        public string ContentType { get; set; }
        public string Name { get; set; }
    }
    public class FileAllSummary
    {
        public string FileName { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime UpdatedDateTime { get; set; }
        public string ContentType { get; set; }
        public string ExternalId { get; set; }


    }
}
