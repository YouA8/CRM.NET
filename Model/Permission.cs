using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// 权限
    /// </summary>
    public class Permission
    {
        public int Id { get; set; }
        public virtual int RoleId { get; set; }
        public virtual int ModuleId { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }

        public Permission()
        {
        }

        public Permission(int id, int roleId, int moduleId, DateTime createTime, DateTime updateTime)
        {
            Id = id;
            RoleId = roleId;
            ModuleId = moduleId;
            CreateTime = createTime;
            UpdateTime = updateTime;
        }
    }
}
