using Model;
using Model.DTO;
using System.Linq;

namespace IRepository
{
    public interface ICusReprieveRepository : IBaseRepository<CusReprieve>
    {
        /// <summary>
        /// 根据条件查询 -- 流失客户Id
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public IQueryable<CusReprieve> GetCusReprieveByParam(CusReprieveQuery query);
    }
}
