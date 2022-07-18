using System.Collections.Generic;

namespace Model.DTO
{
    public class ModuleDTO
    {
        public int? Id { get; set; }                                                           // 资源Id
        public string ModuleName { get; set; }                                    // 资源名称
        public string ModuleStyle { get; set; }                                      // 资源类型
        public string Url { get; set; }                                                     // Url
        public List<ModuleDTO> Children { get; set; }                       // 子级对象
        public int ParentId { get; set; }                                                 //父级ID
        public int Grade { get; set; }                                                     // 层级
        public int Orders { get; set; }                                                   // 排序号
    }
}
