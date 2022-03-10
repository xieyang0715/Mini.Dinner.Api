using Mini.Dinner.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mini.Dinner.Dal.Interfaces.Actions
{
    /// <summary>
    /// 提供了根据指定的属性修改对象实体
    /// </summary>
    /// <typeparam name="T">待修改的实体类型，且该实体必须继承自<see cref="Entity"/>。</typeparam>
    public interface IModifyWithProperties<T> : IDapperOperation<bool> where T : Entity
    {
        /// <summary>
        /// 待修改的对象实体
        /// </summary>
        T UpdateEntity { get; set; }

        /// <summary>
        /// 指定修改的属性集合
        /// </summary>
        string[] UpdateProperties { get; set; }

        /// <summary>
        /// 支持的事务
        /// </summary>
        IDbTransaction TargetTransaction { get; set; }
    }
}
