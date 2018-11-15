using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.CodeTable
{
    /// <summary>
    /// 广告类
    /// </summary>
    [SugarTable("Advertisement")]
    public class AdvertisementTable
    {
        /// <summary>
        /// 分类ID
        /// </summary>
        [SugarColumn(IsNullable = false, IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [SugarColumn(IsNullable = false)]
        public DateTime Createdate { get; set; }

        /// <summary>
        /// 广告图片
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public string ImgUrl { get; set; }

        /// <summary>
        /// 广告标题
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public string Title { get; set; }

        /// <summary>
        /// 广告链接
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public string Url { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public string Remark { get; set; }
    }
}
