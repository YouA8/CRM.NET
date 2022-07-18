using Microsoft.AspNetCore.Mvc;
using IService;
using Model.DTO;
using Model;
using System;
using Serilog;
using NSwag.Annotations;
using Microsoft.AspNetCore.Authorization;

namespace CRM.NET.Controllers
{
    /// <summary>
    /// 营销机会
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [OpenApiTag("SaleChance", Description = "营销机会Api")]
    public class SaleChanceController : ControllerBase
    {
        private readonly ISaleChanceService _saleChanceService;
        private readonly ILogger _logger;

        public SaleChanceController(ISaleChanceService saleChanceService, ILogger logger)
        {
            _saleChanceService = saleChanceService;
            _logger = logger;
        }

        /// <summary>
        /// 营销机会列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public IActionResult ListPage([FromQuery] SaleChanceQuery query)
        {
            var res = new RespResult<object>(null);
            try
            {
                res = _saleChanceService.GetSaleChanceByParams(query);
            }
            catch (Exception e)
            {
                _logger.Error(e.Message);
            }
            return new JsonResult(res);
        }

        /// <summary>
        /// 添加营销机会
        /// </summary>
        /// <param name="saleChanceDTO"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Add(SaleChanceDTO saleChanceDTO)
        {
            var res = new RespResult<object>(null);
            try
            {
                res = _saleChanceService.AddSaleChance(saleChanceDTO);
            } catch (Exception e)
            {
                _logger.Error(e.Message);
            }
            return new JsonResult(res);
        }

        /// <summary>
        /// 修改营销机会
        /// </summary>
        /// <param name="saleChanceDTO"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public IActionResult Update([FromBody] SaleChanceDTO saleChanceDTO, int id)
        {
            var res = new RespResult<object>(null);
            try
            {
                res = _saleChanceService.UpdateSaleChance(saleChanceDTO, id);
            } catch (Exception e)
            {
                _logger.Error(e.Message);
            }
            return new JsonResult(res);
        }
        /// <summary>
        /// 修改开发状态
        /// </summary>
        /// <param name="saleChanceDTO"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPatch("{id}")]
        public IActionResult UpdateDevResult([FromBody] SaleChanceDTO saleChanceDTO, int id)
        {
            var res = new RespResult<object>(null);
            try
            {
                res = _saleChanceService.UpdateSaleChanceDevResult(saleChanceDTO, id);
            }
            catch (Exception e)
            {
                _logger.Error(e.Message);
            }
            return new JsonResult(res);
        }

        /// <summary>
        /// 删除营销机会
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpDelete]
        public IActionResult Delete(int[] ids)
        {
            var res = new RespResult<object>(null);
            try
            {
                res = _saleChanceService.DeleteSaleChance(ids);
            }catch(Exception e)
            {
                _logger.Error(e.Message);
            }
            return new JsonResult(res);
        }

        /// <summary>
        /// 营销机会详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult Details(int id)
        {
            var res = new RespResult<SaleChanceDTO>(null);
            try
            {
                res = _saleChanceService.Details(id);
            }
            catch (Exception e)
            {
                _logger.Error(e.Message);
            }
            return new JsonResult(res);
        }
    }
}
