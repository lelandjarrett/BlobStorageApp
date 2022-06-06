using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BlobStorage.Core;
using BlobStorage.Data;

namespace BlobStorage.Pages.Customers
{
    public class EditImageModel : PageModel
    {
        private readonly ICustomerImage customerImage;
        private readonly ICustomerData customerData;

        [BindProperty]
        public CustomerImages CustomerImages { get; set; }
        public Customer Customer { get; set; }

        public EditImageModel(ICustomerImage customerImage, ICustomerData customerData)
        {
            this.customerImage = customerImage;
            this.customerData = customerData;
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

    }
}
