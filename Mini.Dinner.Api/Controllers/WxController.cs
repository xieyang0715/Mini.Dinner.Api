using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mini.Dinner.Services.Common.Interface;

namespace Mini.Dinner.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WxController : ControllerBase
    {
        public IWxService WxService { get; set; }

        [HttpGet(Name = "WxLogin")]
        public string Get(string code)
        {
            return WxService.WxLogin(code);
        }

        [HttpGet("getPhoneNumber")]
        public string GetPhoneNumber(string code)
        {
            return WxService.GetPhoneNumber(code);
        }
    }
}
