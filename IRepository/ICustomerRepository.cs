using Model;
using Model.DTO;
using System.Linq;

namespace IRepository
{
    public interface ICustomerRepository : IBaseRepository<Customer>
    {
        /// <summary>
        /// 多条件查询 -- 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public IQueryable<Customer> GetCustomerByParams(CustomerQuery query);

        /// <summary>
        /// 查找流失客户 -- （6个月未联系）创建时间大于6个月或者最后订单6个月前，客户判定为流失
        /// </summary>
        /// <returns></returns>
        public IQueryable<Customer> GetLossCustomer();

        /// <summary>
        /// 根据条件查询客户贡献 -- 客户名、区间、时间
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public IQueryable<object> GetCusContributionByParam(CusContributionQuery query);

        /// <summary>
        /// 获取客户构成
        /// </summary>
        /// <returns></returns>
        public IQueryable<object> GetCusMake();
    }
}
