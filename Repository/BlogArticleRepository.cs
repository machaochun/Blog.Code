using Blog.Core.Model.Models;
using Blog.Core.Model.ViewModels;
using IRepository;
using Repository.BASE;
using Repository.CodeTable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class BlogArticleRepository:BaseRepository<BlogArticle>,IBlogArticleRepository
    { 
    }
}
