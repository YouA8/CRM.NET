using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Model
{
    /// <summary>
    /// 角色
    /// </summary>
    public class Role
    {
        public int Id { get; set; }
        public string RoleName { get; set; }
        public string Remarke { get; set; }
        public int IsValid { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }

        [JsonIgnore]
        public virtual List<User> User { get; set; }

        public virtual List<Permission> Permission { get; set; }

        public Role()
        {
        }

        public Role(int id, string roleName, string remarke, int isValid, DateTime createTime, DateTime updateTime, List<User> user, List<Permission> permission)
        {
            Id = id;
            RoleName = roleName;
            Remarke = remarke;
            IsValid = isValid;
            CreateTime = createTime;
            UpdateTime = updateTime;
            User = user;
            Permission = permission;
        }
    }
}
