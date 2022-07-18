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
    [OpenApiTag("CusLoss", Description = "客户流失Api")]
    public class CusLossController : ControllerBase
    {
        private readonly ICusLossService _cusLossService;
        private readonly ILogger _logger;

        public CusLossController(ICusLossService cusLossService, ILogger logger)
        {
            _cusLossService = cusLossService;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult PageList([FromQuery] CusLossQuery query)
        {
            var res = new RespResult<object>(null);
            try
            {
                res = _cusLossService.GetCusLossByParams(query);
            }
            catch (Exception e)
            {
                _logger.Error(e.Message);
            }
            return new JsonResult(res);
        }

        [HttpPatch("{id}")]
        public IActionResult UpdateCusLossState(int id, [FromBody]string lossReason)
        {
            var res = new RespResult<object>(null);
            try
            {
                res = _cusLossService.UpdateCusLossState(id, lossReason);
            }catch(Exception e)
            {
                _logger.Error(e.Message);
            }
            return new JsonResult(res);
        }
    }
}
