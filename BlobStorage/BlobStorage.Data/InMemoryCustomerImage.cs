using BlobStorage.Core;

namespace BlobStorage.Data
{
    public class InMemoryCustomerImage : ICustomerImage
    {
        List<CustomerImages> customerImages;
        public InMemoryCustomerImage()
        {
            customerImages = new List<CustomerImages>()
            {
                new CustomerImages{ ID = 1, CustomerID = 1, FileName = "Contract" },
                new CustomerImages{ ID = 2, CustomerID = 2, FileName = "Contract" },
                new CustomerImages{ ID = 3, CustomerID = 3, FileName = "Contract" },
                new CustomerImages{ ID = 4, CustomerID = 1, FileName = "DamagedTracker" },
                new CustomerImages{ ID = 5, CustomerID = 2, FileName = "BullDozer" },
                new CustomerImages{ ID = 6, CustomerID = 3, FileName = "Mower" }
            };
        }

        public CustomerImages GetImageID(int id)
        {
            return customerImages.SingleOrDefault(r => r.ID == id);
        }

        public CustomerImages Add(CustomerImages newImages)
        {
            customerImages.Add(newImages);
            newImages.ID = customerImages.Max(r => r.ID) + 1;
            return newImages;
        }

        public CustomerImages Update(CustomerImages updatedCustomerImage)
        {
            var customerImage = customerImages.SingleOrDefault(r => r.ID == updatedCustomerImage.ID);
            if (customerImage != null)
            {
                customerImage.FileName = updatedCustomerImage.FileName;
                customerImage.CustomerID = updatedCustomerImage.CustomerID;
            }
            return customerImage;
        }

        public int Commit()
        {
            return 0;
        }


        public IEnumerable<CustomerImages> GetImagesById(int id)
        {
            return from r in customerImages
                   where r.CustomerID == id
                   orderby r.FileName
                   select r;
        }

        public CustomerImages Delete(int id)
        {
            var customerimage = customerImages.FirstOrDefault(r => r.ID == id);
            if (customerimage != null)
            {
                customerImages.Remove(customerimage);
            }
            return customerimage;
        }
    }


}