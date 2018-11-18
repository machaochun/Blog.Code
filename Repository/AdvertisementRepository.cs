using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Blog.Core.Model.Models;
using IRepository;
using Repository.BASE;
using Repository.sugar;
using SqlSugar;

namespace Repository
{
    public class AdvertisementRepository : BaseRepository<Advertisement>,IAdvertisementRepository
    {
       
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

 


    }
}
