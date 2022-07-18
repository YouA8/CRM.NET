using AutoMapper;
using IRepository;
using IService;
using Microsoft.EntityFrameworkCore;
using Model;
using Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Service
{
    public class ModuleService : BaseService<Module>, IModuleService
    {
        private readonly IModuleRepository _moduleRepository;
        private readonly IMapper _mapper;

        public ModuleService(IModuleRepository moduleRepository, IMapper mapper)
        {
            _mapper = mapper;
            _moduleRepository = moduleRepository;
        }

        public RespResult<object> AddModule(ModuleDTO moduleDTO)
        {
            var flag = _moduleRepository.Where(m => m.ModuleName == moduleDTO.ModuleName && m.IsValid == 1).FirstOrDefault();
            if (flag == null && ValueVaild(moduleDTO))
            {
                var module = _mapper.Map<ModuleDTO, Module>(moduleDTO);
                module.CreateTime = DateTime.Now;
                module.UpdateTime = DateTime.Now;
                _moduleRepository.Add(module);
                return new RespResult<object>(null, (int)RespCode.Success, "添加成功！");
            }
            return new RespResult<object>(null, (int)RespCode.Fail, "添加失败！");
        }

        public RespResult<object> DeleteModule(int id)
        {
            var module = _moduleRepository.Where(m => m.Id == id).Include(m => m.Children).FirstOrDefault();
            if (module != null && module.Children == null)
            {
                module.Children = null;
                module.IsValid = 0;
                _moduleRepository.Update(module);
                return new RespResult<object>(null, (int)RespCode.Success, "删除成功！");
            }
            return new RespResult<object>(null, (int)RespCode.Fail, "删除失败，该菜单下有子菜单！");
        }

        public RespResult<object> GetAll()
        {
            var temp = _moduleRepository.GetAll().Where(m => m.Grade == 1 && m.IsValid == 1).Include(m => m.Children).ToList();
            if(temp != null && temp.Count > 0)
            {
                var list = _mapper.Map<List<Module>, List<ModuleDTO>>(temp);
                return new RespResult<object>(list, (int)RespCode.Success, "资源列表");
            }
            return new RespResult<object>(null, (int)RespCode.Fail, "无数据！");
        }

        public RespResult<object> UpdateModule(int id, ModuleDTO moduleDTO)
        {
            // 1.校验数据
            var module = _moduleRepository.Where(m => m.IsValid == 1 && m.Id == id).FirstOrDefault();
            if(module != null && !string.IsNullOrEmpty(module.ModuleName))
            {
                var flag = _moduleRepository.Where(m => m.ModuleName == moduleDTO.ModuleName).FirstOrDefault();
                if (flag == null)
                {
                    //2.值映射
                    var temp = _mapper.Map(moduleDTO,module);
                    //3.默认值设置
                    temp.UpdateTime = DateTime.Now;
                    temp.Id = id;
                    temp.Children = null;
                    //4.更改
                    _moduleRepository.Update(temp);
                    return new RespResult<object>(null, (int)RespCode.Success, "修改成功！");
                }
                return new RespResult<object>(null, (int)RespCode.Fail, "数据重复！");
            }
            return new RespResult<object>(null, (int)RespCode.Fail, "修改失败！");
        }

        public RespResult<object> GetUserModule(string username)
        {
            var user = _moduleRepository.DbContext.User.Where(u => u.IsValid == 1 && u.UserName.Equals(username)).Include(u=>u.Role).Where(u=>u.Role.IsValid == 1).FirstOrDefault();
            if (user != null)
            {
                if(user.Role != null)
                {
                    var module = from p in _moduleRepository.DbContext.Permission
                                 join m in _moduleRepository.DbContext.Module.DefaultIfEmpty() on p.ModuleId equals m.Id
                                 where p.RoleId == user.Role.Id && m.IsValid == 1 && m.Grade == 1
                                 select new Module()
                                 {
                                     Id = m.Id,
                                     Name = m.Name,
                                     ModuleName = m.ModuleName,
                                     ModuleStyle = m.ModuleStyle,
                                     Url = m.Url,
                                     Children = m.Children,
                                     Grade = m.Grade,
                                     Orders = m.Orders,
                                     IsValid = m.IsValid,
                                     CreateTime = m.CreateTime,
                                     UpdateTime = m.UpdateTime
                                 };
                    return new RespResult<object>(module, (int)RespCode.Success, "用户菜单数据！");
                }
            }
            return new RespResult<object>(null, (int)RespCode.Fail, "无数据！");
        }

        /// <summary>
        /// 数据校验
        /// </summary>
        /// <param name="moduleDTO"></param>
        /// <returns></returns>
        private bool ValueVaild(ModuleDTO moduleDTO)
        {
            if (moduleDTO.Grade == 0 || moduleDTO.Grade == 1 || moduleDTO.Grade == 2)
            {
                return true;
            }
            return false;
        }
    }
}
