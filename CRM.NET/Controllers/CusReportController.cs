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
    [OpenApiTag("CusReport", Description ="客户报告Api")]
    public class CusReportController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly ICusLossService _cusLossService;
        private readonly ICusServerService _cusServerService;
        private readonly ILogger _logger;

        public CusReportController(ICustomerService customerService, ILogger logger, ICusLossService cusLossService, ICusServerService cusServerService)
        {
            _customerService = customerService;
            _cusLossService = cusLossService;
            _cusServerService = cusServerService;
            _logger = logger;
        }

        [HttpGet("CusContri")]
        public IActionResult CusContri([FromQuery]CusContributionQuery query)
        {
            var res = new RespResult<object>(null);
            try
            {
                res = _customerService.GetCusContributionByParam(query);
            }catch(Exception e)
            {
                _logger.Error(e.Message);
            }
            return new JsonResult(res);
        }

        [HttpGet("CusMake")]
        public IActionResult CusMake()
        {
            var res = new RespResult<object>(null);
            try
            {
                res = _customerService.GetCusMake();
            }
            catch (Exception e)
            {
                _logger.Error(e.Message);
            }
            return new JsonResult(res);
        }

        [HttpGet("ServerMake")]
        public IActionResult ServerMake()
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

        [HttpGet("CusLoss")]
        public IActionResult CusLoss([FromQuery]CusLossQuery query)
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
    }
}
