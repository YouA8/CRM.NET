using IRepository;
using Model;
using Model.DTO;
using System.Linq;

namespace Repository
{
    public class RoleRepository : BaseRepository<Role>, IRoleRepository
    {

        public RoleRepository(CRMDbContext dbcontext) : base(dbcontext)
        {
        }

        public IQueryable<Role> GetRoleByParams(RoleQuery query)
        {
            var role = _dbContext.Role.Where(r => r.IsValid == 1);
            if (!string.IsNullOrEmpty(query.RoleName))
            {
                role = role.Where(r => r.RoleName.Contains(query.RoleName));
            }
            return role;
        }
    }
}
