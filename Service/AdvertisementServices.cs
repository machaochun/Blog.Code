
using IService;
using IRepository;
using Repository;
using System;
using System.Collections.Generic;
using System.Text;
using Blog.Core.Model.Models;
using System.Linq.Expressions;

namespace Service
{
    public class AdvertisementServices : IAdvertisementServices
    {
        IAdvertisementRepository dal = new AdvertisementRepository();
        ICreaeteTalbeRepository tal = new CreaeteTalbeRepository();
        public int Sum(int i, int j)
        {
            return dal.Sum(i, j);
        } 
        public int Add(Advertisement model)
        {
            return dal.Add(model);
        } 
        public bool Delete(Advertisement model)
        {
            return dal.Delete(model);
        }
        public bool Update(Advertisement model)
        {
            return dal.Update(model);
        }
        public List<Advertisement> Query(Expression<Func<Advertisement, bool>> whereExpression)
        {
            return dal.Query(whereExpression);
        }

        public void CreateTableByEntity()
        {
            tal.CreateTableByEntity();
        }

    }
}
