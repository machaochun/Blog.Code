using Blog.Core.Model;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Blog.Code.AuthHelper.OverWrite
{
    /// <summary>
    /// Token验证授权中间件
    /// </summary>
    public class JwtTokenAuth
    {

        private readonly RequestDelegate _next;

        public JwtTokenAuth(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext httpContext)
        {
            if (!httpContext.Request.Headers.ContainsKey("Authorization"))
            {
                return _next(httpContext);
            }
            var tokenStr = httpContext.Request.Headers["Authorization"].ToString();

            TokenModel tokenModel = JwtHelper.SerializeJWT(tokenStr); //序列化token，获取授权

            //授权 注意这个可以添加多个角色声明，请注意这是一个 list
            var claimList = new List<Claim>();
            var claim = new Claim(ClaimTypes.Role, tokenModel.Role);
            claimList.Add(claim);
            var identity = new ClaimsIdentity(claimList);
            var principal = new ClaimsPrincipal(identity);
            httpContext.User = principal;

            return _next(httpContext);
        }
    }
}
