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
    public class RoleService : BaseService<Role>, IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;

        public RoleService(IRoleRepository roleRepository, IMapper mapper)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
        }

        public RespResult<object> GetAll()
        {
            var temp = _roleRepository.GetAll().Where(r => r.IsValid == 1).ToList();
            if(temp != null && temp.Count > 0)
            {
                var list = _mapper.Map<List<Role>, List<RoleDTO>>(temp);
                return new RespResult<object>(list, (int)RespCode.Success, "角色列表");
            }
            return new RespResult<object>(null, (int)RespCode.Fail, "无数据！");
        }
        
        public RespResult<object> GetRolebyParams(RoleQuery query)
        {
            var role = _roleRepository.GetRoleByParams(query).ToList();
            var total = role.Count;
            var temp = role.Skip((query.Page - 1 * query.PageSize)).Take(query.PageSize).ToList();
            var list = _mapper.Map<List<Role>, List<RoleDTO>>(temp);
            if (list != null && list.Count > 0)
            {
                return new RespResult<object>(new { total, list }, (int)RespCode.Success, "角色列表！");
            }
            return new RespResult<object>(null, (int)RespCode.Fail, "无数据！");
        }

        public RespResult<object> AddRole(RoleDTO roleDTO)
        {
            var flag = _roleRepository.Where(r => r.RoleName == roleDTO.RoleName && r.IsValid == 1).FirstOrDefault();
            if (flag == null)
            {
                var role = _mapper.Map<RoleDTO, Role>(roleDTO);
                role.CreateTime = DateTime.Now;
                role.UpdateTime = DateTime.Now;
                role.IsValid = 1;
                _roleRepository.Add(role);
                return new RespResult<object>(null, (int)RespCode.Success, "添加成功！");
            }
            return new RespResult<object>(null, (int)RespCode.Fail, "添加失败！");
        }

        public RespResult<object> DeleteRole(int[] ids)
        {
            if(ids != null && ids.Length > 0)
            {
                foreach(var id in ids)
                {
                    var role = _roleRepository.GetById(id);
                    role.IsValid = 0;
                    _roleRepository.Update(role);
                }
                return new RespResult<object>(null, (int)RespCode.Success, "删除成功！");
            }
            return new RespResult<object>(null, (int)RespCode.Fail, "删除失败！");
        }

        public RespResult<object> UpdateRole(RoleDTO roleDTO, int id)
        {
            // 1.校验数据
            var role = _roleRepository.Where(r => r.Id == id && r.IsValid == 1).FirstOrDefault();
            if(role != null && !string.IsNullOrEmpty(roleDTO.RoleName))
            {
                var flag = _roleRepository.Where(r => r.RoleName == roleDTO.RoleName && r.IsValid == 1).FirstOrDefault();
                if(flag == null)
                {
                    // 2.值映射
                    var temp = _mapper.Map(roleDTO,role);
                    // 3.默认值设置
                    temp.UpdateTime = DateTime.Now;
                    temp.Id = id;
                    // 4.修改
                    _roleRepository.Update(temp);
                    return new RespResult<object>(null, (int)RespCode.Success, "修改成功！");
                }
                return new RespResult<object>(null, (int)RespCode.Fail, "数据重复！");
            }
            return new RespResult<object>(null, (int)RespCode.Fail, "修改失败!");
        }
        public RespResult<object> Details(int id)
        {
            var role = _roleRepository.Where(r => r.IsValid == 1).Include(r=>r.Permission).FirstOrDefault();
            var roleDTO = _mapper.Map<Role, RoleDTO>(role);
            return new RespResult<object>(roleDTO, (int)RespCode.Success, "角色详情！");
        }
        
        public RespResult<object> AddGrant(int id, int[] mids)
        {
            var role = _roleRepository.Where(r => r.Id == id && r.IsValid == 1).FirstOrDefault();
            if (role != null && mids.Length>0)
            {
                var dbcontext = _roleRepository.DbContext;
                dbcontext.RemoveRange(dbcontext.Permission.Where(r => r.RoleId == id));
                var list = new List<Permission>();
                foreach (var mid in mids)
                {
                    var p = new Permission
                    {
                        ModuleId = mid,
                        RoleId = id,
                        CreateTime = DateTime.Now,
                        UpdateTime = DateTime.Now
                    };
                    list.Add(p);
                }
                dbcontext.AddRange(list);
                dbcontext.SaveChanges();
                return new RespResult<object>(null, (int)RespCode.Success, "授权成功！");
            }
            return new RespResult<object>(null, (int)RespCode.Fail, "授权失败！");
        }
    }
}
