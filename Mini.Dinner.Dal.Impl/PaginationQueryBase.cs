using Dapper;
using Mini.Dinner.Dal.Interfaces;
using Mini.Dinner.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Mini.Dinner.Dal.Impl
{
    public abstract class PaginationQueryBase<T> : DapperOperationBase<PagedResult<T>>, IPaginationQuery<T>
    {
        #region Regex

        private static readonly Regex rxColumns =
           new Regex(@"\A\s*SELECT\s+((?:\((?>\((?<depth>)|\)(?<-depth>)|.?)*(?(depth)(?!))\)|.)*?)(?<!,\s+)\bFROM\b",
               RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Singleline | RegexOptions.Compiled);

        private static readonly Regex rxOrderBy =
            new Regex(
                @"\bORDER\s+BY\s+(?:\((?>\((?<depth>)|\)(?<-depth>)|.?)*(?(depth)(?!))\)|[\w\(\)\.])+(?:\s+(?:ASC|DESC))?(?:\s*,\s*(?:\((?>\((?<depth>)|\)(?<-depth>)|.?)*(?(depth)(?!))\)|[\w\(\)\.])+(?:\s+(?:ASC|DESC))?)*",
                RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Singleline | RegexOptions.Compiled);

        private static Regex rxDistinct = new Regex(@"\ADISTINCT\s",
            RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Singleline | RegexOptions.Compiled);

        #endregion

        #region Constructors

        /// <summary>
        /// 初始化一个泛型<see cref="PaginationQueryBase{T}"/>对象，该构造方法只能由子类调用
        /// </summary>
        protected PaginationQueryBase()
        {
            PageIndex = 0;
            PageSize = 10;
        }

        /// <summary>
        /// 初始化一个泛型<see cref="PaginationQueryBase{T}"/>对象，该构造方法只能由子类调用
        /// </summary>
        /// <param name="pageIndex">页码，不小于1</param>
        /// <param name="pageSize">分页显示的内容条数</param>
        protected PaginationQueryBase(int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
        }

        #endregion

        #region Implementations of IPaginationQuery<T>

        /// <summary>
        /// 获取或设置页码，页码不小于1
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// 获取或设置分页显示的内容条数
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 获取或设置分页查询参数
        /// </summary>
        public object Param { get; set; }

        #endregion

        #region Implementation of DapperOperationBase<PagedResult<T>>


        /// <summary>
        /// 执行数据填充完毕的SQL语句对象并返回结果
        /// </summary>
        /// <returns>分页查询结果</returns>
        public override PagedResult<T> Execute()
        {
            var sqlCount = string.Empty;
            var sqlPage = string.Empty;
            var query = GetQuery();

            BuildMySqlageQueries((PageIndex - 1) * PageSize, PageSize, query, out sqlCount, out sqlPage);

            System.Collections.Generic.IEnumerable<int> qur = Connection.Query<int>(sqlCount, Param);
            var result = new PagedResult<T>
            {
                PageIndex = PageIndex,
                PageSize = PageSize,
                TotalItems = qur.Count() == 0 ? 0 : qur.First()
            };
            result.TotalPages = result.TotalItems / PageSize;
            result.Results = Connection.Query<T>(sqlPage, Param);

            if (result.TotalItems % PageSize != 0)
            {
                result.TotalPages++;
            }

            return result;
        }

        /// <summary>
        /// 分页查询的sql语句。
        /// </summary>
        /// <returns></returns>
        public abstract string GetQuery();

        #endregion

        #region Non-Public Methods

        private void BuildMySqlageQueries(long skip, long take, string sql, out string sqlCount,
            out string sqlPage)
        {
            if (!SplitSqlForPaging(sql, out sqlCount, out string sqlSelectRemoved, out string sqlOrderBy))
            {
                throw new Exception("Unable to parse SQL statement for paged query");
            }

            sqlPage = string.Format(sql + " limit {0},{1}", skip, take);
        }

        private bool SplitSqlForPaging(string sql, out string sqlCount, out string sqlSelectRemoved,
            out string sqlOrderBy)
        {
            sqlSelectRemoved = sqlCount = sqlOrderBy = null;

            Match m = rxColumns.Match(sql);
            if (!m.Success)
            {
                return false;
            }

            Group g = m.Groups[1];
            sqlSelectRemoved = sql.Substring(g.Index);

            if (rxDistinct.IsMatch(sqlSelectRemoved))
            {
                sqlCount = sql.Substring(0, g.Index) + "COUNT(" + m.Groups[1].ToString().Trim() + ") " +
                           sql.Substring(g.Index + g.Length);
            }
            else
            {
                sqlCount = sql.Substring(0, g.Index) + "COUNT(*) " + sql.Substring(g.Index + g.Length); //"select count(1) from (" + sql + ") as tempcount";
            }

            m = rxOrderBy.Match(sqlCount);

            if (m.Success)
            {
                g = m.Groups[0];
                sqlOrderBy = g.ToString();
                sqlCount = sqlCount.Substring(0, g.Index) + sqlCount.Substring(g.Index + g.Length);
            }

            return true;
        }

        #endregion
    }
}
