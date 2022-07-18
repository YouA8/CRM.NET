using System;

namespace Model
{
    /// <summary>
    /// 客户联系
    /// </summary>
    public class CusContatct
    {
        public int Id { get; set; }
        public int CusId { get; set; }                                          //客户编号
        public DateTime? ContactTime { get; set; }                  //联系时间
        public string Address { get; set; }                                  //联系地址
        public string Overview { get; set; }                                  //概述
        public int IsValid { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }

        public CusContatct()
        {
        }

        public CusContatct(int id, int cusId, DateTime? contactTime, string address, string overview, int isValid, DateTime createTime, DateTime updateTime)
        {
            Id = id;
            CusId = cusId;
            ContactTime = contactTime;
            Address = address;
            Overview = overview;
            IsValid = isValid;
            CreateTime = createTime;
            UpdateTime = updateTime;
        }
    }
}
