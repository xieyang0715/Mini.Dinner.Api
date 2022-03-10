using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mini.Dinner.Dal.Interfaces
{
    /// <summary>
    /// 提供了通过IoC方式创建<see cref="ITransaction"/>接口实例的接口。
    /// </summary>
    public interface ITransactionFactory
    {
        /// <summary>
        /// 创建<see cref="ITransaction"/>对象，该方法通过IoC模式有效降低了耦合度。
        /// </summary>
        /// <returns></returns>
        ITransaction BeginTransaction();
    }
}
