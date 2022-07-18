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
    [OpenApiTag("CusServer", Description = "客户服务Api")]
    public class CusServerController : ControllerBase
    {
        private readonly ICusServerService _cusServerService;
        private readonly ILogger _logger;

        public CusServerController(ICusServerService cusServerService, ILogger logger)
        {
            _cusServerService = cusServerService;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult ListPage([FromQuery]CusServerQuery query)
        {
            var res = new RespResult<object>(null);
            try
            {
                res = _cusServerService.GetCusServerByParam(query);
            }catch(Exception e)
            {
                _logger.Error(e.Message);
            }
            return new JsonResult(res);
        }
        [HttpPost]
        public IActionResult Add([FromBody]CusServerDTO cusServerDTO)
        {
            var res = new RespResult<object>(null);
            try
            {
                res = _cusServerService.AddCusServer(cusServerDTO);
            }
            catch (Exception e)
            {
                _logger.Error(e.Message);
            }
            return new JsonResult(res);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody]CusServerDTO cusServerDTO)
        {
            var res = new RespResult<object>(null);
            try
            {
                res = _cusServerService.UpdateCusServerState(id, cusServerDTO);
            }
            catch (Exception e)
            {
                _logger.Error(e.Message);
            }
            return new JsonResult(res);
        }

        [HttpGet("ServerType")]
        public IActionResult GetServerType()
        {
            var res = new RespResult<object>(null);
            try
            {
                res = _cusServerService.GetServerType();
            }catch(Exception e)
            {
                _logger.Error(e.Message);
            }
            return new JsonResult(res);
        }

        [HttpGet("ServerMake")]
        public IActionResult CusServerMake()
        {
            var res = new RespResult<object>(null);
            try
            {
                res = _cusServerService.ServerMake();
            }
            catch (Exception e)
            {
                _logger.Error(e.Message);
            }
            return new JsonResult(res);
        }

        [HttpDelete]
        public IActionResult Delete(int[] ids)
        {
            var res = new RespResult<object>(null);
            try
            {
                res = _cusServerService.DeleteCusServer(ids);
            }
            catch (Exception e)
            {
                _logger.Error(e.Message);
            }
            return new JsonResult(res);
        }
    }
}
