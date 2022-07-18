using Model;
using Model.DTO;
using System.Linq;

namespace IRepository
{
    public interface IRoleRepository : IBaseRepository<Role>
    {
        /// <summary>
        /// 多条件查询 -- 角色名 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public IQueryable<Role> GetRoleByParams(RoleQuery query);
    }
}
