using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace RepositoryModel
{
    /// <summary>
    /// 账号
    /// </summary>
    public class Account : ModelBase
    {
        /// <summary>
        /// 账号
        /// </summary>
        [Description("账号")]
        public string Number { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        [Description("密码")]
        public string Pwd { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        [Description("名称")]
        public string Name { get; set; }
    }
}
