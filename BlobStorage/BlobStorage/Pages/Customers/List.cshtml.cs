using BlobStorage.Core;
using BlobStorage.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BlobStorage.Pages.Customers
{
    public class ListModel : PageModel
    {
        public string Message { get; set; }
        public IConfiguration Config;
        private readonly ICustomerData customerData;

        public IEnumerable<Customer> Customers { get; set; }

        [BindProperty(SupportsGet =true)]
        public string SearchTerm { get; set; }

        public ListModel(IConfiguration config, 
                        ICustomerData customerData)
        {
            this.Config = config;
            this.customerData = customerData;

        }

        public void OnGet(string searchTerm)
        {
            
            Message = Config["Message"];
            Customers = customerData.GetCustomersByName(SearchTerm);
        }
    }
}
