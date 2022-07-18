using Model;
using Model.DTO;
using System.Linq;

namespace IRepository
{
    public interface ISaleChanceRepository : IBaseRepository<SaleChance>
    {
        /// <summary>
        /// 多条件查询 -- 根据客户名，创建人，分配状态
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public IQueryable<SaleChance> GetSaleChanceByParams(SaleChanceQuery query);
    }
}
