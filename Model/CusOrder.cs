using System;

namespace Model
{
    /// <summary>
    /// 客户订单
    /// </summary>
    public class CusOrder
    {
        public int Id { get; set; }
        public int CusId { get; set; }                     //客户Id
        public string OrderNo { get; set; }                //订单编号
        public DateTime OrderTime { get; set; }  //订单时间
        public string Address { get; set; }             //订单地址
        public int State { get; set; }                       //订单状态
        public int IsValid { get; set; }                    // 是否有效--删除标志
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }

        public CusOrder()
        {
        }

        public CusOrder(int id, int cusId, string orderNo, DateTime orderTime, string address, int state, int isValid, DateTime createTime, DateTime updateTime)
        {
            Id = id;
            CusId = cusId;
            OrderNo = orderNo;
            OrderTime = orderTime;
            Address = address;
            State = state;
            IsValid = isValid;
            CreateTime = createTime;
            UpdateTime = updateTime;
        }
    }
}
