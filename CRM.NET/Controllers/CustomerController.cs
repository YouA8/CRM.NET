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
    [OpenApiTag("Customer", Description = "客户Api")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly ILogger _logger;

        public CustomerController(ICustomerService customerService, ILogger logger)
        {
            _customerService = customerService;
            _logger = logger;
        }

        /// <summary>
        /// 客户列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult ListPage([FromQuery]CustomerQuery query)
        {
            var res = new RespResult<object>(null);
            try
            {
               res = _customerService.GetCustomerByParam(query);
            }
            catch (Exception e)
            {
                _logger.Error(e.Message);
            }
            return new JsonResult(res);
        }

        /// <summary>
        /// 添加客户
        /// </summary>
        /// <param name="customerDTO"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult AddCustomer([FromBody]CustomerDTO customerDTO)
        {
            var res = new RespResult<object>(null);
            try
            {
                res = _customerService.AddCustomer(customerDTO);
            }catch(Exception e)
            {
                _logger.Error(e.Message);
            }
            return new JsonResult(res);
        }

        /// <summary>
        /// 修改客户
        /// </summary>
        /// <param name="customerDTO"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public IActionResult UpdateCustomer([FromBody]CustomerDTO customerDTO, int id)
        {
            var res = new RespResult<object>(null);
            try
            {
               res = _customerService.UpdateCustomer(customerDTO, id);
            }
            catch (Exception e)
            {
                _logger.Error(e.Message);
            }
            return new JsonResult(res);
        }

        /// <summary>
        /// 删除客户
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpDelete]
        public IActionResult DeleteCustomer(int[] ids)
        {
            var res = new RespResult<object>(null);
            try
            {
               res = _customerService.DeleteCustomer(ids);
            }
            catch (Exception e)
            {
                _logger.Error(e.Message);
            }
            return new JsonResult(res);
        }

        /// <summary>
        /// 客户构成
        /// </summary>
        /// <returns></returns>
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

       /// <summary>
       ///  客户贡献
       /// </summary>
       /// <param name="query"></param>
       /// <returns></returns>
        [HttpGet("CusCon")]
        public IActionResult CusContribution([FromQuery]CusContributionQuery query)
        {
            var res = new RespResult<object>(null);
            try
            {
                res = _customerService.GetCusContributionByParam(query);
            }
            catch (Exception e)
            {
                _logger.Error(e.Message);
            }
            return new JsonResult(res);
        }
    }
}
