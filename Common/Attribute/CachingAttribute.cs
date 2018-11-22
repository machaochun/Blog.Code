﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Common
{
    /// <summary>
    /// 这个Attribute就是使用时候的验证，把它添加到要缓存数据的方法中，即可完成缓存的操作。注意是对Method验证有效
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, Inherited = true)]
    public class CachingAttribute : Attribute
    {
        //缓存绝对过期时间
        public int AbsoluteExpiration { get; set; } = 30;

    }
}
