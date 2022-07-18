using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Model
{
    /// <summary>
    /// 资源
    /// </summary>
    public class Module
    {
        public int Id { get; set; }                        // 资源Id
        public string ModuleName { get; set; }             // 资源名称
        public string Name { get; set; }                   // 名称
        public string ModuleStyle { get; set; }            // 资源类型
        public string Url { get; set; }                    // Url
        [ForeignKey("ParentId")]
        public List<Module> Children { get; set; }         // 子级Id
        public int Grade { get; set; }                     // 层级
        public int? Orders { get; set; }                   // 排序号
        public int IsValid { get; set; }                   // 是否有效
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }

        [JsonIgnore]
        public virtual List<Permission> Permission { get; set; }

        public Module()
        {
        }

        public Module(int id, string moduleName, string name, string moduleStyle, string url, List<Module> children, int grade, int orders, int isValid, DateTime createTime, DateTime updateTime, List<Permission> permission)
        {
            Id = id;
            ModuleName = moduleName;
            Name = name;
            ModuleStyle = moduleStyle;
            Url = url;
            Children = children;
            Grade = grade;
            Orders = orders;
            IsValid = isValid;
            CreateTime = createTime;
            UpdateTime = updateTime;
            Permission = permission;
        }
    }
}
