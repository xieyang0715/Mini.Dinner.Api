using Microsoft.AspNetCore.Http;
using Mini.Dinner.Dal.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mini.Dinner.Dal.Impl
{
    public class Transaction : ITransaction
    {
        private readonly IDbTransaction _dbTransaction;
        private readonly IDbConnection _connection;
        private readonly IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// 初始化一个<see cref="Transaction"/>对象，该对象实现了对提交数据库事务的封装。
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="HttpContextAccessor"></param>
        public Transaction(IDbTransaction transaction, IHttpContextAccessor httpContextAccessor)
        {
            _dbTransaction = transaction;
            _connection = _dbTransaction.Connection;
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// 提交数据库事务，并将数据库连接关闭。
        /// </summary>
        public void Commit()
        {
            _dbTransaction.Commit();

            this.CloseConnection();
        }

        /// <summary>
        /// 从挂起状态回滚事务，并将数据库连接关闭。
        /// </summary>
        public void Rollback()
        {
            _dbTransaction.Rollback();

            this.CloseConnection();
        }

        /// <summary>
        /// 如果数据库连接正常，且处于打开状态，那么关闭它。
        /// </summary>
        private void CloseConnection()
        {
            if (_connection != null && _connection.State == ConnectionState.Open)
            {
                _connection.Close();
            }

            //从当前上下文中移除已经提交或者回滚的事务对象。
            _httpContextAccessor.HttpContext.Items.Remove("Transaction");
        }

        public void Dispose()
        {
            _dbTransaction.Dispose();
        }
    }
}
