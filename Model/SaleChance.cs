using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// 营销机会
    /// </summary>
    public class SaleChance
    {
        public int Id { get; set; }                             // 编号
        public string ChanceSource { get; set; }                // 机会来源
        public string CustomerName { get; set; }                // 客户信息
        public string Contact { get; set; }                     // 联系人
        public string Phone { get; set; }                       // 联系电话
        public int Probability { get; set; }                    // 机率
        public string Overview { get; set; }                    // 概述
        public string Description { get; set; }                 // 描述
        public string CreatorName { get; set; }                 // 创建者
        public string AssignorName { get; set; }                // 指派人
        public DateTime? AssignTime { get; set; }               // 指派时间
        public int State { get; set; }                          // 分配状态 0-未分配 1-已分配
        public int DevResult { get; set; }                      // 开发结果 0-未开发 1-开发中 2-开发成功 3-开发失败
        public int IsValid { get; set; }                        // 是否有效--删除标志
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }

        public SaleChance()
        {
        }

        public SaleChance(int id, string chanceSource, string customerName, string contact, string phone, int probability, string overview, string description, string creatorName, string assignorName, DateTime? assignTime, int state, int devResult, int isValid, DateTime createTime, DateTime updateTime)
        {
            Id = id;
            ChanceSource = chanceSource;
            CustomerName = customerName;
            Contact = contact;
            Phone = phone;
            Probability = probability;
            Overview = overview;
            Description = description;
            CreatorName = creatorName;
            AssignorName = assignorName;
            AssignTime = assignTime;
            State = state;
            DevResult = devResult;
            IsValid = isValid;
            CreateTime = createTime;
            UpdateTime = updateTime;
        }
    }
}
