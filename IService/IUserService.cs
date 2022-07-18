using Microsoft.Extensions.Configuration;
using Model;
using Model.DTO;
using System.Collections.Generic;

namespace IService
{
    public interface IUserService : IBaseService<User>
    {
        /// <summary>
        /// 检查用户
        /// </summary>
        /// <param name="userLogin"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public RespResult<UserLogin> CheckLogin(UserLogin userLogin, IConfiguration configuration);

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="pwdChange"></param>
        /// <returns></returns>
        public RespResult<PwdChange> ChangeUserPwd(PwdChange pwdChange);

        /// <summary>
        /// 销售角色列表
        /// </summary>
        /// <returns></returns>
        public RespResult<List<UserDTO>> SaleList();

        /// <summary>
        /// 客户经理角色
        /// </summary>
        /// <returns></returns>
        public RespResult<List<UserDTO>> ManagerList();

        /// <summary>
        /// 分页多条件查询 -- 姓名，邮箱，电话
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public RespResult<object> GetUserByParams(UserQuery query);

        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="userDTO"></param>
        /// <returns></returns>
        public RespResult<object> AddUser(UserDTO userDTO);

        /// <summary>
        /// 修改用户
        /// </summary>
        /// <param name="userDTO"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public RespResult<object> UpdateUser(UserDTO userDTO, int id);

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public RespResult<object> DeleteUser(int[] ids);

        /// <summary>
        /// 用户详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public RespResult<UserDTO> UserDetails(int id);
    }
}
