using IRepository;
using Model;

namespace Repository
{
    public class CusDevPlanRepository : BaseRepository<CusDevPlan>, ICusDevPlanRepository
    {
        public CusDevPlanRepository(CRMDbContext dbcontext) : base(dbcontext)
        {
        }
    }
}
