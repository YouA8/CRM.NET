using Model;
using Model.DTO;

namespace IService
{
    public interface ICusLossService : IBaseService<CusLoss>
    {
        /// <summary>
        /// 根据条件查找 -- 客户姓名，客户ID，客户状态
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public RespResult<object> GetCusLossByParams(CusLossQuery query);

        /// <summary>
        /// 修改流失客户状态 -- 确认流失，添加流失原因
        /// </summary>
        /// <param name="id"></param>
        /// <param name="lossReason"></param>
        /// <returns></returns>
        public RespResult<object> UpdateCusLossState(int id, string lossReason);
    }
}
