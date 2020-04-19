using Microsoft.EntityFrameworkCore;
using RepositoryModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepositoryFactory
{
    /// <summary>
    /// 数据库上下文
    /// </summary>
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            :base(options)
        {

        }
        /// <summary>
        /// 账号
        /// </summary>
        DbSet<Account> Accounts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }
    }
}
