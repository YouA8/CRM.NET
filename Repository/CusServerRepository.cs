using IRepository;
using Model;
using Model.DTO;
using System.Linq;

namespace Repository
{
    public class CusServerRepository : BaseRepository<CusServer>, ICusServerRepository
    {
        public CusServerRepository(CRMDbContext dbcontext) : base(dbcontext)
        {
        }

        public IQueryable<CusServer> GetCusServerByParam(CusServerQuery query)
        {
            var cus = _dbContext.CusServer.Where(c => c.IsValid == 1);
            if (!string.IsNullOrEmpty(query.Customer))
            {
                cus = cus.Where(c => c.Customer.Contains(query.Customer));
            }
            if (query.State != null)
            {
                cus = cus.Where(c => c.State == query.State);
            }
            if (!string.IsNullOrEmpty(query.ServerType))
            {
                cus = cus.Where(c => c.ServerType.Contains(query.ServerType));
            }
            if (!string.IsNullOrEmpty(query.Assigner))
            {
                cus = cus.Where(c => c.Assigner == query.Assigner);
            }
            return cus;
        }

        /// <summary>
        /// 服务构成
        /// </summary>
        /// <returns></returns>
        public IQueryable<object> ServerMake()
        {
            var ser = from s in _dbContext.CusServer
                      where s.IsValid == 1
                      group s by s.ServerType into temp
                      select new
                      {
                          ServerType = temp.Key,
                          Total = temp.Count()
                      };
            return ser;
        }
    }
}
