using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Core.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class TokenModel
    {
        /// <summary>
        /// 
        /// </summary>
        public TokenModel()
        {
            this.Uid = 0;
        }
        /// <summary>
        /// 用户Id
        /// </summary>
        public long Uid { get; set; }
        /// <summary>
        /// 角色
        /// </summary>
        public string  Role { get; set; }
        /// <summary>
        /// 职能
        /// </summary>
        public string Work { get; set; }
        /// <summary>
        /// name
        /// </summary>
        public string Uname { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Sub { get; set; }
    }
}
