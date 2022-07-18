using IRepository;
using Model;
using Model.DTO;
using System;
using System.Linq;

namespace Repository
{
    public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
    {

        public CustomerRepository(CRMDbContext dbcontext) : base(dbcontext)
        {
        }

        public IQueryable<Customer> GetCustomerByParams(CustomerQuery query)
        {
            var customer = _dbContext.Customer.Where(c => c.IsValid == 1);
            if (!string.IsNullOrEmpty(query.Name))
            {
                customer = customer.Where(c => c.Name.Contains(query.Name));
            }
            if (!string.IsNullOrEmpty(query.Level))
            {
                customer = customer.Where(c => c.Level.Contains(query.Level));
            }
            if(query.Id != null)
            {
                customer = customer.Where(c => c.Id == query.Id);
            }
            return customer;
        }

        public IQueryable<Customer> GetLossCustomer()
        {
            var cus = from c in _dbContext.Customer
                      where c.IsValid == 1 && c.State == 0 && c.CreateTime.AddMonths(6) < DateTime.Now &&
                      (from co in _dbContext.CusOrder
                       where co.OrderTime.AddMonths(6) > DateTime.Now
                       select co.Id).Contains(c.Id)
                      select c;
            return cus;
        }

        public IQueryable<object> GetCusContributionByParam(CusContributionQuery query)
        {
            var cus = from c in _dbContext.Customer
                      join co in _dbContext.CusOrder.DefaultIfEmpty() on c.Id equals co.CusId
                      join o in _dbContext.OrderDetail.DefaultIfEmpty() on co.Id equals o.OrderId
                      where c.State == 1 && c.IsValid == 1 
                      group o.Sum by c.Name into temp
                      select new
                      {
                          CusName = temp.Key,
                          Sum = temp.Sum()
                      };
            return cus;
        }

        public IQueryable<object> GetCusMake()
        {
            var cus = from c in _dbContext.Customer
                      where c.IsValid == 1 && c.State == 1
                      group c by c.Level into temp
                      select new
                      {
                          Level = temp.Key,
                          Total = temp.Count()
                      };
            return cus;
        }
    }
}
