using Azure.Storage.Blobs.Models;
using Azure.Storage.Blobs;
using Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class AzureBlobService : IFilesService
    {
        private readonly string images = string.Empty;
        private readonly string audios = string.Empty;
        private readonly string connectionString = string.Empty;

        public AzureBlobService(IConfiguration configuration)
        {
            images = configuration.GetSection("AzureBlobOptions").GetValue<string>("ImageContainer")!;
            audios = configuration.GetSection("AzureBlobOptions").GetValue<string>("AudiosContainer")!;
            connectionString = configuration.GetConnectionString("AzureBlobs")!;
        }

        public async Task DeleteFile(string path)
        {
            /*BlobContainerClient client;
            if (isImage)
                client = new BlobContainerClient(connectionString, images);
            else
                client = new BlobContainerClient(connectionString, audios);

            client.
            await client.CreateIfNotExistsAsync();
            await client.SetAccessPolicyAsync(PublicAccessType.Blob);

            string root = environment.WebRootPath;
            string fullPath = root + path;

            if (File.Exists(fullPath))
                return Task.Run(() => File.Delete(fullPath));

            return Task.CompletedTask;*/
        }

        public Task<string> EditFile(string oldPath, IFormFile newFile, bool isImage)
        {
            throw new NotImplementedException();
        }

        public async Task<string> SaveFile(IFormFile file, bool isImage)
        {
            BlobContainerClient client;
            if (isImage)
                client = new BlobContainerClient(connectionString, images);
            else
                client = new BlobContainerClient(connectionString, audios);

            await client.CreateIfNotExistsAsync();
            await client.SetAccessPolicyAsync(PublicAccessType.Blob);

            string name = Guid.NewGuid().ToString(); 
            string extension = Path.GetExtension(file.FileName);
            string fullName = name + extension;

            BlobHttpHeaders httpHeaders = new BlobHttpHeaders()
            {
                ContentType = file.ContentType
            };
            var blob = client.GetBlobClient(fullName);
            await blob.UploadAsync(file.OpenReadStream(), httpHeaders);
            return blob.Uri.ToString();
        }
    }
}
