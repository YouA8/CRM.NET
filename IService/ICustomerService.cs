using Model;
using Model.DTO;

namespace IService
{
    public interface ICustomerService : IBaseService<Customer>
    {
        /// <summary>
        /// 根据条件查询客户--客户Id，等级，姓名
        /// </summary>
        /// <param name="customerQuery"></param>
        /// <returns></returns>
        public RespResult<object> GetCustomerByParam(CustomerQuery customerQuery);

        /// <summary>
        /// 添加客户
        /// </summary>
        /// <param name="customerDTO"></param>
        /// <returns></returns>
        public RespResult<object> AddCustomer(CustomerDTO customerDTO);

        /// <summary>
        /// 修改客户
        /// </summary>
        /// <param name="customerDTO"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public RespResult<object> UpdateCustomer(CustomerDTO customerDTO, int id);

        /// <summary>
        /// 删除客户
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public RespResult<object> DeleteCustomer(int[] ids);

        /// <summary>
        /// 更新客户流失状态
        /// </summary>
        /// <returns></returns>
        public RespResult<object> UpdateCusLossState();

        /// <summary>
        /// 获取客户贡献
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public RespResult<object> GetCusContributionByParam(CusContributionQuery query);

        /// <summary>
        /// 获取客户构成
        /// </summary>
        /// <returns></returns>
        public RespResult<object> GetCusMake();
    }
}
