using Castle.DynamicProxy;
using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Code.AOP
{
    /// <summary>
    /// 面向切面的缓存使用
    /// </summary>
    public class BlogCacheAOP : IInterceptor
    {
        //通过注入的方式，把缓存操作接口通过构造函数注入
        private ICaching _cache;
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="cache"></param>
        public BlogCacheAOP(ICaching cache)
        {
            _cache = cache;
        }
        /// <summary>
        /// Intercept方法是拦截的关键所在，也是IInterceptor接口中的唯一定义
        /// </summary>
        public void Intercept(IInvocation invocation)
        {

            var method = invocation.MethodInvocationTarget ?? invocation.Method;

            //对当前方法的特性验证
            var qCachingAttribute = method.GetCustomAttributes(true).FirstOrDefault(x => x.GetType() == typeof(CachingAttribute)) as CachingAttribute;

            if (qCachingAttribute != null)
            {


                //获取自定义缓存键
                var cacheKey = CustomCacheKey(invocation);

                //根据Key获取相应缓存值
                var cacheValue = _cache.Get(cacheKey);
                if (cacheValue != null)
                {
                    //将当前获取到的缓存值，赋值给当前执行方法
                    invocation.ReturnValue = cacheValue;
                    return;
                }
                //去执行当前的方法
                invocation.Proceed();
                //存入缓存
                if (!string.IsNullOrWhiteSpace(cacheKey))
                {
                    _cache.Set(cacheKey, invocation.ReturnValue);
                }
            }
            else
            {
                //直接执行被拦截的方法
                invocation.Proceed();
            }
        }

        //自定义缓存键
        private string CustomCacheKey(IInvocation invocation)
        {
            var typeName = invocation.TargetType.Name;
            var methodName = invocation.Method.Name;
            var methodArguments = invocation.Arguments.Select(GetArgumentValue).Take(3).ToList();//获取参数列表，最多三个

            string key = $"{typeName}:{methodName}:";
            foreach (var param in methodArguments)
            {
                key += $"{param}:";
            }

            return key.TrimEnd(':');
        }
        //object 转 string
        private string GetArgumentValue(object arg)
        {
            if (arg is int || arg is long || arg is string)
                return arg.ToString();

            if (arg is DateTime)
                return ((DateTime)arg).ToString("yyyyMMddHHmmss");

            return "";
        }
    }
}
