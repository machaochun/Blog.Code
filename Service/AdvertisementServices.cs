
using IService;
using IRepository;
using Blog.Core.Model.Models;
using Service.BASE;

namespace Service
{
    public class AdvertisementServices :BaseServices<Advertisement>, IAdvertisementServices
    {


        public IAdvertisementRepository _advertisementRepository;
        public ICreaeteTalbeRepository _creaeteTalbeRepository;
        public AdvertisementServices(IAdvertisementRepository advertisementRepository, ICreaeteTalbeRepository creaeteTalbeRepository)
        {
            _advertisementRepository = advertisementRepository;
            _creaeteTalbeRepository = creaeteTalbeRepository;
            base.baseDal = _advertisementRepository;
        } 
        public int Sum(int i, int j)
        {
            return _advertisementRepository.Sum(i, j);
        }  

        public void CreateTableByEntity()
        {
            _creaeteTalbeRepository.CreateTableByEntity();
        }

    }
}
