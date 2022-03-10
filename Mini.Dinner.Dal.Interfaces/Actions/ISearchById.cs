using Mini.Dinner.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mini.Dinner.Dal.Interfaces.Actions
{
    /// <summary>
    /// 封装了一个通过ID来查询业务对象的查询器。
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ISearchById<T> : IDapperOperation<T> where T : Entity
    {
        /// <summary>
        /// ID
        /// </summary>
        object Id { get; set; }
    }
}
