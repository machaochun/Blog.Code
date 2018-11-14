using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    [Authorize(Policy ="Admin")]
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

    }
}