using Blog.Core.Model.Models;
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
        public async Task<List<BlogArticle>> getBlogs()
        {
            var bloglist = await _blogArticleRepository.Query(a => a.bID > 0, a => a.bID);
            return bloglist;
        }

       
    }
}
