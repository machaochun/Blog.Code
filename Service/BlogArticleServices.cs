using Blog.Core.Model.Models;
using Common;
using IRepository;
using IService;
using Service.BASE;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class BlogArticleServices :BaseServices<BlogArticle>,IBlogArticleServices
    {
        public IBlogArticleRepository _blogArticleRepository;

        public BlogArticleServices(IBlogArticleRepository blogArticleRepository)
        {
            base.baseDal = blogArticleRepository;
            _blogArticleRepository = blogArticleRepository;
        }
        /// <summary>
        /// 获取博客列表
        /// </summary>
        /// <returns></returns>
      //  [Caching(AbsoluteExpiration =10)]//增加特性
        public async Task<List<BlogArticle>> getBlogs()
        {
      //      var connect = Appsettings.app(new string[] { "AppSettings", "RedisCaching", "ConnectionString" });
            var bloglist = await _blogArticleRepository.Query(a => a.bID > 0, a => a.bID);
            return bloglist;
        }

       
    }
}
