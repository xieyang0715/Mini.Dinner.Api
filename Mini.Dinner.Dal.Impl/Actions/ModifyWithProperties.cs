using Dapper;
using Mini.Dinner.Dal.Interfaces.Actions;
using Mini.Dinner.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dapper.Contrib.Extensions.SqlMapperExtensions;

namespace Mini.Dinner.Dal.Impl.Actions
{
    /// <summary>
    /// 实现了根据指定的对象属性修改对象实体
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ModifyWithProperties<T> : DapperOperationBase<bool>, IModifyWithProperties<T> where T : Entity
    {
        /// <summary>
        /// 待修改的对象实体
        /// </summary>
        public T UpdateEntity { get; set; }

        /// <summary>
        /// 指定修改的属性集合
        /// </summary>
        public string[] UpdateProperties { get; set; }

        /// <summary>
        /// 支持的事务
        /// </summary>
        public IDbTransaction TargetTransaction { get; set; }

        public override bool Execute()
        {
            var proxy = UpdateEntity as IProxy;
            if (proxy != null)
            {
                if (!proxy.IsDirty)
                {
                    return false;
                }
            }
            Type type = typeof(T);
            System.Collections.Generic.List<System.Reflection.PropertyInfo> keyProperties = DapperExtensions.KeyPropertiesCache(type);
            if (!keyProperties.Any())
            {
                throw new ArgumentException("Entity must have at least one [Key] property");
            }

            var name = DapperExtensions.GetTableName(type);

            var sb = new StringBuilder();
            sb.AppendFormat("update `{0}` set ", name);

            for (var i = 0; i < UpdateProperties.Count(); i++)
            {
                sb.AppendFormat("`{0}` = @{1},", UpdateProperties[i], UpdateProperties[i]);
            }
            var newstr = sb.ToString().Substring(0, sb.Length - 1);
            sb.Clear();
            sb.Append(newstr);
            sb.Append(" where ");

            for (var i = 0; i < keyProperties.Count(); i++)
            {
                System.Reflection.PropertyInfo property = keyProperties.ElementAt(i);
                sb.AppendFormat("{0} = @{1}", property.Name, property.Name);
                if (i < keyProperties.Count() - 1)
                {
                    sb.AppendFormat(" and ");
                }
            }
            var updated = Connection.Execute(sb.ToString(), UpdateEntity, Transaction);

            return updated > 0;
        }
    }
}
