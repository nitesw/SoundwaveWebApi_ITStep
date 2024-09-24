using Core.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class FilesService : IFilesService
    {
        const string imageFolder = "images";
        const string trackFolder = "audios";
        private readonly IWebHostEnvironment environment;

        public FilesService(IWebHostEnvironment environment)
        {
            this.environment = environment;
        }

        public Task DeleteFile(string path)
        {
            string root = environment.WebRootPath;
            string fullPath = root + path;

            if (File.Exists(fullPath))
                return Task.Run(() => File.Delete(fullPath));

            return Task.CompletedTask;
        }

        public async Task<string> EditFile(string oldPath, IFormFile newFile, bool isImage)
        {
            await DeleteFile(oldPath);
            return await SaveFile(newFile, isImage);
        }

        public async Task<string> SaveFile(IFormFile file, bool isImage)
        {
            string root = environment.WebRootPath;
            string name = Guid.NewGuid().ToString();
            string extension = Path.GetExtension(file.FileName);
            string fullName = name + extension;

            if (isImage)
            {
                string imagePath = Path.Combine(imageFolder, fullName);
                string imageFullPath = Path.Combine(root, imagePath);

                using (FileStream fs = new FileStream(imageFullPath, FileMode.Create))
                {
                    await file.CopyToAsync(fs);
                }

                return Path.DirectorySeparatorChar + imagePath;
            }
            else
            {
                string trackPath = Path.Combine(trackFolder, fullName);
                string trackFullPath = Path.Combine(root, trackPath);

                using (FileStream fs = new FileStream(trackFullPath, FileMode.Create))
                {
                    await file.CopyToAsync(fs);
                }

                return Path.DirectorySeparatorChar + trackPath;
            }
        }
    }
}
