using System;

namespace Model
{
    /// <summary>
    /// 客户
    /// </summary>
    public class Customer
    {
        public int Id { get; set; }     
        public string Name { get; set; }                    // 客户名称
        public string Contact { get; set; }                 // 联系人
        public string Phone { get; set; }                   // 联系电话
        public string Email { get; set; }                   // 联系邮箱
        public string Address { get; set; }                 //地址
        public string WebSite { get; set; }                 //网站
        public string Level { get; set; }                   //客户等级
        public string Xyd { get; set; }                     //信用度
        public string CusManager { get; set; }              //客户经理
        public string Myd { get; set; }                     //满意度
        public int State { get; set; }                      //客户流失状态 0-流失 1-未流失
        public string Description { get; set; }             // 描述
        public int IsValid { get; set; }                    // 是否有效--删除标志
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }

        public Customer()
        {
        }

        public Customer(int id, string name, string contact, string phone, string email, string address, string webSite, string level, string xyd, string cusManager, string myd, int state, string description, int isValid, DateTime createTime, DateTime updateTime)
        {
            Id = id;
            Name = name;
            Contact = contact;
            Phone = phone;
            Email = email;
            Address = address;
            WebSite = webSite;
            Level = level;
            Xyd = xyd;
            CusManager = cusManager;
            Myd = myd;
            State = state;
            Description = description;
            IsValid = isValid;
            CreateTime = createTime;
            UpdateTime = updateTime;
        }
    }
}
