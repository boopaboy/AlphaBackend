using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Http;

namespace Business.Services
{
    public interface IImageService
    {
        Task<string> UploadImageAsync(IFormFile file);
    }

    public class ImageService : IImageService
    {
        private readonly BlobServiceClient _blobServiceClient;
        private readonly string _containerName = "images";

        public ImageService(BlobServiceClient blobServiceClient)
        {
            _blobServiceClient = blobServiceClient;
        }

        public async Task<string> UploadImageAsync(IFormFile file)
        {
            BlobContainerClient container = _blobServiceClient.GetBlobContainerClient(_containerName);
            await container.CreateIfNotExistsAsync(PublicAccessType.Blob);
            BlobClient blobClient = container.GetBlobClient(file.FileName);
            await blobClient.UploadAsync(file.OpenReadStream(), new BlobHttpHeaders { ContentType = file.ContentType });
            return blobClient.Uri.ToString();
        }

        public async Task DeleteImageAsync(string imageUrl)
        {
            BlobContainerClient container = _blobServiceClient.GetBlobContainerClient(_containerName);
            BlobClient blobClient = container.GetBlobClient(imageUrl);
            await blobClient.DeleteAsync();
        }

       public async Task checkIfImageExistsAsync(IFormFile file)
        {
            BlobContainerClient container = _blobServiceClient.GetBlobContainerClient(_containerName);
            BlobClient blobClient = container.GetBlobClient(file.FileName);
            await blobClient.ExistsAsync();
        }
    }
}