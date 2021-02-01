using FileUploadProject.WebAPI.DataAccess.Attributes;
using FileUploadProject.WebAPI.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FileUploadProject.WebAPI.DataAccess.Repositories
{
    [LifecycleTransient]
    public class FileRepository : IFileRepository, IDisposable
    {
        private FileContext _context;
        public FileRepository(FileContext fileContext)
        {
            _context = fileContext;
        }
        public IEnumerable<FileSummaryResult> SaveFiles(FileInpuDTO fileInpuDTO)
        {
            foreach (var item in fileInpuDTO.InputFiles)
            {
                _context.FileDescriptions.Add(
                    new FileDescription()
                    {
                        ContentType = item.ContentType,
                        FileName = item.Name,
                        Name = item.ExternalId,
                        CreatedTimestamp = fileInpuDTO.CreatedTimeStamp,
                        UpdatedTimestamp = fileInpuDTO.UpdatedTimeStamp,
                        Description = item.Description
                    });
            }
            _context.SaveChanges();
            return GetNewSavedFiles(fileInpuDTO.InputFiles);
        }

        private IEnumerable<FileSummaryResult> GetNewSavedFiles(List<InputFileInfo> files)
        {
            List<string> fileNames = files.Select(c => c.Name).ToList<string>();
            IEnumerable<FileDescription> x = _context.FileDescriptions.Where(r => fileNames.Contains(r.FileName));
            return x.Select(t => new FileSummaryResult { Name = t.Name, Id = t.Id, Description = t.Description });
        }

        public void Dispose()
        {
            if (_context != null)
            {
                _context.Dispose();
                _context = null;
            }
        }

        public bool DeleteFile(int id)
        {
            var selectedFile = _context.FileDescriptions.Find(id);
            if (selectedFile != null)
            {
                _context.FileDescriptions.Remove(selectedFile);
                _context.SaveChanges();
                return true;
            }
            else
                return false;
        }

        public bool UpdateFile()
        {
            throw new NotImplementedException();
        }

        IEnumerable<FileSummaryResult> IFileRepository.GetAllFiles()
        {
            return _context.FileDescriptions.Select(
                   t => new FileSummaryResult { Name = t.Name, Id = t.Id, Description = t.Description });
        }

        public FileAllSummary GetFile(int id)
        {
            var selectedFile = _context.FileDescriptions.SingleOrDefault(t => t.Id == id);
            if (selectedFile != null)
            {
                return new FileAllSummary()
                {
                    ContentType = selectedFile.ContentType,
                    CreatedDateTime = selectedFile.CreatedTimestamp,
                    UpdatedDateTime = selectedFile.UpdatedTimestamp,
                    Description = selectedFile.Description,
                    FileName = selectedFile.FileName,
                    ExternalId = selectedFile.Name
                };
            }
            else
                return new FileAllSummary();
        }

        public bool UpdateFile(FileInpuDTO fileInpuDTO)
        {
            throw new NotImplementedException();
        }
    }
}
