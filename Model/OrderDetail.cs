using System;

namespace Model
{
    /// <summary>
    /// 订单详情
    /// </summary>
    public class OrderDetail
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public string GoodsName { get; set; }
        public int? GoodsNum { get; set; }
        public string Unit { get; set; }
        public float? Price { get; set; }
        public float? Sum { get; set; }
        public int IsValid { get; set; }                    // 是否有效--删除标志
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }

        public OrderDetail()
        {
        }

        public OrderDetail(int id, int orderId, string goodsName, int? goodsNum, string unit, float? price, float? sum, int isValid, DateTime createTime, DateTime updateTime)
        {
            Id = id;
            OrderId = orderId;
            GoodsName = goodsName;
            GoodsNum = goodsNum;
            Unit = unit;
            Price = price;
            Sum = sum;
            IsValid = isValid;
            CreateTime = createTime;
            UpdateTime = updateTime;
        }
    }
}
