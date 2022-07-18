using Model;
using IRepository;

namespace Repository
{
    public class ModuleRepository : BaseRepository<Module>, IModuleRepository
    {
        public ModuleRepository(CRMDbContext dbcontext) : base(dbcontext)
        {
        }
    }
}
