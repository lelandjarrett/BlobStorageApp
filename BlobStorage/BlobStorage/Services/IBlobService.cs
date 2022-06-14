using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlobStorage.Core;
using Microsoft.AspNetCore.Http;

namespace BlobStorage.Data
{
    public interface IBlobService
    {
        Task<string> GetBlob(string name, string containerName);
        Task<IEnumerable<string>> AllBlobs(string containerName);
        Task<bool> UploadBlob(string name, IFormFile file, string containerName);
        Task<bool> DeleteBlob(string name, string containerName);
    }
}
