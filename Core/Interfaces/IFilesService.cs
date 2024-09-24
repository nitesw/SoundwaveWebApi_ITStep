using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IFilesService
    {
        Task<string> SaveFile(IFormFile file, bool isImage);
        Task DeleteFile(string path);
        Task<string> EditFile(string oldPath, IFormFile newFile, bool isImage);
    }
}
