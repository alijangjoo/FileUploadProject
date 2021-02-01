using FileUploadProject.WebAPI.DataAccess.Models;
using FileUploadProject.WebAPI.DataAccess.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileUploadProject.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileClientController : ControllerBase
    {
        private readonly IFileRepository _fileRepository;

        public FileClientController(IFileRepository fileRepository)
        {
            _fileRepository = fileRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return Ok("Index Page");
        }
        [Route("Client")]
        [HttpGet]
        public IActionResult Client()
        {
            return Ok("Ex Client");
        }
        [Route("MultiClient")]
        [HttpGet]
        public IActionResult MultiClient()
        {
            return Ok("Ex MultiClient");
        }

        [Route("ViewAllFiles")]
        [HttpGet]
        /// <summary>
        /// Just a test method to view all files.
        /// </summary>
        public IActionResult ViewAllFiles()
        {
            // var model = new AllUploadedFiles { FileShortDescriptions = _fileRepository.GetAllFiles().ToList() };
            var model = _fileRepository.GetAllFiles();
            return Ok(model);
        }
    }
}
