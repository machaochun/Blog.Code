using Blog.Core.Model.Models;
using IRepository.BASE;
using System;
using System.Collections.Generic;
using System.Text;

namespace IRepository
{
    public interface IBlogArticleRepository:IBaseRepository<BlogArticle>
    {
    }
}
