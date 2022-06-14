using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using BlobStorage.Data;

namespace BlobStorage.Services
{
    public class BlobService : IBlobService
    {
        private readonly BlobServiceClient _blobClient;
        private readonly IConfiguration configuration;
        private readonly IBlobService _blobService;

        public BlobService(BlobServiceClient blobClient)
        {
            this._blobClient = blobClient;
        }

        public async Task<IEnumerable<string>> AllBlobs(string containerName )
        {
            //Allow us to access the data inside the container
            var containerClient = _blobClient.GetBlobContainerClient("images");

            var files = new List<string>();

            var blobs = containerClient.GetBlobsAsync();

            await foreach(var item in blobs)
            {
                files.Add(item.Name);
            }

            return files;
        }

        public async Task<bool> DeleteBlob(string name, string containerName )
        {
            var containerClient = _blobClient.GetBlobContainerClient("images");
            var blobClient = containerClient.GetBlobClient(name);
            return await blobClient.DeleteIfExistsAsync();
        }

        public async Task<string> GetBlob(string name, string containerName )
        {
            //var res = await _blobService.GetBlob(name, configuration.GetValue<string>("BlobContainer"));
            //This will allow us access to the storage container
            var containerClient = _blobClient.GetBlobContainerClient(containerName);
            // This will allow us access to the file inside the container via the file name
            var blobClient = containerClient.GetBlobClient(name);

            return blobClient.Uri.AbsoluteUri;

        }

        public async Task<bool> UploadBlob(string name, IFormFile file, string containerName )
        {
            var containerClient = _blobClient.GetBlobContainerClient("images");

            var blobClient = containerClient.GetBlobClient(name);

            var httpheaders = new BlobHttpHeaders()
            {
                ContentType = file.ContentType
            };
            var res = await blobClient.UploadAsync(file.OpenReadStream(), httpheaders);

            if(res != null)
                return true;
                return false;
        }
    }
}
