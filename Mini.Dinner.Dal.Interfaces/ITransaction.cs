using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mini.Dinner.Dal.Interfaces
{
    /// <summary>
    /// 提供了提交和回滚事务的通用接口。
    /// </summary>
    public interface ITransaction : IDisposable
    {
        /// <summary>
        /// 提交数据库事务。
        /// </summary>
        void Commit();

        /// <summary>
        /// 从挂起状态回滚事务。
        /// </summary>
        void Rollback();
    }
}
