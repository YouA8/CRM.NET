using Model;
using Model.DTO;
using System.Linq;

namespace IRepository
{
    public interface ICusServerRepository : IBaseRepository<CusServer>
    {
        /// <summary>
        /// 查询客户服务--根据客户名和状态查询
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public IQueryable<CusServer> GetCusServerByParam(CusServerQuery query);

        /// <summary>
        /// 服务构成
        /// </summary>
        /// <returns></returns>
        public IQueryable<object> ServerMake();
    }
}
