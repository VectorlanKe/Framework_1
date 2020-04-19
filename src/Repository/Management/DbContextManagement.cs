using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using RepositoryFactory;

namespace Management
{
    public class DbContextManagement : IDesignTimeDbContextFactory<DataContext>
    {
        public DataContext CreateDbContext(string[] args)
        {
            var option = new DbContextOptionsBuilder<DataContext>()
                .UseMySql("server=127.0.0.1;port=3366;database=TestData;uid=root;pwd=style123@;charset=utf8mb4;",option=> option.MigrationsAssembly("Management"));
            return new DataContext(option.Options);
        }
    }
}
