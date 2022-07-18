using System;

namespace Model.DTO
{
    public class SaleChanceDTO
    {
        public int? Id { get; set; }                            // 编号
        public string ChanceSource { get; set; }                // 机会来源
        public string CustomerName { get; set; }                // 客户名称
        public string Contact { get; set; }                     // 联系人
        public string Phone { get; set; }                       // 联系电话
        public int Probability { get; set; }                    // 机率
        public string Overview { get; set; }                    // 概述
        public string Description { get; set; }                 // 描述
        public string CreatorName { get; set; }                 // 创建名称
        public string AssignorName { get; set; }                // 指派人名称
        public DateTime? AssignTime { get; set; }               // 指派时间
        public int State { get; set; }                          // 分配状态 0-未分配 1-已分配
        public int DevResult { get; set; }                      // 开发结果 0-未开发 1-开发中 2-开发成功 3-开发失败

        public SaleChanceDTO()
        {
        }

    }
}
