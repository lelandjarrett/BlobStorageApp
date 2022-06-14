using BlobStorage.Core;
using BlobStorage.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;

namespace BlobStorage.Pages.Customers
{
    public class DetailModel : PageModel
    {
        public IConfiguration Config;
        private readonly ICustomerData customerData;
        private readonly ICustomerImage customerImage;
        private readonly AzureStorageConfig storageConfig;
        private readonly IBlobService blobImage;


        //Model! an object in my contorller and in my view
        public Customer Customer { get; set; }
        public IEnumerable<CustomerImages> Image { get; set; }
        public IEnumerable<BlobImages> BlobImage { get; set; }




        //Controller
        //Constructor (dep injection)
        public DetailModel(ICustomerData customerData,
                            ICustomerImage customerImage,
                            IOptions<AzureStorageConfig> storageConfig,
                            IBlobService blobImage)
        {
            this.customerData = customerData;
            this.customerImage = customerImage;
            this.blobImage = blobImage;
        }

        //Some changes
        public void OnGet(int customerId, string containerName, string name)
        {
            Image = (IEnumerable<CustomerImages>)customerImage.GetImagesById(customerId);
            //BlobImage = (IEnumerable<IBlobService>)blobImage.GetBlob(name,containerName);
            //BlobImage = blobImage.GetBlob(name, containerName).ToList;
            Customer = customerData.GetById(customerId);
        }
    }
}
