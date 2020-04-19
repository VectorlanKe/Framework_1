using Autofac;
using IRepository;
using Microsoft.EntityFrameworkCore;
using RepositoryModel;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace RepositoryFactory
{
    public class RepositoryBuilder : IRepositoryBuilder
    {
        private static DataContext _dataContext = null;
        private static readonly object _objLock = new object();
        public RepositoryBuilder(string connStr)
        {
            if (_dataContext == null)
            {
                lock (_objLock)
                {
                    if (_dataContext == null)
                    {
                        var option = new DbContextOptionsBuilder<DataContext>().UseMySql(connStr);
                        _dataContext = new DataContext(option.Options);
                    }
                }
            }
        }

        public IRepositoryData Build() => new RepositoryData<DataContext>(_dataContext);
        public IRepositoryData Build<TDbContext>() where TDbContext : class
            => new RepositoryData<DataContext>(_dataContext);
        public IRepositoryData Build<TDbContext, TModel>()
            where TDbContext : class
            where TModel : ModelBase
            => new RepositoryData<DataContext, TModel>(_dataContext);
    }

}
