using RepositoryModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace IRepository
{
    public interface IRepositoryBuilder
    {
        IRepositoryData Build();
        IRepositoryData Build<TDbContext>() where TDbContext : class;
        IRepositoryData Build<TDbContext,TModel>() where TDbContext : class where TModel : ModelBase;
    }
}
