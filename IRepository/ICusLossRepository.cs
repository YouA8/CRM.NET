using Model;
using Model.DTO;
using System.Linq;

namespace IRepository
{
    public interface ICusLossRepository : IBaseRepository<CusLoss>
    {
        /// <summary>
        /// 根据条件查找 -- 客户姓名，客户ID，客户状态
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public IQueryable<CusLossDTO> GetCusLossByParams(CusLossQuery query);
    }
}
