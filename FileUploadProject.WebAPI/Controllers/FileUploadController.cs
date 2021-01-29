using FileUploadProject.WebAPI.DataAccess.Models;
using FileUploadProject.WebAPI.DataAccess.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using FileResult = FileUploadProject.WebAPI.DataAccess.Models.FileResult;
namespace FileUploadProject.WebAPI.Controllers
{
    [Route("api/test")]
    [ApiController]
    public class FileUploadController : ControllerBase
    {
        private readonly IFileRepository _fileRepository;
        private readonly IOptions<ApplicationConfiguration> _optionsApplicationConfiguration;

        public FileUploadController(IFileRepository fileRepository, IOptions<ApplicationConfiguration> o)
        {
            _fileRepository = fileRepository;
            _optionsApplicationConfiguration = o;
        }

        [Route("files")]
        [HttpPost]
        [ServiceFilter(typeof(ValidateMimeMultipartContentFilter))]
        public async Task<IActionResult> UploadFiles(FileDescriptionShort fileDescriptionShort)
        {
            var names = new List<string>();
            var contentTypes = new List<string>();
            if (ModelState.IsValid)
            {
                // http://www.mikesdotnetting.com/article/288/asp-net-5-uploading-files-with-asp-net-mvc-6
                // http://dotnetthoughts.net/file-upload-in-asp-net-5-and-mvc-6/
                foreach (var file in fileDescriptionShort.File)
                {
                    if (file.Length > 0)
                    {
                        var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.ToString().Trim('"');
                        contentTypes.Add(file.ContentType);

                        names.Add(fileName);

                        // Extension method update RC2 has removed this 
                        await file.SaveAsAsync(Path.Combine(_optionsApplicationConfiguration.Value.ServerUploadFolder, fileName));
                    }
                }
            }

            var files = new FileResult
            {
                FileNames = names,
                ContentTypes = contentTypes,
                Description = fileDescriptionShort.Description,
                CreatedTimestamp = DateTime.UtcNow,
                UpdatedTimestamp = DateTime.UtcNow,
            };

            _fileRepository.AddFileDescriptions(files);

            return Ok();
        }

        [Route("download/{id}")]
        [HttpGet]
        public FileStreamResult Download(int id)
        {
            var fileDescription = _fileRepository.GetFileDescription(id);

            var path = _optionsApplicationConfiguration.Value.ServerUploadFolder + "\\" + fileDescription.FileName;
            var stream = new FileStream(path, FileMode.Open);
            return File(stream, fileDescription.ContentType);
        }
    }
}
