using Model;
using Model.DTO;

namespace IService
{
    public interface ICusServerService : IBaseService<CusServer>
    {
        /// <summary>
        /// 查询客户服务--根据客户名和状态查询
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public RespResult<object> GetCusServerByParam(CusServerQuery query);

        /// <summary>
        /// 添加客户服务
        /// </summary>
        /// <param name="cusServerDTO"></param>
        /// <returns></returns>
        public RespResult<object> AddCusServer(CusServerDTO cusServerDTO);

        /// <summary>
        /// 服务分配/服务处理/服务反馈
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cusServerDTO"></param>
        /// <returns></returns>
        public RespResult<object> UpdateCusServerState(int id, CusServerDTO cusServerDTO);

        /// <summary>
        /// 删除客户服务
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public RespResult<object> DeleteCusServer(int[] ids);

        /// <summary>
        /// 获取服务类型
        /// </summary>
        /// <returns></returns>
        public RespResult<object> GetServerType();

        /// <summary>
        /// 服务构成
        /// </summary>
        /// <returns></returns>
        public RespResult<object> ServerMake();
    }
}
