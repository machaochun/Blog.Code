using Blog.Core.Model;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.sugar
{
    public class DbContext
    {
        private static string _connectionString;
        private static DbType _dbType;
        private SqlSugarClient _db;

        /// <summary>
        /// 设置连接字符串
        /// </summary>
        public static string ConnectionString
        {
            get { return _connectionString; }
            set { _connectionString = value; }
        }
        /// <summary>
        /// 数据库类型
        /// </summary>
        public static DbType DbType
        {
            get { return _dbType; }
            set { _dbType = value; }
        }
        /// <summary>
        /// 数据库连接对象
        /// </summary>
        public SqlSugarClient Db
        {
            get { return _db; }
            set { _db = value; }
        }


        /// <summary>
        /// 数据库上下文实例（自动关闭连接）
        /// Blog.Core 
        /// </summary>
        public static DbContext Context
        {
            get
            {
                return new DbContext();
            }

        }
        /// <summary>
        /// 构造函数 单例模式
        /// </summary>
        private DbContext()
        {
            if (string.IsNullOrEmpty(_connectionString))
                throw new ArgumentNullException(ErrorMassageStatic.DBConnectionStringIsNULLError);
            _db = new SqlSugarClient(new ConnectionConfig()
            {
                ConnectionString = _connectionString, //连接字符串
                DbType = _dbType,//设置数据库类型
                IsAutoCloseConnection =true,//自动释放数据务，如何存在事务，在事务结束后释放
                IsShardSameThread =true,//设为true相同线程是同一个SqlConnection
                ConfigureExternalServices =new ConfigureExternalServices()
                {

                },
                //用于一些全局设置
                MoreSettings = new ConnMoreSettings()
                {
                    IsAutoRemoveDataCache= true
                }
            });
        }

        /// <summary>
        /// 功能描述:构造函数
        /// 作　　者:Blog.Core
        /// </summary>
        /// <param name="blnIsAutoCloseConnection">是否自动关闭连接</param>
        private DbContext(bool blnIsAutoCloseConnection)
        {
            if (string.IsNullOrEmpty(_connectionString))
                throw new ArgumentNullException(ErrorMassageStatic.DBConnectionStringIsNULLError);
            _db = new SqlSugarClient(new ConnectionConfig()
            {
                ConnectionString = _connectionString,
                DbType = _dbType,
                IsAutoCloseConnection = blnIsAutoCloseConnection,
                IsShardSameThread = true,
                ConfigureExternalServices = new ConfigureExternalServices()
                {
                    //DataInfoCacheService = new HttpRuntimeCache()
                },
                MoreSettings = new ConnMoreSettings()
                {
                    //IsWithNoLockQuery = true,
                    IsAutoRemoveDataCache = true
                }
            });
        }
    }
}
