using IRepository;
using Model;
using Model.DTO;
using System.Linq;

namespace Repository
{
    public class SaleChanceRepository : BaseRepository<SaleChance>, ISaleChanceRepository
    {
        public SaleChanceRepository(CRMDbContext dbcontext) : base(dbcontext)
        {
        }

        public IQueryable<SaleChance> GetSaleChanceByParams(SaleChanceQuery query)
        {
            var saleChances = _dbContext.SaleChance.Where(s => s.IsValid == 1);
            if (!string.IsNullOrEmpty(query.CustomerName))
            {
                saleChances = saleChances.Where(s => s.CustomerName.Contains(query.CustomerName));
            }
            if (!string.IsNullOrEmpty(query.CreatorName))
            {
                saleChances = saleChances.Where(s => s.CreatorName.Contains(query.CreatorName));
            }
            if (query.State != null)
            {
                saleChances = saleChances.Where(s => s.State == query.State);
            }
            if (!string.IsNullOrEmpty(query.AssignorName))
            {
                saleChances = saleChances.Where(s => s.AssignorName == query.AssignorName);
            }
            if (query.DevResult != null)
            {
                saleChances = saleChances.Where(s => s.DevResult == query.DevResult);
            }
            return saleChances;
        }
    }
}
