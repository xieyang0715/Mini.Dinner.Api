using Microsoft.Extensions.Configuration;
using Mini.Dinner.Dal.Interfaces;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using Mini.Dinner.Utill;

namespace Mini.Dinner.Dal.Impl
{
    public class TransactionWrapper : DependenceWrapper
    {
        public TransactionWrapper()
        {
            //Dapper.SqlMapper.AddTypeMap(typeof(Uuid), DbType.String);
        }

        private static readonly object Mutex = new object();

        /// <summary>
        /// 数据库连接字符串名称
        /// </summary>
        protected virtual string ConnectionName => "MySqlConnection";

        /// <summary>
        /// 数据库类型
        /// </summary>
        protected virtual string Type => "MySql";

        /// <summary>
        /// 获取或设置一个用作封装数据库事务的<see cref="IDbTransaction"/>对象，
        /// <para>该对象由业务层开启与提交，作用于数据实现层。</para>
        /// </summary>
        public IDbTransaction Transaction => Bind<IDbTransaction>(o => o);

        /// <summary>
        /// 获取或设置一个用作与数据库连接的<see cref="IDbConnection"/>对象。
        /// </summary>
        public IDbConnection Connection => Bind<IDbConnection>(
            o =>
            {

                var conStr = RSAEncryptServer.ParametDecryptMore(Configuration.GetConnectionString(ConnectionName));
                return o ?? (Type == "SqlServer" ? new SqlConnection(conStr).As<IDbConnection>() : new MySqlConnection(conStr).As<IDbConnection>());
            }
        );

        /// <summary>
        /// 开始一个数据库事物。
        /// </summary> 
        /// <returns></returns>
        public ITransaction BeginTransaction()
        {
            if (Connection.State == ConnectionState.Closed)
            {
                Connection.Open();
            }

            Bind<IDbTransaction>(
                o =>
                {
                    if (o != null)
                    {
                        throw new Exception("已经存在一个挂起的事务");
                    }

                    return Connection.BeginTransaction();
                });

            return new Transaction(Transaction, HttpContextAccessor);
        }

        /// <summary>
        /// 为每次请求绑定唯一的数据库连接对象和数据库事务对象。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="func"></param>
        /// <returns></returns>
        private T Bind<T>(Func<T, T> func) where T : class
        {
            lock (Mutex)
            {
                var itemKey = typeof(T) == typeof(IDbConnection) ? ConnectionName : $"{ConnectionName}_Transaction";
                T con = HttpContextAccessor == null || HttpContextAccessor.HttpContext == null ? null :
                    (HttpContextAccessor.HttpContext.Items.ContainsKey(itemKey) ? HttpContextAccessor.HttpContext.Items[itemKey] : null)?.As<T>();

                T item = func.Invoke(con);

                if (HttpContextAccessor == null || HttpContextAccessor.HttpContext == null)
                {
                    return item;
                }

                HttpContextAccessor.HttpContext.Items[itemKey] = item;

                return HttpContextAccessor.HttpContext.Items[itemKey]?.As<T>();
            }
        }
    }
}
