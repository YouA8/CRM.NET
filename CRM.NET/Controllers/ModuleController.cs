using IService;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using Serilog;
using Model;
using System;
using Model.DTO;

namespace CRM.NET.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [OpenApiTag("Module",Description ="资源Api")]
    public class ModuleController : ControllerBase
    {
        public readonly IModuleService _moduleService;
        public readonly ILogger _logger;

        public ModuleController(IModuleService moduleService, ILogger logger)
        {
            _moduleService = moduleService;
            _logger = logger;
        }

        /// <summary>
        /// 资源列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult List()
        {
            var res = new RespResult<object>(null);
            try
            {
                res = _moduleService.GetAll();
            }catch(Exception e)
            {
                _logger.Error(e.Message);
            }
            return new JsonResult(res);
        }

        /// <summary>
        /// 添加资源
        /// </summary>
        /// <param name="moduleDTO"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult AddModule(ModuleDTO moduleDTO)
        {
            var res = new RespResult<object>(null);
            try
            {
                res = _moduleService.AddModule(moduleDTO);
            }catch(Exception e)
            {
                _logger.Error(e.Message);
            }
            return new JsonResult(res);
        }

        /// <summary>
        /// 删除资源
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public IActionResult DeleteModule(int  id)
        {
            var res = new RespResult<object>(null);
            try
            {
                res = _moduleService.DeleteModule(id);
            }catch(Exception e)
            {
                _logger.Error(e.Message);
            }
            return new JsonResult(res);
        }

        /// <summary>
        /// 修改资源
        /// </summary>
        /// <param name="id"></param>
        /// <param name="moduleDTO"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public IActionResult UpdateModule(int id, [FromBody]ModuleDTO moduleDTO)
        {
            var res = new RespResult<object>(null);
            try
            {
                res = _moduleService.UpdateModule(id, moduleDTO);
            }catch(Exception e)
            {
                _logger.Error(e.Message);
            }
            return new JsonResult(res);
        }

        /// <summary>
        /// 根据用户名获取资源
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        [HttpGet("UserModule")]
        public IActionResult GetUserModule(string username)
        {
            var res = new RespResult<object>(null);
            try
            {
                res = _moduleService.GetUserModule(username);
            }
            catch (Exception e)
            {
                _logger.Error(e.Message);
            }
            return new JsonResult(res);
        }
    }
}
