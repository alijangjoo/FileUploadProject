using FileUploadProject.WebAPI.DataAccess.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FileUploadProject.WebAPI.DataAccess.Repositories
{

    public interface IFileRepository
    {
        IEnumerable<FileDescriptionShort> AddFileDescriptions(FileResult fileResult);

        IEnumerable<FileDescriptionShort> GetAllFiles();

        FileDescription GetFileDescription(int id);
    }
}
