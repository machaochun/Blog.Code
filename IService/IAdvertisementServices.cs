using Blog.Core.Model.Models;
using IService.BASE;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace IService
{
    public interface IAdvertisementServices : IBaseServices<Advertisement>
    {
        int Sum(int i, int j);      
        void CreateTableByEntity();
    }
}
