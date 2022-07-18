using System;

namespace Model.DTO
{
    public class CusReprieveDTO
    {
        public int Id { get; set; }
        public int LossId { get; set; }                             //流失客户Id
        public string Measure { get; set; }                    //措施
        public DateTime? PlanStartTime { get; set; }     //计划开始时间
        public DateTime? PlanEndTime { get; set; }       //计划结束时间
        public string ExeAffect { get; set; }                   //执行结果
    }
}
