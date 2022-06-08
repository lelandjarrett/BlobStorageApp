using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlobStorage.Data
{
    public class AzureStorageConfig
    {
        public string ConnectionString { get; set; }
        public string FileContainerName { get; set; }
    }
}
