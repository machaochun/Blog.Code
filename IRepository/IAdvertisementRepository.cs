using Blog.Core.Model.Models;
using IRepository.BASE;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace IRepository
{
    public interface IAdvertisementRepository : IBaseRepository<Advertisement>
    {
        /// <summary>
        /// 求和
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <returns></returns>
        int Sum(int i, int j);
         
    }
}
