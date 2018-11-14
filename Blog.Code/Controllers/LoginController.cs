using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Code.AuthHelper.OverWrite;
using Blog.Core.Model;
using Microsoft.AspNetCore.Mvc;
 

namespace Blog.Code.Controllers
{
    /// <summary>
    /// Login
    /// </summary>
    [Route("api/[controller]")]
    public class LoginController : Controller
    {
        [HttpGet]
        [Route("Token2")]
        public JsonResult GetJwtStr(long id = 1, string sub = "Admin")
        {
            TokenModel tokenModel = new TokenModel();
            tokenModel.Uid = id;
            tokenModel.Role = sub;

            string jwtStr = JwtHelper.IssueJWT(tokenModel);
            return Json(jwtStr);
        }
    }
}
