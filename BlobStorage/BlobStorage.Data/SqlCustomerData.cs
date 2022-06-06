using BlobStorage.Core;
using Microsoft.EntityFrameworkCore;

namespace BlobStorage.Data
{
    public class SqlCustomerData : ICustomerData
    {
        private readonly CustomerDbContext db;

        public SqlCustomerData(CustomerDbContext db)
        {
            this.db = db;
        }
        public Customer Add(Customer newCustomer)
        {
            db.Add(newCustomer);
            return newCustomer;
        }

        public int Commit()
        {
            return db.SaveChanges();
        }

        public Customer Delete(int id)
        {
            var customer = GetById(id);
            if(customer != null)
            {
                db.Customers.Remove(customer);
            }
            return customer;
        }

        public Customer GetById(int id)
        {
            return db.Customers.Find(id);
        }

        public IEnumerable<Customer> GetCustomersByName(string name)
        {
            var query = from r in db.Customers
                        where r.Name.StartsWith(name) || string.IsNullOrEmpty(name)
                        orderby r.Name
                        select r;
            return query;
        }

        public Customer Update(Customer updatedCustomer)
        {
            var entity = db.Customers.Attach(updatedCustomer);
            entity.State = EntityState.Modified;
            return updatedCustomer;
        }
    }
}
