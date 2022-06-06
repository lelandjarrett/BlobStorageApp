using BlobStorage.Core;
using Microsoft.EntityFrameworkCore;
using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace BlobStorage.Data
{
    public class SqlCustomerImageData : ICustomerImage
    {
        private readonly CustomerDbContext db;

        public SqlCustomerImageData(CustomerDbContext db)
        {
            this.db = db;
        }
        public CustomerImages Add(CustomerImages newImages)
        {
            db.Add(newImages);
            return (newImages);
        }

        public int Commit()
        {
            return db.SaveChanges();
        }

        public CustomerImages Delete(int id)
        {
            var customerImage = GetImageID(id);
            if(customerImage != null)
            {
                db.CustomerImages.Remove((CustomerImages)customerImage);
            }
            return (CustomerImages)customerImage;
        }

        public CustomerImages GetImageID(int id)
        {
            return db.CustomerImages.Find(id);
        }

        public IEnumerable<CustomerImages> GetImagesById(int id)
        {
            return from r in db.CustomerImages
                   where r.CustomerID == id
                   orderby r.FileName
                   select r;
        }

        public CustomerImages Update(CustomerImages updatedCustomerImage)
        {
            var entity = db.CustomerImages.Attach(updatedCustomerImage);
            entity.State = EntityState.Modified;
            return updatedCustomerImage;
        }
    }


}