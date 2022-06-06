using BlobStorage.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlobStorage.Data
{
    public interface ICustomerImage
    {
        IEnumerable<CustomerImages> GetImagesById(int id);
        CustomerImages GetImageID(int id);
        //CustomerImages GetById(int customerid);
        CustomerImages Update(CustomerImages updatedCustomerImage);
        CustomerImages Add(CustomerImages newImages);
        CustomerImages Delete(int id);
        int Commit();

    }


}