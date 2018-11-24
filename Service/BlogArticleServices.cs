using Blog.Core.Model.Models;
using Blog.Core.Model.ViewModels;
using Common;
using IRepository;
using IService;
using Service.BASE;
using System;
using System.Collections.Generic;
using System.Linq;
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
        /// 获取视图博客详情信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<BlogViewModels> GetBlogDetailsAsync(int id)
        {
            var bloglist = await _blogArticleRepository.Query(a => a.bID > 0, a => a.bID);
            var idmin = bloglist.FirstOrDefault() != null ? bloglist.FirstOrDefault().bID : 0;
            var idmax = bloglist.LastOrDefault() != null ? bloglist.LastOrDefault().bID : 1;
            var idminshow = id;
            var idmaxshow = id;

            BlogArticle blogArticle = new BlogArticle();

            blogArticle = (await _blogArticleRepository.Query(a => a.bID == idminshow)).FirstOrDefault();

            BlogArticle prevblog = new BlogArticle();


            while (idminshow > idmin)
            {
                idminshow--;
                prevblog = (await _blogArticleRepository.Query(a => a.bID == idminshow)).FirstOrDefault();
                if (prevblog != null)
                {
                    break;
                }
            }

            BlogArticle nextblog = new BlogArticle();
            while (idmaxshow < idmax)
            {
                idmaxshow++;
                nextblog = (await _blogArticleRepository.Query(a => a.bID == idmaxshow)).FirstOrDefault();
                if (nextblog != null)
                {
                    break;
                }
            }


            blogArticle.btraffic += 1;
            await _blogArticleRepository.Update(blogArticle, new List<string> { "btraffic" },null, " bID == " + blogArticle.bID.ToString());

            BlogViewModels models = new BlogViewModels()
            {
                bsubmitter = blogArticle.bsubmitter,
                btitle = blogArticle.btitle,
                bcategory = blogArticle.bcategory,
                bcontent = blogArticle.bcontent,
                btraffic = blogArticle.btraffic,
                bcommentNum = blogArticle.bcommentNum,
                bUpdateTime = blogArticle.bUpdateTime,
                bCreateTime = blogArticle.bCreateTime,
                bRemark = blogArticle.bRemark,
            };

            if (nextblog != null)
            {
                models.next = nextblog.btitle;
                models.nextID = nextblog.bID;
            }

            if (prevblog != null)
            {
                models.previous = prevblog.btitle;
                models.previousID = prevblog.bID;
            }
            return models;
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
