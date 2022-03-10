using Mini.Dinner.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Mini.Dinner.Dal.Interfaces
{
    /// <summary>
    /// 提供了实现Repository模式的数据库基本操作接口
    /// </summary>
    /// <typeparam name="T">集合中的业务实体类型</typeparam>
    public interface IRepository<T> : IRepository, ITransactionFactory where T : Entity
    {
        /// <summary>
        /// 创建业务对象。
        /// </summary>
        /// <param name="item">待添加的业务对象</param>
        void Create(T item);

        /// <summary>
        /// 删除指定的业务对象。
        /// </summary>
        /// <param name="item">待删除的业务对象</param>
        /// <returns>删除成功，返回true，否则，返回false</returns>
        bool Remove(T item);

        /// <summary>
        /// 修改指定的业务对象。
        /// </summary>
        /// <param name="item">待修改的业务对象</param>
        bool Modify(T item);

        /// <summary>
        /// 按照指定的对象属性修改业务对象
        /// </summary>
        /// <param name="item">待修改的业务对象</param>
        /// <param name="memberExpressions">修改的对象属性集合</param>
        bool Modify(T item, params Expression<Func<T, object>>[] memberExpressions);


        /// <summary>
        /// 获取指定id对象
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T SearchById(Uuid id);
    }
}
