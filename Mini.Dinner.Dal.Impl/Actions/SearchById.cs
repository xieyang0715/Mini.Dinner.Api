using DapperExtensions;
using Mini.Dinner.Dal.Interfaces.Actions;
using Mini.Dinner.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mini.Dinner.Dal.Impl.Actions
{
    /// <summary>
    /// 实现了一个通用的通过ID查询数据对象的接口。
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SearchById<T> : DapperOperationBase<T>, ISearchById<T> where T : Entity
    {
        public object Id { get; set; }

        public override T Execute()
        {
            return Connection.Get<T>(Id, Transaction);
        }
    }
}
