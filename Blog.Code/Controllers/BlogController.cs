using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Blog.Core.Model.Models;
using Common.Redis;
using IService;
using Microsoft.AspNetCore.Mvc;

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

        private IAdvertisementServices _advertisementServices;
        private IBlogArticleServices _blogArticleServices;
        private IRedisCacheManager _redisCacheManager;
        /// <summary>
        /// 构造方法注入
        /// </summary>
        /// <param name="advertisementServices"></param>
        /// <param name="blogArticleServices"></param>
        /// <param name="redisCacheManager"></param>
        public BlogController(IAdvertisementServices advertisementServices, IBlogArticleServices blogArticleServices, IRedisCacheManager redisCacheManager)
        {
            _advertisementServices = advertisementServices;
            _blogArticleServices = blogArticleServices;
            _redisCacheManager = redisCacheManager;
        }
        /// <summary>
        /// 获取和
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <returns></returns>
        [HttpGet]
        public int GetSum(int i, int j)
        { 
            return _advertisementServices.Sum(i,j);

        }
        /// <summary>
        /// 根据ID获取广告信息
        /// </summary>
        /// <param name="id">广告ID</param>
        /// <returns></returns>
        [HttpGet("{id}", Name = "Get")]
        public async Task<List<Advertisement>> GetAdvertisementInfo(int id)
        { 
            return await _advertisementServices.Query(d => d.Id == id);
        }
        /// <summary>
        /// 创建表
        /// </summary>
        [HttpGet("aa")]
        public void CreateTableByEntity()
        { 
            _advertisementServices.CreateTableByEntity();

        }

        /// <summary>
        /// 获取博客列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetBlogs")]
        public async Task<List<BlogArticle>> GetBlogs()
        {

            List<BlogArticle> blogArticleList = new List<BlogArticle>();

            if (_redisCacheManager.Get<object>("Redis.Blog") != null)
            {
                blogArticleList = _redisCacheManager.Get<List<BlogArticle>>("Redis.Blog");
            }
            else
            {
                blogArticleList= await _blogArticleServices.Query(d => d.bID == 1);
                _redisCacheManager.Set("Redis.Blog", blogArticleList, TimeSpan.FromHours(2));

            }
            return blogArticleList;            
        }
    }
}