using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// 客户流失
    /// </summary>
    public class CusLoss
    {
        public int Id { get; set; }                                             
        public int CusId { get; set; }                                          //客户Id
        public DateTime? LastOrderTime { get; set; }               //最后订单时间
        public DateTime? ConfirmLossTime { get; set; }           //确认流失时间
        public string LossReason { get; set; }                            //客户流失原因
        public int State { get; set; }                                            //客户状态
        public int IsValid { get; set; }                                          // 是否有效--删除标志
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }

        public CusLoss()
        {
        }

        public CusLoss(int id, int cusId, DateTime? lastOrderTime, DateTime? confirmLossTime, string lossReason, int state, int isValid, DateTime createTime, DateTime updateTime)
        {
            Id = id;
            CusId = cusId;
            LastOrderTime = lastOrderTime;
            ConfirmLossTime = confirmLossTime;
            LossReason = lossReason;
            State = state;
            IsValid = isValid;
            CreateTime = createTime;
            UpdateTime = updateTime;
        }
    }
}
