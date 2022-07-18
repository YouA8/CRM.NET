using Model;
using Model.DTO;

namespace IService
{
    public interface ISaleChanceService : IBaseService<SaleChance>
    {
        /// <summary>
        /// 分页多条件查询 -- 客户名，创建人，分配状态
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public RespResult<object> GetSaleChanceByParams(SaleChanceQuery query);

        /// <summary>
        /// 添加营销机会
        /// </summary>
        /// <param name="saleChance"></param>
        /// <returns></returns>
        public RespResult<object> AddSaleChance(SaleChanceDTO saleChance);

        /// <summary>
        /// 修改营销机会
        /// </summary>
        /// <param name="saleChanceDTO"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public RespResult<object> UpdateSaleChance(SaleChanceDTO saleChanceDTO, int id);

        /// <summary>
        /// 修改开发状态
        /// </summary>
        /// <param name="saleChanceDTO"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public RespResult<object> UpdateSaleChanceDevResult(SaleChanceDTO saleChanceDTO, int id);

        /// <summary>
        /// 批量删除营销机会
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public RespResult<object> DeleteSaleChance(int[] ids);

        /// <summary>
        /// 营销机会详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public RespResult<SaleChanceDTO> Details(int id);
    }
}
