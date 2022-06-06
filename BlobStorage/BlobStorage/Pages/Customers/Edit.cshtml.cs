using BlobStorage.Core;
using BlobStorage.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BlobStorage.Pages.Customers
{
    public class EditModel : PageModel
    {
        private readonly ICustomerData customerData;
        private readonly IHtmlHelper htmlHelper;

        [BindProperty]
        public Customer Customer { get; set; }
        public IEnumerable<SelectListItem> BusinessType { get; set; }

        public EditModel(ICustomerData customerData, IHtmlHelper htmlHelper)
        {
            this.customerData = customerData;
            this.htmlHelper = htmlHelper;
        }

        public IActionResult OnGet(int? customerId)
        {
            BusinessType = htmlHelper.GetEnumSelectList<BussinesType>();
            if (customerId.HasValue)
            {
                Customer = customerData.GetById(customerId.Value);
            }
            else
            {
                Customer = new Customer();
            }

            return Page();
        }
        public IActionResult OnPost()
        {
            if(Customer.ID > 0)
            {
                customerData.Update(Customer);
            }
            else
            {
                customerData.Add(Customer);
            }
            //Customer = customerData.Update(Customer);
            customerData.Commit();
            return Page();
        } 
    }
}