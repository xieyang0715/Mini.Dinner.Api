using Microsoft.Extensions.Configuration;
using Mini.Dinner.Model.Wx;
using Mini.Dinner.Services.Common.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mini.Dinner.Services.Common.Impl
{
    public class WxService : IWxService
    {
        public IConfiguration MyProperty { get; set; }
        public IConfiguration Configuration { get; set; }
        public WxService(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }
        public string WxLogin(string code)
        {
            var code2SessionUrl = Configuration["wx:code2SessionUrl"];
            var appId = Configuration["wx:appId"];
            var appSecret = Configuration["wx:appSecret"];
            //var url = $"{code2SessionUrl}?appid={appId}&secret={appSecret}&js_code={code}&grant_type=authorization_code";
            var result = HttpRequestServices.DoGet(code2SessionUrl, new
            {
                appid = appId,
                secret = appSecret,
                js_code = code,
                grant_type = "authorization_code"
            });
            Code2SessionResponse code2SessionResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<Code2SessionResponse>(result);
            return "";
        }
        public string GetPhoneNumber(string code)
        {
            var token = GetAccessToken();

            var phoneNumberUrl = Configuration["wx:phoneNumber"];
            var result = HttpRequestServices.DoPost($"{phoneNumberUrl}?access_token={token}", new
            {
                code = code
            }, "application/json");
            //Code2SessionResponse code2SessionResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<Code2SessionResponse>(result);
            return result;
        }
        private string GetAccessToken()
        {
            var accessTokenUrl = Configuration["wx:accessToken"];
            var appId = Configuration["wx:appId"];
            var appSecret = Configuration["wx:appSecret"];
            var result = HttpRequestServices.DoGet(accessTokenUrl, new
            {
                appid = appId,
                secret = appSecret,
                grant_type = "client_credential"
            });
            AccessTokenResponse accessTokenResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<AccessTokenResponse>(result);
            return accessTokenResponse.access_token;
        }
    }
}
