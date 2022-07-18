using Model;
using Model.DTO;

namespace IService
{
    public interface ICusReprieveService : IBaseService<CusReprieve>
    {
        /// <summary>
        /// 根据条件查询 -- 流失客户Id
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public RespResult<object> GetCusReprieveByParam(CusReprieveQuery query);

        /// <summary>
        /// 添加客户流失数据
        /// </summary>
        /// <param name="cusReprieveDTO"></param>
        /// <returns></returns>
        public RespResult<object> AddCusReprieve(CusReprieveDTO cusReprieveDTO);

        /// <summary>
        /// 修改客户流失数据
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cusReprieveDTO"></param>
        /// <returns></returns>
        public RespResult<object> UpdateCusReprieve(int id,CusReprieveDTO cusReprieveDTO);

        /// <summary>
        /// 删除客户流失数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public RespResult<object> DeleteCusReprieve(int id);
    }
}
