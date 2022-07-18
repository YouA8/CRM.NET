using IRepository;
using Model;
using Model.DTO;
using System.Linq;

namespace Repository
{
    public class CusLossRepository : BaseRepository<CusLoss>, ICusLossRepository
    {
        public CusLossRepository(CRMDbContext dbcontext) : base(dbcontext)
        {
        }
        
        public IQueryable<CusLossDTO> GetCusLossByParams(CusLossQuery query)
        {
            var cusloss = _dbContext.CusLoss.Join(_dbContext.Customer, cl => cl.CusId, c => c.Id, (cl, c) => new CusLossDTO
            {
                Id = cl.Id,
                CusId = cl.CusId,
                CusName = c.Name,
                ConfirmLossTime = cl.ConfirmLossTime,
                CusManager = c.CusManager,
                LastOrderTime = cl.LastOrderTime,
                LossReason = cl.LossReason,
                State = cl.State
            });
            if(query.CusId != null)
            {
                cusloss = cusloss.Where(c => c.CusId == query.CusId); 
            }
            if(query.State != null)
            {
                cusloss = cusloss.Where(c => c.State == query.State);
            }
            if (!string.IsNullOrEmpty(query.CusName))
            {
                cusloss = cusloss.Where(c => c.CusName.Contains(query.CusName));
            }
            return cusloss;
        }
    }
}
