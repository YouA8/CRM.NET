using IRepository;
using Model;
using Model.DTO;
using System.Linq;

namespace Repository
{
    public class CusReprieveRespository : BaseRepository<CusReprieve>, ICusReprieveRepository
    {
        public CusReprieveRespository(CRMDbContext dbcontext) : base(dbcontext)
        {
        }

        public IQueryable<CusReprieve> GetCusReprieveByParam(CusReprieveQuery query)
        {
            var cusreprieve = _dbContext.CusReprieve.Where(c => c.IsValid == 1);
            if(query.LossId != null)
            {
                cusreprieve = cusreprieve.Where(c => c.LossId == query.LossId);
            }
            return cusreprieve;
        }
    }
}
