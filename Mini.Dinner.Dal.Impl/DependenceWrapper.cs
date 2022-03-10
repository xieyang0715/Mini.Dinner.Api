using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mini.Dinner.Dal.Impl
{
    public class DependenceWrapper
    {
        /// <summary>
        /// 应用程序配置属性。
        /// </summary>
        public IConfiguration Configuration { get; set; }

        /// <summary>
        /// 有关单个HTTP请求的所有特定于HTTP的信息
        /// </summary>
        public IHttpContextAccessor HttpContextAccessor { get; set; }
    }
}
