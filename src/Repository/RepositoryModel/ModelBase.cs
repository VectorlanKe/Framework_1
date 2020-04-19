using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RepositoryModel
{
    /// <summary>
    /// 实体抽象类
    /// </summary>
    public abstract class ModelBase
    {
        /// <summary>
        /// id
        /// </summary>
        [Key]
        [Required]
        [Description("主键")]
        public string Id { get; set; } = Guid.NewGuid().ToString("N");

        /// <summary>
        /// 创建时间
        /// </summary>
        [Description("创建时间")]
        public DateTime CreateTime { get; set; } = DateTime.Now;
    }
}
