using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Core.Model.Models;
using IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service;

namespace Blog.Code.Controllers
{
    /// <summary>
    /// Blog控制器
    /// </summary>
    [Produces("application/json")]
    [Route("api/Blog")]
  //  [Authorize(Policy ="Admin")]
    public class BlogController : Controller
    {
        /// <summary>
        /// 获取和
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <returns></returns>
        [HttpGet]
        public int GetSum(int i, int j)
        {
            IAdvertisementServices advertisementServices = new AdvertisementServices();
            return advertisementServices.Sum(i,j);

        }
        /// <summary>
        /// 根据ID获取广告信息
        /// </summary>
        /// <param name="id">广告ID</param>
        /// <returns></returns>
        [HttpGet("{id}", Name = "Get")]
        public List<Advertisement> GetAdvertisementInfo(int id)
        {
            IAdvertisementServices advertisementServices = new AdvertisementServices();
            return advertisementServices.Query(d => d.Id == id);
        }
        /// <summary>
        /// 创建表
        /// </summary>
        [HttpGet("aa")]
        public void CreateTableByEntity()
        {
            IAdvertisementServices advertisementServices = new AdvertisementServices();
            advertisementServices.CreateTableByEntity();

        }
    }
}