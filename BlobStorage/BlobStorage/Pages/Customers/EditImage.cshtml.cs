using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BlobStorage.Core;
using BlobStorage.Data;
using Azure.Storage.Blobs;
using Microsoft.Extensions.Options;

namespace BlobStorage.Pages.Customers
{
    public class EditImageModel : PageModel
    {
        private readonly ICustomerImage customerImage;
        private readonly ICustomerData customerData;
        private readonly AzureStorageConfig storageConfig;

        [BindProperty]
        public CustomerImages CustomerImages { get; set; }
        public Customer Customer { get; set; }

        public EditImageModel(ICustomerImage customerImage, ICustomerData customerData, IOptions<AzureStorageConfig> storageConfig)
        {
            this.customerImage = customerImage;
            this.customerData = customerData;
            this.storageConfig = storageConfig.Value;
        }


        public IActionResult OnGet(int? customerImageID, int customerId)
        {
            if (customerImageID.HasValue)
            {
                CustomerImages = customerImage.GetImageID(customerImageID.Value);
            }
            else
            {
                Customer = customerData.GetById(customerId);
                CustomerImages = new CustomerImages();
                CustomerImages.CustomerID = Customer.ID;
            }
            
            //customerImage.Commit();
            return Page();
        }

        public IActionResult OnPost()
        {
            if (CustomerImages.ID > 0)
            {
                customerImage.Update(CustomerImages);
            }
            else
            {
                customerImage.Add(CustomerImages);
            }
            //CustomerImages = customerImage.Update(CustomerImages);
            customerImage.Commit();
            return Page();
        }
        public Task Save(Stream fileStream, string name)
        {
            BlobServiceClient blobServiceClient = new BlobServiceClient(storageConfig.ConnectionString);

            // Get the container (folder) the file will be saved in
            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(storageConfig.FileContainerName);

            // Get the Blob Client used to interact with (including create) the blob
            BlobClient blobClient = containerClient.GetBlobClient(name);

            // Upload the blob
            return blobClient.UploadAsync(fileStream);
        }

    }
}
