using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Blog.Core.Model.Models;
using IRepository;
using Repository.sugar;
using SqlSugar;

namespace Repository
{
    public class AdvertisementRepository : IAdvertisementRepository
    {
        private SimpleClient<Advertisement> entityDB;
        internal SqlSugarClient Db { get; private set; }
        public DbContext Context { get; set; }

        public AdvertisementRepository()
        {
            DbContext.Init(BaseDBConfig.ConnectionString);
            Context = DbContext.GetDbContext();
            Db = Context.Db;
            entityDB = Context.GetEntityDB<Advertisement>(Db);
        }
        public int Add(Advertisement model)
        {
            var i = Db.Insertable(model).ExecuteReturnBigIdentity();
            return i.ObjToInt();
        }

        public bool Delete(Advertisement model)
        {
            var i = Db.Deleteable<Advertisement>(model).ExecuteCommand();
            return i > 0;
        }

        public List<Advertisement> Query(Expression<Func<Advertisement, bool>> whereExpression)
        {
            return entityDB.GetList(whereExpression);
        }

        /// <summary>
        /// 求和
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <returns></returns>
        public int Sum(int i, int j)
        {
            return i + j;
        }

        public bool Update(Advertisement model)
        {
            var i = Db.Updateable<Advertisement>(model).ExecuteCommand();
            return i > 0;
        }


    }
}
