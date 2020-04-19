using IRepository;
using RepositoryModel;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IServices
{
    public interface IService
    {
        #region 添加
        int Add<TModel>(TModel model) where TModel : class;
        int Add<TModel>(IEnumerable<TModel> models) where TModel : class;
        ValueTask<int> AddAsync<TModel>(TModel model) where TModel : class;
        ValueTask<int> AddAsync<TModel>(IEnumerable<TModel> models) where TModel : class;
        #endregion

        #region 修改
        int Update<TModel>(TModel model) where TModel : class;
        int Update<TModel>(IEnumerable<TModel> models) where TModel : class;
        ValueTask<int> UpdateAsync<TModel>(TModel model) where TModel : class;
        ValueTask<int> UpdateAsync<TModel>(IEnumerable<TModel> models) where TModel : class;
        #endregion

        #region 删除
        int Delete<TModel>(string key) where TModel : ModelBase;
        int Delete<TModel>(TModel model) where TModel : class;
        int Delete<TModel>(IEnumerable<string> keys) where TModel : ModelBase;
        int Delete<TModel>(IEnumerable<TModel> models) where TModel : class;
        ValueTask<int> DeleteAsync<TModel>(string key) where TModel : ModelBase;
        ValueTask<int> DeleteAsync<TModel>(TModel model) where TModel : class;
        ValueTask<int> DeleteAsync<TModel>(IEnumerable<string> keys) where TModel : ModelBase;
        ValueTask<int> DeleteAsync<TModel>(IEnumerable<TModel> models) where TModel : class;
        #endregion

        #region 查询
        TModel Get<TModel>(string key) where TModel : ModelBase;
        IEnumerable<TModel> Get<TModel>(IEnumerable<string> keys) where TModel : ModelBase;
        IEnumerable<TModel> Get<TModel>(Expression<Func<TModel, bool>> where) where TModel : class;
        TModel Query<TModel>(string key) where TModel : ModelBase;
        IEnumerable<TModel> Query<TModel>(IEnumerable<string> keys) where TModel : ModelBase;
        IEnumerable<TModel> Query<TModel>(Expression<Func<TModel, bool>> where) where TModel : class;
        (int total, IEnumerable<TModel> models) QueryPage<TModel>(Expression<Func<TModel, bool>> where, int pageIndex, int pageSize) where TModel : ModelBase;
        ValueTask<TModel> GetAsync<TModel>(string key) where TModel : ModelBase;
        ValueTask<IEnumerable<TModel>> GetAsync<TModel>(IEnumerable<string> keys) where TModel : ModelBase;
        ValueTask<IEnumerable<TModel>> GetAsync<TModel>(Expression<Func<TModel, bool>> where) where TModel : class;
        ValueTask<TModel> QueryAsync<TModel>(string key) where TModel : ModelBase;
        ValueTask<IEnumerable<TModel>> QueryAsync<TModel>(Expression<Func<TModel, bool>> where) where TModel : class;
        ValueTask<IEnumerable<TModel>> QueryAsync<TModel>(IEnumerable<string> keys) where TModel : ModelBase;
        ValueTask<(int total, IEnumerable<TModel> models)> QueryPageAsync<TModel>(Expression<Func<TModel, bool>> where, int pageIndex, int pageSize) where TModel : ModelBase;

        bool Exist<TModel>(string key) where TModel : ModelBase;
        bool Exist<TModel>(Expression<Func<TModel, bool>> where) where TModel : class;
        ValueTask<bool> ExistAsync<TModel>(string key) where TModel : ModelBase;
        ValueTask<bool> ExistAsync<TModel>(Expression<Func<TModel, bool>> where) where TModel : class;

        int Count<TModel>(Expression<Func<TModel, bool>> where) where TModel : class;
        ValueTask<int> CountAsync<TModel>(Expression<Func<TModel, bool>> where) where TModel : class;
        #endregion

        
    }
    public interface IService<TModel>: IService where TModel: ModelBase
    {
        #region 删除
        int Delete(string key);
        int Delete(IEnumerable<string> keys);
        ValueTask<int> DeleteAsync(string key);
        ValueTask<int> DeleteAsync(IEnumerable<string> keys);
        #endregion

        #region 查询
        TModel Get(string key);
        IEnumerable<TModel> Get(IEnumerable<string> keys);
        TModel Query(string key);
        ValueTask<TModel> GetAsync(string key);
        ValueTask<IEnumerable<TModel>> GetAsync(IEnumerable<string> keys);
        ValueTask<TModel> QueryAsync(string key);

        bool Exist(string key);
        ValueTask<bool> ExistAsync(string key);
        #endregion
    }
    public interface IService<TDbContext, TModel> : IService<TModel> where TDbContext : class where TModel : ModelBase
    {
        
    }
}
