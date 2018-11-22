using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Code.AOP
{
    /// <summary>
    /// 简单的缓存接口，只有查询和添加，以后会进行扩展
    /// </summary>
    public interface ICaching
    {
        /// <summary>
        /// get
        /// </summary>
        /// <param name="cacheKey"></param>
        /// <returns></returns>
        object Get(string cacheKey);
        /// <summary>
        /// set
        /// </summary>
        /// <param name="cacheKey"></param>
        /// <param name="cacheValue"></param>
        void Set(string cacheKey, object cacheValue);
    }
}
