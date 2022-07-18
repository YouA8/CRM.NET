using AutoMapper;
using Model;
using Model.DTO;

namespace CRM.NET
{
    public class AutoMapperConfig : Profile
    {
        /// <summary>
        /// 配置构造函数，创建映射关系
        /// </summary>
        public AutoMapperConfig()
        {
            CreateMap<SaleChance, SaleChanceDTO>().ReverseMap();
            CreateMap<CusDevPlan, CusDevPlanDTO>().ReverseMap();
            CreateMap<Module, ModuleDTO>().ForMember(m => m.Children, opt => opt.MapFrom(m => m.Children)).ReverseMap();
            CreateMap<Role, RoleDTO>().ReverseMap();
            CreateMap<User, UserDTO>().ForMember(u => u.Role, opt => opt.MapFrom(u => u.Role)).ReverseMap();
            CreateMap<Customer, CustomerDTO>().ReverseMap();
            CreateMap<Permission, PermissionDTO>().ReverseMap();
            CreateMap<CusLoss, CusLossDTO>().ReverseMap();
            CreateMap<CusServer, CusServerDTO>().ReverseMap();
            CreateMap<CusReprieve, CusReprieveDTO>().ReverseMap();
        }
    }
}
