
using Mini.Dinner.Dal.Interfaces;
using Mini.Dinner.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mini.Dinner.Dal.Impl
{
    public abstract class DapperOperationBase<TResult> : TransactionWrapper, IDapperOperation<TResult>
    {
        /// <summary>
        /// 初始化一个DapperOperationBase泛型对象，该构造方法只能被子类调用。
        /// </summary>
        protected DapperOperationBase()
        {
        }

        /// <summary>
        /// 获取或设置查询结果排序的字段
        /// </summary>
        public string OrderBy { get; set; }

        /// <summary>
        /// 获取或设置查询结果排序方向（升序或降序）
        /// </summary>
        public Sort Sort { get; set; }

        /// <summary>
        /// 执行数据填充完毕的SQL语句对象并返回结果。
        /// </summary>
        /// <returns>SQL语句对象执行结果</returns>
        public abstract TResult Execute();
    }
}
