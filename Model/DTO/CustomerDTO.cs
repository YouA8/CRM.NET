namespace Model.DTO
{
    public class CustomerDTO
    {
        public int? Id { get; set; }
        public string Name { get; set; }                    // 客户名称
        public string Contact { get; set; }                 // 联系人
        public string Phone { get; set; }                   // 联系电话
        public string Email { get; set; }                   // 联系邮箱
        public string Address { get; set; }                //地址
        public string WebSite { get; set; }                 //网站
        public string Level { get; set; }                    //客户等级
        public string Xyd { get; set; }                      //信用度
        public string CusManager { get; set; }          //客户经理
        public string Myd { get; set; }                     //满意度
        public int State { get; set; }                    //客户流失状态 0-流失 1-未流失
        public string Description { get; set; }             // 描述
    }
}
