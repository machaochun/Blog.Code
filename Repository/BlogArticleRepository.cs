using Blog.Core.Model.Models;
using IRepository;
using Repository.BASE;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository
{
    public class BlogArticleRepository:BaseRepository<BlogArticle>,IBlogArticleRepository
    {
    }
}
