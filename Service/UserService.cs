using IRepository;
using IService;
using Model;
using Model.DTO;
using System.Collections.Generic;
using System.Linq;
using Common;
using Microsoft.Extensions.Configuration;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;

namespace Service
{
    public class UserService : BaseService<User>, IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper) 
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public RespResult<UserLogin> CheckLogin(UserLogin userLogin, IConfiguration configuration)
        {
            var vaildcode = configuration.GetSection("TempData")["ValidCode"];
            // 1.判断数据或用户名是否空
            if (userLogin != null && !string.IsNullOrEmpty(userLogin.UserName) && userLogin.VaildCode.Equals(vaildcode))
            {
                // 2.查找校验用户
                var user = _userRepository.Where(u => u.UserName == userLogin.UserName && u.IsValid == 1).Include(u => u.Role).FirstOrDefault();
                var md5pwd = MD5Util.MD5Encrypt32(user?.UserPwd);
                if (user != null && userLogin.PassWord == md5pwd)
                {
                    var role = user.Role;
                    // 3.查询用户角色
                    if (role != null)
                    {
                        var token = JwtToken.GetToken(userLogin.UserName, role.RoleName, configuration);
                        var respm = new UserLogin()
                        {
                            UserName = userLogin.UserName,
                            Token = token
                        };
                        return new RespResult<UserLogin>(respm, (int)RespCode.Success, "登录成功！");
                    }
                    return new RespResult<UserLogin>(null, (int)RespCode.Fail, "用户没有分配角色，不能登录！");
                }
            }
            return new RespResult<UserLogin>(null, (int)RespCode.Fail, "用户名、密码或验证码错误！");
        }

        public RespResult<PwdChange> ChangeUserPwd(PwdChange pwdChange)
        {
            // 1.判断更改密码模型是否为空
            if(pwdChange != null && !string.IsNullOrEmpty(pwdChange.UserName))
            {
                // 2.查找用户
                var user = _userRepository.Where(u => u.UserName == pwdChange.UserName && u.IsValid == 1).FirstOrDefault();
                var md5pwd = MD5Util.MD5Encrypt32(pwdChange.OldUserPwd);
                // 3.判断密码正确性
                if (user != null && user.UserPwd == pwdChange.OldUserPwd)
                {
                    user.UserPwd = pwdChange.NewUserPwd;
                    // 4.更改密码
                    _userRepository.Update(user, true);
                    return new RespResult<PwdChange>(null, (int)RespCode.Success, "密码修改成功！");
                }
                return new RespResult<PwdChange>(null, (int)RespCode.Fail, "旧密码不正确！");
            }
            return new RespResult<PwdChange>(null, (int)RespCode.Fail, "数据错误！");
        }

        public RespResult<List<UserDTO>> SaleList()
        {
            var temp = _userRepository.Entities.Where(u => u.Role.Id == 2 && u.IsValid == 1).ToList();
            var list = _mapper.Map<List<User>, List<UserDTO>>(temp);
            if (list != null && list.Count >= 0)
            {
                return new RespResult<List<UserDTO>>(list, (int)RespCode.Success, "销售角色列表");
            }
            return new RespResult<List<UserDTO>>(null, (int)RespCode.Fail, "无数据！");
        }

        public RespResult<List<UserDTO>> ManagerList()
        {
            var temp = _userRepository.Entities.Where(u => u.Role.Id == 3 && u.IsValid == 1).ToList();
            var list = _mapper.Map<List<User>, List<UserDTO>>(temp);
            if (list != null && list.Count >= 0)
            {
                return new RespResult<List<UserDTO>>(list, (int)RespCode.Success, "客户经理角色列表");
            }
            return new RespResult<List<UserDTO>>(null, (int)RespCode.Fail, "无数据！");
        }

        public RespResult<object> GetUserByParams(UserQuery query)
        {
            // ef core 对sql优化问题 sqlserver2008不支持 只能先查到内存 后分页
            // 1.获取多条件查询结果
            var user = _userRepository.GetUserByParams(query).Include(u=>u.Role).ToList();
            // 2.分页查询
            var total = user.Count;
            var temp = user.Skip((query.Page - 1) * query.PageSize).Take(query.PageSize).ToList();
            var list = _mapper.Map<List<User>, List<UserDTO>>(temp);
            if (list != null && list.Count > 0)
            {
                return new RespResult<object>(new { total, list }, (int)RespCode.Success, "用户列表");
            }
            return new RespResult<object>(null, (int)RespCode.Fail, "无数据");
        }

        public RespResult<object> AddUser(UserDTO userDTO)
        {
            // 1.数据校验
            if (ValueVaild(userDTO, -1))
            {
                // 2.值映射
                var user = _mapper.Map<UserDTO, User>(userDTO);
                // 3.默认值设置
                user.CreateTime = DateTime.Now;
                user.UpdateTime = DateTime.Now;
                user.IsValid = 1;
                // 4.设置业务值
                //设置默认密码为123546 使用md5加密
                user.UserPwd = MD5Util.MD5Encrypt32("123456");
                // 5.添加
                _userRepository.Add(user);
                return new RespResult<object>(null, (int)RespCode.Success, "添加成功！");
            }
            return new RespResult<object>(null, (int)RespCode.Fail, "姓名、手机、邮箱数据有误！");
        }

        public RespResult<object> UpdateUser(UserDTO userDTO, int id)
        {
            // 1.验证数值
            var user = _userRepository.Where(u => u.Id == id && u.IsValid == 1).FirstOrDefault();
            if (ValueVaild(userDTO,user.Id))
            {
                if(user != null)
                {
                    // 2.值映射
                    var temp = _mapper.Map(userDTO, user);
                    // 3.默认值设置
                    temp.UpdateTime = DateTime.Now;
                    // 4.更新
                    _userRepository.Add(temp);
                    return new RespResult<object>(null, (int)RespCode.Success, "修改成功！");
                }
                return new RespResult<object>(null, (int)RespCode.Fail, "数据有误！");
            }
            return new RespResult<object>(null, (int)RespCode.Fail, "姓名、手机、邮箱数据有误！");
        }

        public RespResult<object> DeleteUser(int[] ids)
        {
            if(ids != null && ids.Length > 0)
            {
                foreach(var id in ids)
                {
                    var user = _userRepository.GetById(id);
                    user.IsValid = 0;
                    _userRepository.Update(user);
                }
                return new RespResult<object>(null, (int)RespCode.Success, "删除成功！");
            }
            return new RespResult<object>(null, (int)RespCode.Fail, "删除失败！");
        }

        public RespResult<UserDTO> UserDetails(int id)
        {
            var user = _userRepository.Where(u => u.Id == id && u.IsValid == 1).Include(u => u.Role).FirstOrDefault();
            var userinfo = _mapper.Map<User, UserDTO>(user);
            return new RespResult<UserDTO>(userinfo, (int)RespCode.Success, "用户详情");
        }

        /// <summary>
        /// 对数据进行校验
        /// </summary>
        /// <param name="userDTO"></param>
        /// <returns></returns>
        private bool ValueVaild(UserDTO userDTO,int id)
        {
            if (!string.IsNullOrEmpty(userDTO.UserName) && !string.IsNullOrEmpty(userDTO.Phone) && !string.IsNullOrEmpty(userDTO.Email) && IsValueUtil.IsPhone(userDTO.Phone) && IsValueUtil.IsEmail(userDTO.Email))
            {
                var user = _userRepository.Where(u => u.UserName.Equals(userDTO.UserName) && u.IsValid == 1).FirstOrDefault();
                if (id == -1)
                {
                    if (user == null)
                    {
                        return true;
                    }
                }
                else
                {
                    if(user == null && user.Id != id)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
