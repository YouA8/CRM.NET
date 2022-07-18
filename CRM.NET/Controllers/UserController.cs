using IService;
using Microsoft.AspNetCore.Mvc;
using Model;
using Model.DTO;
using NSwag.Annotations;
using Serilog;
using System;
using System.Collections.Generic;

namespace CRM.NET.Controllers
{
    /// <summary>
    /// 用户
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [OpenApiTag("User", Description = "用户Api")] //Nwag注释
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger _logger;

        public UserController(IUserService userService, ILogger logger)
        {
            _userService = userService;
            _logger = logger;
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="pwdChange"></param>
        /// <returns></returns>
        [HttpPatch]
        public IActionResult PwdChange(PwdChange pwdChange)
        {
            var res = new RespResult<PwdChange>(null);
            try
            {
                res = _userService.ChangeUserPwd(pwdChange);
            }
            catch (Exception e)
            {
                _logger.Error(e.Message);
            }
            return new JsonResult(res);
        }

        /// <summary>
        /// 用户列表
        /// </summary>
        /// <param name="userQuery"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult ListPageUser([FromQuery]UserQuery userQuery)
        {
            var res = new RespResult<object>(null);
            try
            {
                res = _userService.GetUserByParams(userQuery);
            }catch(Exception e)
            {
                _logger.Error(e.Message);
            }
            return new JsonResult(res);
        }

        /// <summary>
        /// 销售列表
        /// </summary>
        /// <returns></returns>
        [HttpGet("Sale")]
        public IActionResult ListPageSale()
        {
            var res = new RespResult<List<UserDTO>>(null);
            try
            {
                res = _userService.SaleList();
            }catch(Exception e)
            {
                _logger.Error(e.Message);
            }
            return new JsonResult(res);
        }
        
        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="userDTO"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult AddUser(UserDTO userDTO)
        {
            var res = new RespResult<object>(null);
            try
            {
                res = _userService.AddUser(userDTO);
            }catch(Exception e)
            {
                _logger.Error(e.Message);
            }
            return new JsonResult(res);
        }

        /// <summary>
        /// 修改用户
        /// </summary>
        /// <param name="userDTO"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public IActionResult UpdateUser([FromBody]UserDTO userDTO,int id)
        {
            var res = new RespResult<object>(null);
            try
            {
                res = _userService.UpdateUser(userDTO, id);
            }catch(Exception e)
            {
                _logger.Error(e.Message);
            }
            return new JsonResult(res);
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpDelete]
        public IActionResult DeleteUser(int[] ids)
        {
            var res = new RespResult<object>(null);
            try
            {
                res = _userService.DeleteUser(ids);
            }
            catch (Exception e)
            {
                _logger.Error(e.Message);
            }
            return new JsonResult(res);
        }

        /// <summary>
        /// 用户详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult Details(int id)
        {
            var details = _userService.UserDetails(id);
            return new JsonResult(details);
        }
    }
}
