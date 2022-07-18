using Model;
using Model.DTO;

namespace IService
{
    public interface ICusDevPlanService : IBaseService<CusDevPlan>
    {
        /// <summary>
        /// 分页查询客户开发计划 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public RespResult<object> GetCusDevPlanByParams(CusDevPlanQuery query);

        /// <summary>
        /// 添加客户开发计划
        /// </summary>
        /// <param name="cusDevPlanDTO"></param>
        /// <returns></returns>
        public RespResult<object> AddCusDevPlan(CusDevPlanDTO cusDevPlanDTO);

        /// <summary>
        /// 修改客户开发计划
        /// </summary>
        /// <param name="cusDevPlanDTO"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public RespResult<object> UpdateCusDevPlan(CusDevPlanDTO cusDevPlanDTO,int id);

        /// <summary>
        /// 删除客户开发计划
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public RespResult<object> DeleteCusDevPlan(int[] ids);

        /// <summary>
        /// 客户开发计划详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public RespResult<object> Details(int id);
    }
}
