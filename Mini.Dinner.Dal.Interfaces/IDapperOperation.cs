using Mini.Dinner.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mini.Dinner.Dal.Interfaces
{
    /// <summary>
    /// 提供了代表Dapper SQL语句对象的基本接口1。
    /// </summary>
    /// <typeparam name="TResult">执行结果的数据集类型</typeparam>
    public interface IDapperOperation<out TResult> : IDapperOperation
    {
        /// <summary>
        /// 获取或设置查询结果排序的字段
        /// </summary>
        string OrderBy { get; set; }

        /// <summary>
        /// 获取或设置查询结果排序方向（升序或降序）
        /// </summary>
        Sort Sort { get; set; }

        /// <summary>
        /// 执行数据填充完毕的SQL语句对象并返回结果11。
        /// </summary>
        /// <returns>SQL语句对象执行结果</returns>
        TResult Execute();
    }
}
