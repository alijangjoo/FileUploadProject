using FileUploadProject.WebAPI.DataAccess.Models;
using FileUploadProject.WebAPI.DataAccess.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.Net.Http.Headers;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FileUploadProject.WebAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly IFileRepository _fileRepository;
        private readonly IOptions<ApplicationConfiguration> _optionsApplicationConfiguration;
        public FileController(IFileRepository fileRepository, IOptions<ApplicationConfiguration> o)
        {
            _fileRepository = fileRepository;
            _optionsApplicationConfiguration = o;
        }

        #region Get

        [Route("GetCustomerImage")]
        [HttpGet]
        public async Task<IActionResult> GetCustomerImage(int id)
        {
            return await GetImage(id);
        }

        private Task<FileStreamResult> GetImage(int id)
        {
            var fileAllSummary = _fileRepository.GetFile(id);
            var path = _optionsApplicationConfiguration.Value.ServerUploadFolder + "\\" + fileAllSummary.FileName;
            var stream = new FileStream(path, FileMode.Open);
            return Task.FromResult(File(stream, fileAllSummary.ContentType));
        }

        [Route("GetUserImage")]
        [HttpGet]
        public async Task<IActionResult> GetUserImage(int id)
        {
            return await GetImage(id);
        }
        #endregion

        [Route("SaveFile")]
        [HttpPost]
        public async Task<IActionResult> SaveFiles([FromForm] FileInputParams inputParams)
        {
            var data = new FileInpuDTO()
            {
                CreatedTimeStamp = DateTime.Now,
                UpdatedTimeStamp = DateTime.Now
            };
            if (ModelState.IsValid)
            {
                foreach (var file in inputParams.Files)
                {
                    if (file.Length > 0)
                    {
                        var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.ToString().Trim('"');
                        data.InputFiles.Add(
                            new InputFileInfo()
                            {
                                ContentType = file.ContentType,
                                Description = inputParams.Description,
                                ExternalId = inputParams.Name,
                                Name = fileName,
                            });
                        await file.SaveAsAsync(Path.Combine(_optionsApplicationConfiguration.Value.ServerUploadFolder, fileName));
                    }
                }
            }

            var result = _fileRepository.SaveFiles(data);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            bool result = _fileRepository.DeleteFile(id);
            // To-Do: must delete from system file
            return Ok(result);
        }
    }
}
