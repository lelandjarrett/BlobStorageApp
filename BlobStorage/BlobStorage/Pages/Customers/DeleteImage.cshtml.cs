using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BlobStorage.Data;
using BlobStorage.Core;

namespace BlobStorage.Pages.Customers
{
    public class DeleteImageModel : PageModel
    {
        private readonly ICustomerImage customerImage;
        public CustomerImages CustomerImages { get; set; }

        public DeleteImageModel(ICustomerImage customerImage)
        {
            this.customerImage = customerImage;
        }
        public IActionResult OnGet(int customerImageID)
        {
            CustomerImages = customerImage.GetImageID(customerImageID);
            return Page();
        }
        public IActionResult OnPost(int customerImageID)
        {
            var customerImages = customerImage.Delete(customerImageID);
            customerImage.Commit();

            TempData["Message"] = $"{customerImages.FileName} deleted";
            return RedirectToPage("./List");
        }
    }
}
