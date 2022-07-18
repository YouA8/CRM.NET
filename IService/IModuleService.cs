using Model;
using Model.DTO;

namespace IService
{
    public interface IModuleService : IBaseService<Module>
    {
        /// <summary>
        /// 资源列表
        /// </summary>
        /// <returns></returns>
        public RespResult<object> GetAll();

        /// <summary>
        /// 添加资源
        /// </summary>
        /// <param name="moduleDTO"></param>
        /// <returns></returns>
        public RespResult<object> AddModule (ModuleDTO moduleDTO);

        /// <summary>
        /// 更新资源
        /// </summary>
        /// <param name="id"></param>
        /// <param name="moduleDTO"></param>
        /// <returns></returns>
        public RespResult<object> UpdateModule(int id, ModuleDTO moduleDTO);

        /// <summary>
        /// 删除资源
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public RespResult<object> DeleteModule(int id);

        /// <summary>
        /// 根据用户名获取资源
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public RespResult<object> GetUserModule(string username);
    }
}
