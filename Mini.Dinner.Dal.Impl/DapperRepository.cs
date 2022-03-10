using DapperExtensions;
using Mini.Dinner.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Mini.Dinner.Dal.Interfaces;
using System.Reflection;
using Mini.Dinner.Dal.Impl.Actions;
using Mini.Dinner.Dal.Interfaces.Actions;

namespace Mini.Dinner.Dal.Impl
{
    public class DapperRepository<T> : TransactionWrapper, IRepository<T> where T : Entity
    {
        #region Constructors

        /// <summary>
        /// 初始化一个实现数据库基本操作的DapperRepository对象。
        /// </summary>
        public DapperRepository()
        {

        }

        #endregion

        /// <summary>
        /// 创建业务对象。
        /// </summary>
        /// <param name="item">待添加的业务对象</param>
        public void Create(T item)
        {
            Connection.Insert(item, Transaction);
        }

        /// <summary>
        /// 删除指定的业务对象。
        /// </summary>
        /// <param name="item">待删除的业务对象</param>
        /// <returns>删除成功，返回true，否则，返回false</returns>
        public bool Remove(T item)
        {
            return Connection.Delete(item, Transaction);
        }

        /// <summary>
        /// 修改指定的业务对象。
        /// </summary>
        /// <param name="item">待修改的业务对象</param>
        public bool Modify(T item)
        {
            return Connection.Update(item, Transaction);
        }

        /// <summary>
        /// 按照指定的对象属性修改业务对象
        /// </summary>
        /// <param name="item">待修改的业务对象</param>
        /// <param name="memberExpressions">修改的对象属性集合</param>
        public bool Modify(T item, params Expression<Func<T, object>>[] memberExpressions)
        {
            var modifyPropertys = memberExpressions.Select(o => GetMemberInfo(o.Body).Name).ToArray();

            ModifyWithProperties<T> action = InitializerProvider<ModifyWithProperties<T>, IModifyWithProperties<T>>();
            action.UpdateEntity = item;
            action.UpdateProperties = modifyPropertys;
            action.TargetTransaction = Transaction;

            return action.Execute();
        }




        /// <summary>
        /// 实例化仓储元素，并统一注入组建依赖。
        /// </summary>
        /// <typeparam name="TM">元素实现类型</typeparam>
        /// <typeparam name="TI">元素接口</typeparam>
        /// <returns></returns>
        protected TM InitializerProvider<TM, TI>() where TM : DependenceWrapper, TI, new()
        {
            var obj = new TM()
            {
                Configuration = Configuration,
                HttpContextAccessor = HttpContextAccessor
            };

            return obj;
        }

        /// <summary>
        /// 获取访问字段或属性
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        private MemberInfo GetMemberInfo(Expression expression)
        {
            MemberExpression memberExpression = null;
            if (expression.NodeType == ExpressionType.Convert)
            {
                var body = (UnaryExpression)expression;
                memberExpression = body.Operand as MemberExpression;
            }
            else if (expression.NodeType == ExpressionType.MemberAccess)
            {
                memberExpression = expression as MemberExpression;
            }
            else
            {
                throw new ArgumentException("Not a member access", "expression");
            }

            return memberExpression.Member;
        }


        /// <summary>
        /// 根据ID获取<see cref="T"/>对象
        /// </summary>
        /// <returns></returns>
        public ISearchById<T> SearchById()
        {
            return InitializerProvider<SearchById<T>, ISearchById<T>>();
        }
        /// <summary>
        /// 获取指定id对象
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public T SearchById(Uuid id)
        {
            ISearchById<T> action = SearchById();
            action.Id = id;
            return action.Execute();

        }
    }
}
