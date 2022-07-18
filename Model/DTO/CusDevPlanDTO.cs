using System;

namespace Model.DTO
{
    public class CusDevPlanDTO
    {
        public int? Id { get; set; }
        public int SaleChanceId { get; set; }
        public string PlanCantext { get; set; }
        public DateTime PlanStartTime { get; set; }
        public DateTime PlanEndTime { get; set; }
        public string ExeAffect { get; set; }

        public CusDevPlanDTO()
        {
        }
    }
}
