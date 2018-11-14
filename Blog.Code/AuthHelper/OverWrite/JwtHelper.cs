
using Blog.Core;
using Blog.Core.Model;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Code.AuthHelper.OverWrite
{
    /// <summary>
    /// 令牌类
    /// </summary>
    public class JwtHelper
    {
        public static string secretKey { get; set; } = "sdfsdfsrty45634kkhllghtdgdfss345t678fs";
        /// <summary>
        /// 获取JWT字符串
        /// </summary>
        /// <param name="tokenModel"></param>
        /// <returns></returns>
        public static string IssueJWT(TokenModel tokenModel)
        {
            var dateTime = DateTime.UtcNow;
            var claims = new Claim[]
                {
                    new Claim(JwtRegisteredClaimNames.Jti,tokenModel.Uid.ToString()),//JWT ID,JWT的唯一标识
                    new Claim("Role",tokenModel.Role),//角色
                    new Claim(JwtRegisteredClaimNames.Iat,dateTime.ToString(),ClaimValueTypes.Integer64)//Issued At，JWT颁发的时间，采用标准unix时间，用于验证过期
                };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));//使用私钥进行签名加密
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            JwtSecurityToken jwt = new JwtSecurityToken(
                issuer: "Blog.Core",//签发者
                claims: claims,//声明集合
                expires: dateTime.AddHours(2),//指定token的生命周期，unix时间戳格式,非必须
                signingCredentials: creds
                );
            var jwtHandle = new JwtSecurityTokenHandler();//生成最后的JWT字符串
            var encodedJwt = jwtHandle.WriteToken(jwt);

            return encodedJwt;
        }
        /// <summary>
        /// 解析 jwt
        /// </summary>
        /// <param name="jwtStr"></param>
        /// <returns></returns>
        public static TokenModel SerializeJWT(string jwtStr)
        {
            var jwtHandler = new JwtSecurityTokenHandler();
            var jwtToken = jwtHandler.ReadJwtToken(jwtStr);
            object role = new object();

            try
            {
                jwtToken.Payload.TryGetValue("Role", out role);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            var tm = new TokenModel
            {
                Uid = (jwtToken.Id).ObjToInt(),
                Role = role != null ? role.ObjToString() : ""
            };

            return tm;
        }
    }
}
