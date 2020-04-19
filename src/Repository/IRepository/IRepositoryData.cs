using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Linq.Expressions;
using RepositoryModel;

namespace IRepository
{
    public interface IRepositoryData
    {
        #region 添加
        int Add<TModel>(TModel model) where TModel:class;
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
        (int total,IEnumerable<TModel> models) QueryPage<TModel>(Expression<Func<TModel, bool>> where, int pageIndex, int pageSize) where TModel : ModelBase;
        ValueTask<TModel> GetAsync<TModel>(string key) where TModel : ModelBase;
        ValueTask<IEnumerable<TModel>> GetAsync<TModel>(IEnumerable<string> keys) where TModel : ModelBase;
        ValueTask<IEnumerable<TModel>> GetAsync<TModel>(Expression<Func<TModel, bool>> where) where TModel : class;
        ValueTask<TModel> QueryAsync<TModel>(string key) where TModel : ModelBase;
        ValueTask<IEnumerable<TModel>> QueryAsync<TModel>(Expression<Func<TModel, bool>> where) where TModel : class;
        ValueTask<IEnumerable<TModel>> QueryAsync<TModel>(IEnumerable<string> keys) where TModel : ModelBase;
        ValueTask<(int total,IEnumerable<TModel> models)> QueryPageAsync<TModel>(Expression<Func<TModel, bool>> where, int pageIndex, int pageSize) where TModel : ModelBase;

        int Count<TModel>(Expression<Func<TModel, bool>> where) where TModel : class;
        ValueTask<int> CountAsync<TModel>(Expression<Func<TModel, bool>> where) where TModel : class;
        #endregion
    }

    public interface IRepositoryData<TModel>: IRepositoryData where TModel: ModelBase
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

        #endregion
    }
}
