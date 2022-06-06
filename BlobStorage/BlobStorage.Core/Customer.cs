using System.ComponentModel.DataAnnotations;

namespace BlobStorage.Core
{
    public class Customer 
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public BussinesType BussineType { get; set; }
    }


}


