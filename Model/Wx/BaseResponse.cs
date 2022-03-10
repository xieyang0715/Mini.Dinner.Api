using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mini.Dinner.Model.Wx
{
    public  class BaseResponse
    {

        /// <summary>
        /// 错误码
        /// -1	系统繁忙，此时请开发者稍候再试	
        ///0	请求成功	
        ///40029	code 无效	
        ///45011	频率限制，每个用户每分钟100次	
        ///40226	高风险等级用户，小程序登录拦截 。风险等级详见用户安全解方案
        /// </summary>
        public int errCode { get; set; }
        /// <summary>
        /// 错误信息
        /// </summary>
        public string errMsg { get; set; }
    }
}
