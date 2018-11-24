using Blog.Core.Model.Models;
using Blog.Core.Model.ViewModels;
using IService.BASE;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IService
{
    public interface IBlogArticleServices:IBaseServices<BlogArticle>
    {
        Task<List<BlogArticle>> getBlogs();

        Task<BlogViewModels> GetBlogDetailsAsync(int id);
    }
}
