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



        public Customer Customer { get; set; }
        public IEnumerable<CustomerImages> Image { get; set; }



        public DetailModel(ICustomerData customerData,
                            ICustomerImage customerImage,
                            IOptions<AzureStorageConfig> storageConfig)
        {
            this.customerData = customerData;
            this.customerImage = customerImage;
        }

        public void OnGet(int customerId)
        {
            Image = (IEnumerable<CustomerImages>)customerImage.GetImagesById(customerId);
            Customer = customerData.GetById(customerId);
        }

    }

}
