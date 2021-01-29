﻿using FileUploadProject.WebAPI.DataAccess.Attributes;
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

        public IEnumerable<FileDescriptionShort> AddFileDescriptions(FileResult fileResult)
        {
            List<string> filenames = new List<string>();
            for (int i = 0; i < fileResult.FileNames.Count(); i++)
            {
                var fileDescription = new FileDescription
                {
                    ContentType = fileResult.ContentTypes[i],
                    FileName = fileResult.FileNames[i],
                    Name = fileResult.Names[i],
                    CreatedTimestamp = fileResult.CreatedTimestamp,
                    UpdatedTimestamp = fileResult.UpdatedTimestamp,
                    Description = fileResult.Description
                };

                filenames.Add(fileResult.FileNames[i]);
                _context.FileDescriptions.Add(fileDescription);
            }

            _context.SaveChanges();
            return GetNewFiles(filenames);
        }

        private IEnumerable<FileDescriptionShort> GetNewFiles(List<string> filenames)
        {
            IEnumerable<FileDescription> x = _context.FileDescriptions.Where(r => filenames.Contains(r.FileName));
            return x.Select(t => new FileDescriptionShort { Name = t.Name, Id = t.Id, Description = t.Description });
        }

        public IEnumerable<FileDescriptionShort> GetAllFiles()
        {
            return _context.FileDescriptions.Select(
                    t => new FileDescriptionShort { Name = t.Name, Id = t.Id, Description = t.Description });
        }

        public FileDescription GetFileDescription(int id)
        {
            return _context.FileDescriptions.Single(t => t.Id == id);
        }


        public void Dispose()
        {
            if (_context != null)
            {
                _context.Dispose();
                _context = null;
            }
        }
    }
}
