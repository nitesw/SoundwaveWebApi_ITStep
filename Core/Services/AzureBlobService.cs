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
            const string baseUrl = "https://soundwavestorage.blob.core.windows.net/";
            if (path.StartsWith(baseUrl, StringComparison.OrdinalIgnoreCase))
            {
                path = path.Substring(baseUrl.Length);
            }

            var pathParts = path.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
            string directory = pathParts[0];
            string fileName = string.Join("/", pathParts.Skip(1));

            BlobContainerClient client;
            if (directory == images)
                client = new BlobContainerClient(connectionString, images);
            else
                client = new BlobContainerClient(connectionString, audios);

            await client.DeleteBlobIfExistsAsync(fileName);
            await client.SetAccessPolicyAsync(PublicAccessType.Blob);
        }

        public async Task<string> EditFile(string oldPath, IFormFile newFile, bool isImage)
        {
            Console.WriteLine("\n\n\n" + oldPath);
            Console.WriteLine(newFile);
            Console.WriteLine(isImage + "\n\n\n");
            await DeleteFile(oldPath);
            return await SaveFile(newFile, isImage);
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
