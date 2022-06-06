using System.ComponentModel.DataAnnotations;

namespace BlobStorage.Core
{
    public class CustomerImages
    {
        public int ID { get; set; }
        public int CustomerID { get; set; }
        public string FileName { get; set; }
    }
}


