
using IService; 
using IRepository;
using Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service
{
    public class AdvertisementServices: IAdvertisementServices
    {
        IAdvertisementRepository dal = new AdvertisementRepository();
        public int Sum(int i, int j)
        {
            return dal.Sum(i, j);
        }
    }
}
