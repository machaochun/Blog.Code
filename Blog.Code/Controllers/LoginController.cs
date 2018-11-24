using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Code.AuthHelper;
using Blog.Code.AuthHelper.OverWrite;
using Blog.Core.Model;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
 

namespace Blog.Code.Controllers
{
    /// <summary>
    /// Login
    /// </summary>
    [Produces("application/json")]
    [Route("api/[controller]")]
    [EnableCors("LimitRequests")]//就是这里
    public class LoginController : Controller
    {
        /// <summary>
        /// 登陆
        /// </summary>
        /// <param name="id"></param>
        /// <param name="sub"></param>
        /// <returns></returns>
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

        #region Token
        /// <summary>
        /// 获取JWT，并存入缓存
        /// </summary>
        /// <param name="id">用户Id</param>
        /// <param name="sub">身份</param>
        /// <param name="expiresSliding">相对过期时间，单位为分</param>
        /// <param name="expiresAbsoulute">绝对过期时间，单位为天</param>
        /// <returns></returns>
        [HttpGet]
        [Route("Token")]
        public JsonResult GetJWTStr(long id = 1, string sub = "Admin", int expiresSliding = 30, int expiresAbsoulute = 30)
        {
            TokenModel tokenModel = new TokenModel();
            tokenModel.Uid = id;
            tokenModel.Sub = sub;

            DateTime d1 = DateTime.Now;
            DateTime d2 = d1.AddMinutes(expiresSliding);
            DateTime d3 = d1.AddDays(expiresAbsoulute);
            TimeSpan sliding = d2 - d1;
            TimeSpan absoulute = d3 - d1;

            string jwtStr = BlogCoreToken.IssueJWT(tokenModel, sliding, absoulute);
            return Json(jwtStr);
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="callBack"></param>
        /// <param name="id"></param>
        /// <param name="sub"></param>
        /// <param name="expiresSliding"></param>
        /// <param name="expiresAbsoulute"></param>
        [HttpGet]
        [Route("jsonp")]
        public void Getjsonp(string callBack, long id = 1, string sub = "Admin", int expiresSliding = 30, int expiresAbsoulute = 30)
        {
            TokenModel tokenModel = new TokenModel();
            tokenModel.Uid = id;
            tokenModel.Sub = sub;

            DateTime d1 = DateTime.Now;
            DateTime d2 = d1.AddMinutes(expiresSliding);
            DateTime d3 = d1.AddDays(expiresAbsoulute);
            TimeSpan sliding = d2 - d1;
            TimeSpan absoulute = d3 - d1;

            string jwtStr = BlogCoreToken.IssueJWT(tokenModel, sliding, absoulute);

            //重要，一定要这么写
            string response = string.Format("\"value\":\"{0}\"", jwtStr);
            string call = callBack + "({" + response + "})";
            Response.WriteAsync(call);
        }
    }
}
