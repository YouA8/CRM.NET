using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// 客户流失暂缓
    /// </summary>
    public class CusReprieve
    {
        public int Id { get; set; }
        public int LossId { get; set; }                             //流失客户Id
        public string Measure { get; set; }                    //措施
        public DateTime? PlanStartTime { get; set; }     //计划开始时间
        public DateTime? PlanEndTime { get; set; }       //计划结束时间
        public string ExeAffect { get; set; }                   //执行结果
        public int IsValid { get; set; }                            // 是否有效--删除标志
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }

        public CusReprieve()
        {
        }

        public CusReprieve(int id, int lossId, string measure, int isValid, DateTime createTime, DateTime updateTime)
        {
            Id = id;
            LossId = lossId;
            Measure = measure;
            IsValid = isValid;
            CreateTime = createTime;
            UpdateTime = updateTime;
        }
    }
}
