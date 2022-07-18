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
    [OpenApiTag("CusDevPlan", Description = "客户开发计划Api")]
    public class CusDevPlanController : ControllerBase
    {
        private readonly ICusDevPlanService _cusDevPlanService;
        private readonly ILogger _logger;

        public CusDevPlanController(ICusDevPlanService cusDevPlanService, ILogger logger)
        {
            _cusDevPlanService = cusDevPlanService;
            _logger = logger;
        }

        /// <summary>
        /// 客户开发计划列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult ListPage([FromQuery] CusDevPlanQuery query)
        {
            var res = new RespResult<object>(null);
            try
            {
                res = _cusDevPlanService.GetCusDevPlanByParams(query);
            } catch (Exception e)
            {
                _logger.Error(e.Message);
            }
            return new JsonResult(res);
        }

        /// <summary>
        /// 添加客户开发计划
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Add(CusDevPlanDTO cusDevPlanDTO)
        {
            var res = new RespResult<object>(null);
            try
            {
                res = _cusDevPlanService.AddCusDevPlan(cusDevPlanDTO);
            } catch (Exception e)
            {
                _logger.Error(e.Message);
            }
            return new JsonResult(res);
        }

        /// <summary>
        /// 修改客户开发计划
        /// </summary>
        /// <returns></returns>
        [HttpPut("{id}")]
        public IActionResult Update(CusDevPlanDTO cusDevPlanDTO, int id)
        {
            var res = new RespResult<object>(null);
            try
            {
                res = _cusDevPlanService.UpdateCusDevPlan(cusDevPlanDTO, id);
            } catch (Exception e)
            {
                _logger.Error(e.Message);
            }
            return new JsonResult(res);
        }

        /// <summary>
        /// 删除客户开发计划
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpDelete]
        public IActionResult Delete(int[] ids)
        {
            var res = new RespResult<object>(null);
            try
            {
                res = _cusDevPlanService.DeleteCusDevPlan(ids);
            } catch (Exception e)
            {
                _logger.Error(e.Message);
            }
            return new JsonResult(res);
        }

        /// <summary>
        /// 客户开发计划详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult Details(int id)
        {
            var res = new RespResult<object>(null);
            try
            {
                res = _cusDevPlanService.Details(id);
            }catch(Exception e)
            {
                _logger.Error(e.Message);
            }
            return new JsonResult(res);
        }
    }
}
