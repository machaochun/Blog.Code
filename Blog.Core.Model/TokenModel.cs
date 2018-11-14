using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Core.Model
{
    public class TokenModel
    {
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
       

    }
}
