using System;
using System.Collections.Generic;

namespace Model
{
    /// <summary>
    /// 用户
    /// </summary>
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }                // 用户名
        public string UserPwd { get; set; }                 // 密码
        public string Name { get; set; }                    // 姓名
        public string Email { get; set; }                   // 邮箱
        public string Phone { get; set; }                   // 电话
        public int IsValid { get; set; }                    // 是否有效--删除标志
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }

        public virtual Role Role { get; set; }

        public User()
        {
        }

        public User(int id, string userName, string userPwd, string name, string email, string phone, int isValid, DateTime createTime, DateTime updateTime, Role role)
        {
            Id = id;
            UserName = userName;
            UserPwd = userPwd;
            Name = name;
            Email = email;
            Phone = phone;
            IsValid = isValid;
            CreateTime = createTime;
            UpdateTime = updateTime;
            Role = role;
        }
    }
}
