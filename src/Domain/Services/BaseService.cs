using IRepository;
using IServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RepositoryFactory;
using RepositoryModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public abstract class ServiceBase
    {
        private protected IRepositoryData _repository;
        private protected IConfiguration _config;
        public ServiceBase(IConfiguration configuration) 
        {
            _config = configuration;
            DataRepositoryBuilder();
        }
        private protected abstract void DataRepositoryBuilder();
    }

    public class BaseService : ServiceBase, IService
    {
        public BaseService(IConfiguration configuration)
            :base(configuration) { }
        private protected override void DataRepositoryBuilder()=> _repository = new RepositoryBuilder(_config.GetConnectionString("MySqlConn")).Build();
        public int Add<TModel>(TModel model) where TModel : class => _repository.Add(model);
        public int Add<TModel>(IEnumerable<TModel> models) where TModel : class => _repository.Add(models);

        public async ValueTask<int> AddAsync<TModel>(TModel model) where TModel : class => await _repository.AddAsync(model);

        public async ValueTask<int> AddAsync<TModel>(IEnumerable<TModel> models) where TModel : class => await _repository.AddAsync(models);

        public int Count<TModel>(Expression<Func<TModel, bool>> where) where TModel : class => _repository.Count(where);

        public async ValueTask<int> CountAsync<TModel>(Expression<Func<TModel, bool>> where) where TModel : class =>await _repository.CountAsync(where);

        public int Delete<TModel>(string key) where TModel : ModelBase => _repository.Delete<TModel>(key);

        public int Delete<TModel>(TModel model) where TModel : class => _repository.Delete(model);

        public int Delete<TModel>(IEnumerable<string> keys) where TModel : ModelBase => _repository.Delete<TModel>(keys);

        public int Delete<TModel>(IEnumerable<TModel> models) where TModel : class => _repository.Delete(models);

        public async ValueTask<int> DeleteAsync<TModel>(string key) where TModel : ModelBase =>await _repository.DeleteAsync<TModel>(key);

        public async ValueTask<int> DeleteAsync<TModel>(TModel model) where TModel : class => await _repository.DeleteAsync(model);

        public async ValueTask<int> DeleteAsync<TModel>(IEnumerable<string> keys) where TModel : ModelBase =>await _repository.DeleteAsync(keys);

        public async ValueTask<int> DeleteAsync<TModel>(IEnumerable<TModel> models) where TModel : class =>await _repository.DeleteAsync(models);

        public bool Exist<TModel>(string key) where TModel : ModelBase => _repository.Query<TModel>(p => p.Id == key).Count() > 0;

        public bool Exist<TModel>(Expression<Func<TModel, bool>> where) where TModel : class => _repository.Query(where).Count() > 0;

        public async ValueTask<bool> ExistAsync<TModel>(string key) where TModel : ModelBase => (await _repository.QueryAsync<TModel>(p => p.Id == key)).Count() >= 0;

        public async ValueTask<bool> ExistAsync<TModel>(Expression<Func<TModel, bool>> where) where TModel : class => (await _repository.QueryAsync(where)).Count() >= 0;

        public TModel Get<TModel>(string key) where TModel : ModelBase => _repository.Get<TModel>(key);

        public IEnumerable<TModel> Get<TModel>(IEnumerable<string> keys) where TModel : ModelBase => _repository.Get<TModel>(keys);

        public IEnumerable<TModel> Get<TModel>(Expression<Func<TModel, bool>> where) where TModel : class => _repository.Get(where);

        public async ValueTask<TModel> GetAsync<TModel>(string key) where TModel : ModelBase =>await _repository.GetAsync<TModel>(key);

        public async ValueTask<IEnumerable<TModel>> GetAsync<TModel>(IEnumerable<string> keys) where TModel : ModelBase => await _repository.GetAsync<TModel>(keys);

        public async ValueTask<IEnumerable<TModel>> GetAsync<TModel>(Expression<Func<TModel, bool>> where) where TModel : class =>await _repository.GetAsync(where);

        public TModel Query<TModel>(string key) where TModel : ModelBase => _repository.Query<TModel>(key);

        public IEnumerable<TModel> Query<TModel>(IEnumerable<string> keys) where TModel : ModelBase => _repository.Query<TModel>(keys);

        public IEnumerable<TModel> Query<TModel>(Expression<Func<TModel, bool>> where) where TModel : class => _repository.Query(where);

        public async ValueTask<TModel> QueryAsync<TModel>(string key) where TModel : ModelBase =>await _repository.QueryAsync<TModel>(key);

        public async ValueTask<IEnumerable<TModel>> QueryAsync<TModel>(Expression<Func<TModel, bool>> where) where TModel : class => await _repository.QueryAsync(where);

        public async ValueTask<IEnumerable<TModel>> QueryAsync<TModel>(IEnumerable<string> keys) where TModel : ModelBase =>await _repository.QueryAsync<TModel>(keys);

        public (int total, IEnumerable<TModel> models) QueryPage<TModel>(Expression<Func<TModel, bool>> where, int pageIndex, int pageSize) where TModel : ModelBase
            => _repository.QueryPage(where,pageIndex,pageSize);

        public async ValueTask<(int total, IEnumerable<TModel> models)> QueryPageAsync<TModel>(Expression<Func<TModel, bool>> where, int pageIndex, int pageSize) where TModel : ModelBase
            => await _repository.QueryPageAsync(where, pageIndex, pageSize);

        public int Update<TModel>(TModel model) where TModel : class => _repository.Update(model);

        public int Update<TModel>(IEnumerable<TModel> models) where TModel : class => _repository.Update(models);

        public async ValueTask<int> UpdateAsync<TModel>(TModel model) where TModel : class => await _repository.UpdateAsync(model);

        public async ValueTask<int> UpdateAsync<TModel>(IEnumerable<TModel> models) where TModel : class=>await _repository.UpdateAsync(models);
    }

    public class BaseService<TModel> : BaseService, IService<TModel> where TModel : ModelBase
    {
        public BaseService(IConfiguration configuration) :base(configuration) { }
        private protected override void DataRepositoryBuilder()=> _repository = new RepositoryBuilder(_config.GetConnectionString("MySqlConn")).Build<TModel>();

        public int Delete(string key) => Delete<TModel>(key);

        public int Delete(IEnumerable<string> keys) => Delete<TModel>(keys);

        public async ValueTask<int> DeleteAsync(string key) =>await DeleteAsync<TModel>(key);

        public async ValueTask<int> DeleteAsync(IEnumerable<string> keys) =>await DeleteAsync<TModel>(keys);

        public bool Exist(string key) => Exist<TModel>(key);

        public async ValueTask<bool> ExistAsync(string key) => await ExistAsync(key);

        public TModel Get(string key) => Get<TModel>(key);

        public IEnumerable<TModel> Get(IEnumerable<string> keys) => Get<TModel>(keys);

        public async ValueTask<TModel> GetAsync(string key) =>await GetAsync<TModel>(key);

        public async ValueTask<IEnumerable<TModel>> GetAsync(IEnumerable<string> keys) => await GetAsync<TModel>(keys);

        public TModel Query(string key) => Query<TModel>(key);

        public async ValueTask<TModel> QueryAsync(string key) => await QueryAsync<TModel>(key);
    }

    public class BaseService<TDbContext, TModel>: BaseService<TModel>, IService<TDbContext, TModel> where TDbContext:class where TModel : ModelBase
    {
        public BaseService(IConfiguration configuration) : base(configuration) { }
        private protected override void DataRepositoryBuilder() => _repository = new RepositoryBuilder(_config.GetConnectionString("MySqlConn")).Build<TDbContext,TModel>();

    }
}
