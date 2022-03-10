using Mini.Dinner.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mini.Dinner.Dal.Interfaces
{
    /// <summary>
    /// 提供了根据数据库查询结果来计算分页的业务对象接口。
    /// </summary>
    public interface IPaginationQuery<T> : IDapperOperation<PagedResult<T>>
    {
        /// <summary>
        /// 获取或设置页码，页码不小于1。
        /// </summary>
        int PageIndex { get; set; }

        /// <summary>
        /// 获取或设置分页显示的内容条数。
        /// </summary>
        int PageSize { get; set; }

    }
}
