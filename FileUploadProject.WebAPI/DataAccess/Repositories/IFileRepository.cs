using FileUploadProject.WebAPI.DataAccess.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FileUploadProject.WebAPI.DataAccess.Repositories
{

    public interface IFileRepository
    {
        IEnumerable<FileSummaryResult> SaveFiles(FileInpuDTO fileInpuDTO); 
        IEnumerable<FileSummaryResult> GetAllFiles();
        FileAllSummary GetFile(int id);
        bool DeleteFile(int id);
        bool UpdateFile(FileInpuDTO fileInpuDTO);
    }
}
