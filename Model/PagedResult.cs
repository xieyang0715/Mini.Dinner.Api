using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mini.Dinner.Model
{
    /// <summary>
    /// 作为可以将数据库查询结果进行分页显示的业务对象。
    /// </summary>
    public class PagedResult<T>
    {
        #region Constructors

        /// <summary>
        /// 初始化一个泛型<see cref="PagedResult"/>对象，该构造方法只能由子类调用。
        /// </summary>
        public PagedResult()
        {

        }

        /// <summary>
        /// 初始化一个泛型<see cref="PagedResult"/>对象，该构造方法只能由子类调用。
        /// </summary>
        /// <param name="totalItems">所有信息条数</param>
        /// <param name="totalPages">所有页数</param>
        /// <param name="pageIndex">不小于1的页码</param>
        /// <param name="pageSize">分页显示的内容条数</param>
        /// <param name="results">保存分页数据集的泛型IEnumerable对象</param>
        public PagedResult(Int32 totalItems, Int32 totalPages, Int32 pageIndex, Int32 pageSize, IEnumerable<T> results)
        {
            TotalItems = totalItems;
            TotalPages = totalPages;
            PageIndex = pageIndex;
            PageSize = pageSize;
            Results = results;
        }

        #endregion

        #region Properties

        /// <summary>
        /// 获取或设置所有信息条数。
        /// </summary>
        public int TotalItems { get; set; }

        /// <summary>
        /// 获取或设置所有页数。
        /// </summary>
        public Int32 TotalPages { get; set; }

        /// <summary>
        /// 获取或设置页码，页码不小于1。
        /// </summary>
        public Int32 PageIndex { get; set; }

        /// <summary>
        /// 获取或设置分页显示的内容条数。
        /// </summary>
        public Int32 PageSize { get; set; }

        /// <summary>
        /// 获取或设置保存分页数据集的泛型<see cref="System.Collections.IEnumerable"/>对象。
        /// </summary>
        public IEnumerable<T> Results { get; set; }

        #endregion
    }
}
