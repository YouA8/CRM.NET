using Model;
using Model.DTO;

namespace IService
{
    public interface IRoleService : IBaseService<Role>
    {
        /// <summary>
        /// 角色列表
        /// </summary>
        /// <returns></returns>
        public RespResult<object> GetAll();

        /// <summary>
        /// 分页多条件查询 -- 角色名
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public RespResult<object> GetRolebyParams(RoleQuery query);

        /// <summary>
        /// 添加角色
        /// </summary>
        /// <param name="roleDTO"></param>
        /// <returns></returns>
        public RespResult<object> AddRole(RoleDTO roleDTO);

        /// <summary>
        /// 修改角色
        /// </summary>
        /// <param name="roleDTO"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public RespResult<object> UpdateRole(RoleDTO roleDTO, int id);

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public RespResult<object> DeleteRole(int[] ids);
        
        /// <summary>
        /// 用户详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public RespResult<object> Details(int id);
        
        /// <summary>
        /// 角色授权
        /// </summary>
        /// <param name="id"></param>
        /// <param name="mid"></param>
        /// <returns></returns>
        public RespResult<object> AddGrant(int id, int[] mids);
    }
}
