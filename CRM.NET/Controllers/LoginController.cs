using Common;
using IService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Model;
using Model.DTO;
using NSwag.Annotations;
using Serilog;
using System;

namespace CRM.NET.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [OpenApiTag("Login", Description = "登录Api")]
    public class LoginController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;

        public LoginController(IUserService userService, IConfiguration configuration, ILogger logger)
        {
            _userService = userService;
            _configuration = configuration;
            _logger = logger;
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="loginModel"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Login(UserLogin loginModel)
        {
            var res = new RespResult<UserLogin>(null);
            try
            {
                res = _userService.CheckLogin(loginModel, _configuration);
            }
            catch (Exception e)
            {
                _logger.Error(e.Message);
            }
            return new JsonResult(res);
        }

        [HttpGet("Captcha")]
        public IActionResult Captcha()
        {
            string code = VaildCode.CreateRandomCode(4); //验证码的字符为4个
            _configuration.GetSection("TempData")["ValidCode"] = code;//验证码存放在TempData中
            return File(VaildCode.CreateValidateGraphic(code), "image/Jpeg");
        }

        [HttpGet("Salt")]
        public IActionResult Salt()
        {
            var res = new RespResult<object>(null);
            try
            {
                res.Data = MD5Util.GetMD5Key();
                res.Message = "MD5Salt";
                res.Code = (int)RespCode.Success;
            }catch(Exception e)
            {
                _logger.Error(e.Message);
            }
            return new JsonResult(res);
        }
    }
}
