using BlobStorage.Data;
using BlobStorage.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BlobStorage.Pages.Customers
{
    public class DeleteModel : PageModel
    {
        private readonly ICustomerData customerData;
        public Customer Customer { get; set; }
 
        public DeleteModel(ICustomerData customerData)
        {
            this.customerData = customerData;
        }
        public IActionResult OnGet(int customerId)
        {
            Customer = customerData.GetById(customerId);
           return Page();
        }
        public IActionResult OnPost(int customerId)
        {
            var customer = customerData.Delete(customerId);
            customerData.Commit();

            TempData["Message"] = $"{customer.Name} deleted";
            return RedirectToPage("./List");
        }
    }
}
