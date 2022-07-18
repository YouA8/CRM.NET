using System;

namespace Model.DTO
{
    public class CusLossDTO
    {
        public int Id { get; set; }                                                 //编号
        public int? CusId { get; set; }                                         //客户编号
        public string CusName { get; set; }                               //客户名称
        public string CusManager { get; set; }                          //客户经理
        public DateTime? LastOrderTime { get; set; }               //最后订单时间
        public DateTime? ConfirmLossTime { get; set; }           //确认流失时间
        public string LossReason { get; set; }                            //客户流失原因
        public int State { get; set; }                                            //客户状态
    }
}
