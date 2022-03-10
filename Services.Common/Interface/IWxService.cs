using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mini.Dinner.Services.Common.Interface
{
    public interface IWxService
    {
        string WxLogin(string code);

        string GetPhoneNumber(string code);
    }
}
