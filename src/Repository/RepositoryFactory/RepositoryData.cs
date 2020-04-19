using IRepository;
using Microsoft.EntityFrameworkCore;
using RepositoryModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryFactory
{
    public class RepositoryData<TDbContext> : IRepositoryData where TDbContext : class
    {
        private protected readonly DbContext _db;
        public RepositoryData(TDbContext dbContext)
        {
            _db = dbContext as DbContext??throw new AccessViolationException("数据库对象实例失败");
        }
        private DbSet<TModel> Entity<TModel>() where TModel : class => _db.Set<TModel>();
        public int Add<TModel>(TModel model) where TModel : class
        {
            Entity<TModel>().Add(model);
            return _db.SaveChanges();
        }

        public int Add<TModel>(IEnumerable<TModel> models) where TModel : class
        {
            Entity<TModel>().AddRange(models);
            return _db.SaveChanges();
        }

        public async ValueTask<int> AddAsync<TModel>(TModel model) where TModel : class
        {
            await Entity<TModel>().AddAsync(model);
            return await _db.SaveChangesAsync();
        }

        public async ValueTask<int> AddAsync<TModel>(IEnumerable<TModel> models) where TModel : class
        {
            Entity<TModel>().AddRange(models);
            return await _db.SaveChangesAsync();
        }

        public int Count<TModel>(Expression<Func<TModel, bool>> where) where TModel : class => Entity<TModel>().AsNoTracking().Where(where).Count();

        public async ValueTask<int> CountAsync<TModel>(Expression<Func<TModel, bool>> where) where TModel : class => await Entity<TModel>().AsNoTracking().Where(where).CountAsync();

        public int Delete<TModel>(string key) where TModel : ModelBase => Delete(Get<TModel>(key));

        public int Delete<TModel>(TModel model) where TModel : class
        {
            Entity<TModel>().Remove(model);
            return _db.SaveChanges();
        }

        public int Delete<TModel>(IEnumerable<string> keys) where TModel : ModelBase => Delete(Get<TModel>(keys));

        public int Delete<TModel>(IEnumerable<TModel> models) where TModel : class
        {
            Entity<TModel>().RemoveRange(models);
            return _db.SaveChanges();
        }

        public async ValueTask<int> DeleteAsync<TModel>(string key) where TModel : ModelBase => await DeleteAsync(await GetAsync<TModel>(key));

        public async ValueTask<int> DeleteAsync<TModel>(TModel model) where TModel : class
        {
            Entity<TModel>().Remove(model);
            return await _db.SaveChangesAsync();
        }

        public async ValueTask<int> DeleteAsync<TModel>(IEnumerable<string> keys) where TModel : ModelBase => await DeleteAsync(await GetAsync<TModel>(keys));

        public async ValueTask<int> DeleteAsync<TModel>(IEnumerable<TModel> models) where TModel : class
        {
            Entity<TModel>().RemoveRange(models);
            return await _db.SaveChangesAsync();
        }

        public TModel Get<TModel>(string key) where TModel : ModelBase => Entity<TModel>().FirstOrDefault(p => p.Id == key);

        public IEnumerable<TModel> Get<TModel>(Expression<Func<TModel, bool>> where) where TModel : class => Entity<TModel>().Where(where);

        public async ValueTask<TModel> GetAsync<TModel>(string key) where TModel : ModelBase => await Entity<TModel>().FirstOrDefaultAsync(p => p.Id == key);

        public async ValueTask<IEnumerable<TModel>> GetAsync<TModel>(Expression<Func<TModel, bool>> where) where TModel : class => await Task.FromResult(Entity<TModel>().Where(where));

        public TModel Query<TModel>(string key) where TModel : ModelBase => Entity<TModel>().AsNoTracking().FirstOrDefault(p => p.Id == key);

        public async ValueTask<TModel> QueryAsync<TModel>(string key) where TModel : ModelBase => await Entity<TModel>().AsNoTracking().FirstOrDefaultAsync(p => p.Id == key);

        public (int total, IEnumerable<TModel> models) QueryPage<TModel>(Expression<Func<TModel, bool>> where, int pageIndex, int pageSize) where TModel : ModelBase
        {
            IQueryable<TModel> queryable = Entity<TModel>().AsNoTracking().Where(where);
            return (queryable.Count(), queryable.OrderBy(p => p.CreateTime).Skip(((pageIndex <= 1 ? 1 : pageIndex) - 1) * pageSize).Take(pageSize));
        }

        public async ValueTask<(int total, IEnumerable<TModel> models)> QueryPageAsync<TModel>(Expression<Func<TModel, bool>> where, int pageIndex, int pageSize) where TModel : ModelBase
        {
            IQueryable<TModel> queryable = Entity<TModel>().AsNoTracking().Where(where);
            return (await queryable.CountAsync(), queryable.OrderBy(p => p.CreateTime).Skip(((pageIndex <= 1 ? 1 : pageIndex) - 1) * pageSize).Take(pageSize));
        }

        public int Update<TModel>(TModel model) where TModel : class => _db.SaveChanges();

        public int Update<TModel>(IEnumerable<TModel> models) where TModel : class => _db.SaveChanges();

        public async ValueTask<int> UpdateAsync<TModel>(TModel model) where TModel : class => await _db.SaveChangesAsync();

        public async ValueTask<int> UpdateAsync<TModel>(IEnumerable<TModel> models) where TModel : class => await _db.SaveChangesAsync();

        public IEnumerable<TModel> Get<TModel>(IEnumerable<string> keys) where TModel : ModelBase => Entity<TModel>().Where(p => keys.Contains(p.Id));

        public IEnumerable<TModel> Query<TModel>(IEnumerable<string> keys) where TModel : ModelBase => Entity<TModel>().AsNoTracking().Where(p => keys.Contains(p.Id));

        public async ValueTask<IEnumerable<TModel>> GetAsync<TModel>(IEnumerable<string> keys) where TModel : ModelBase => await Task.FromResult(Get<TModel>(keys));

        public async ValueTask<IEnumerable<TModel>> QueryAsync<TModel>(IEnumerable<string> keys) where TModel : ModelBase => await Task.FromResult(Query<TModel>(keys));

        public IEnumerable<TModel> Query<TModel>(Expression<Func<TModel, bool>> where) where TModel : class => Entity<TModel>().AsNoTracking().Where(where);

        public async ValueTask<IEnumerable<TModel>> QueryAsync<TModel>(Expression<Func<TModel, bool>> where) where TModel : class => await Task.FromResult(Query(where));
    }

    public class RepositoryData<TDbContext, TModel> : RepositoryData<TDbContext>, IRepositoryData<TModel> where TDbContext : class where TModel : ModelBase
    {
        public RepositoryData(TDbContext dbContext) :base(dbContext) { }
        public int Delete(string key)=>Delete<TModel>(key);

        public int Delete(IEnumerable<string> keys) => Delete<TModel>(keys);

        public async ValueTask<int> DeleteAsync(string key) => await DeleteAsync<TModel>(key);

        public async ValueTask<int> DeleteAsync(IEnumerable<string> keys) => await DeleteAsync<TModel>(keys);

        public TModel Get(string key) => Get<TModel>(key);

        public IEnumerable<TModel> Get(IEnumerable<string> keys) => Get<TModel>(keys);

        public async ValueTask<TModel> GetAsync(string key) => await GetAsync(key);

        public async ValueTask<IEnumerable<TModel>> GetAsync(IEnumerable<string> keys) => await GetAsync<TModel>(keys);

        public TModel Query(string key) => Query<TModel>(key);

        public async ValueTask<TModel> QueryAsync(string key) => await QueryAsync<TModel>(key);
    }
}
