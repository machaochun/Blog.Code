using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Code.AOP
{
    /// <summary>
    /// 实例化缓存接口ICaching
    /// </summary>
    public class MemoryCaching : ICaching
    {
        private IMemoryCache _cache;
        /// <summary>
        /// 通过构造方法获取
        /// </summary>
        /// <param name="cache"></param>
        public MemoryCaching(IMemoryCache cache)
        {
            _cache = cache;
        }
        /// <summary>
        /// get
        /// </summary>
        /// <param name="cacheKey"></param>
        /// <returns></returns>
        public object Get(string cacheKey)
        {
            return _cache.Get(cacheKey);
        }
        /// <summary>
        /// set
        /// </summary>
        /// <param name="cacheKey"></param>
        /// <param name="cacheValue"></param>
        public void Set(string cacheKey, object cacheValue)
        {
            _cache.Set(cacheKey, cacheValue, TimeSpan.FromSeconds(7200));
        }
    }
}
