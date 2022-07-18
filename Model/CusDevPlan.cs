using System;

namespace Model
{
    /// <summary>
    /// 客户开发计划
    /// </summary>
    public class CusDevPlan
    {
        public int Id { get; set; }
        public int SaleChanceId { get; set; }
        public string PlanCantext { get; set; }
        public DateTime? PlanStartTime { get; set; }
        public DateTime? PlanEndTime { get; set; }
        public string ExeAffect { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }
        public int IsValid { get; set; }

        public CusDevPlan()
        {
        }

        public CusDevPlan(int id, int saleChanceId, string planCantext, DateTime planStartTime, DateTime planEndTime, string exeAffect, DateTime createTime, DateTime updateTime, int isValid)
        {
            Id = id;
            SaleChanceId = saleChanceId;
            PlanCantext = planCantext;
            PlanStartTime = planStartTime;
            PlanEndTime = planEndTime;
            ExeAffect = exeAffect;
            CreateTime = createTime;
            UpdateTime = updateTime;
            IsValid = isValid;
        }
    }
}
