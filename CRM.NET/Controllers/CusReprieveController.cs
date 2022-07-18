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
    [OpenApiTag("CusReprieve", Description = "客户暂留Api")]
    public class CusReprieveController : ControllerBase
    {
        private readonly ICusReprieveService _cusReprieveService;
        private readonly ILogger _logger;

        public CusReprieveController(ICusReprieveService cusReprieveService, ILogger logger)
        {
            _cusReprieveService = cusReprieveService;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult ListPage([FromQuery] CusReprieveQuery query)
        {
            var res = new RespResult<object>(null);
            try
            {
                res = _cusReprieveService.GetCusReprieveByParam(query);
            } catch (Exception e)
            {
                _logger.Error(e.Message);
            }
            return new JsonResult(res);
        }

        [HttpPost]
        public IActionResult AddCusReprieve(CusReprieveDTO cusReprieveDTO)
        {
            var res = new RespResult<object>(null);
            try
            {
                res = _cusReprieveService.AddCusReprieve(cusReprieveDTO);
            } catch (Exception e)
            {
                _logger.Error(e.Message);
            }
            return new JsonResult(res);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCusReprieve(int id, [FromBody] CusReprieveDTO cusReprieveDTO)
        {
            var res = new RespResult<object>(null);
            try
            {
                res = _cusReprieveService.UpdateCusReprieve(id, cusReprieveDTO);
            }
            catch (Exception e)
            {
                _logger.Error(e.Message);
            }
            return new JsonResult(res);
        }

        [HttpDelete]
        public IActionResult DeleteCusReprieve(int id)
        {
            var res = new RespResult<object>(null);
            try
            {
                res = _cusReprieveService.DeleteCusReprieve(id);
            }
            catch (Exception e)
            {
                _logger.Error(e.Message);
            }
            return new JsonResult(res);
        }
    }
}
