using IService;
using Microsoft.AspNetCore.Mvc;
using Model;
using Model.DTO;
using NSwag.Annotations;
using Serilog;
using System;

namespace CRM.NET.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [OpenApiTag("Role", Description = "角色Api")]
    public class RoleController : ControllerBase
    {
        public readonly IRoleService _roleService;
        public readonly ILogger _logger;

        public RoleController(IRoleService roleService, ILogger logger)
        {
            _roleService = roleService;
            _logger = logger;
        }

        /// <summary>
        /// 角色列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult List()
        {
            var res = new RespResult<object>(null);
            try
            {
                res = _roleService.GetAll();
            } catch (Exception e)
            {
                _logger.Error(e.Message);
            }
            return new JsonResult(res);
        }

        /// <summary>
        /// 分页条件列表 -- 角色名
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpGet("PageList")]
        public IActionResult PageListL([FromQuery]RoleQuery query)
        {
            var res = new RespResult<object>(null);
            try
            {
                res = _roleService.GetRolebyParams(query);
            } catch (Exception e)
            {
                _logger.Error(e.Message);
            }
            return new JsonResult(res);
        }

        /// <summary>
        /// 添加角色
        /// </summary>
        /// <param name="roleDTO"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult AddRole([FromBody] RoleDTO roleDTO)
        {
            var res = new RespResult<object>(null);
            try
            {
                res = _roleService.AddRole(roleDTO);
            } catch (Exception e)
            {
                _logger.Error(e.Message);
            }
            return new JsonResult(res);
        }

        /// <summary>
        /// 修改角色
        /// </summary>
        /// <param name="roleDTO"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public IActionResult UpdateRole(int id, [FromBody] RoleDTO roleDTO)
        {
            var res = new RespResult<object>(null);
            try
            {
                res = _roleService.UpdateRole(roleDTO, id);
            } catch (Exception e)
            {
                _logger.Error(e.Message);
            }
            return new JsonResult(res);
        }

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpDelete]
        public IActionResult DeleteRole(int[] ids)
        {
            var res = new RespResult<object>(null);
            try
            {
                res = _roleService.DeleteRole(ids);
            } catch (Exception e)
            {
                _logger.Error(e.Message);
            }
            return new JsonResult(res);
        }

        /// <summary>
        /// 角色详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult Details(int id)
        {
            var res = new RespResult<object>(null);
            try
            {
                res = _roleService.Details(id);
            }
            catch(Exception e)
            {
                _logger.Error(e.Message);
            }
            return new JsonResult(res);
        }
        
        /// <summary>
        /// 添加授权
        /// </summary>
        /// <param name="id"></param>
        /// <param name="mids"></param>
        /// <returns></returns>
        [HttpPost("grant/{id}")]
        public IActionResult AddGrant(int id,[FromBody]int[] mids)
        {
            var res = new RespResult<object>(null);
            try
            {
                res = _roleService.AddGrant(id,mids);
            }
            catch (Exception e)
            {
                _logger.Error(e.Message);
            }
            return new JsonResult(res);
        }
    }
}
