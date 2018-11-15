using IRepository;
using Repository.CodeTable;
using Repository.sugar;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository
{
    public class CreaeteTalbeRepository: ICreaeteTalbeRepository
    { 
        internal SqlSugarClient Db { get; private set; }
        public DbContext Context { get; set; }

        public CreaeteTalbeRepository()
        {
            DbContext.Init(BaseDBConfig.ConnectionString);
            Context = DbContext.GetDbContext();
            Db = Context.Db; 
        }
        public void CreateTableByEntity()
        {
            Context.CreateTableByEntity(false, new List<AdvertisementTable> { new AdvertisementTable() {
                Id=1,
                Createdate=DateTime.Now 
            } });
        }
    }
}
