using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Tdf.Dapper.Repositories
{
    public class DapperRepositoryBase<T> : IRepository<T> where T : class, new()
    {
        #region 基础方法

        #region 查询系列
        /// <summary>
        /// 单个值返回值
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public async Task<T> ExecuteScalarAsync(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = default(int?), CommandType? commandType = default(CommandType?))
        {
            return await DapperDataAsync.ExecuteScalarAsync<T>(sql, param, transaction, commandTimeout, commandType);
        }

        /// <summary>
        /// 强类型查询
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="transaction"></param>
        /// <param name="buffered"></param>
        /// <returns></returns>
        public async Task<IEnumerable<T>> QueryAsync(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = default(int?), CommandType? commandType = default(CommandType?))
        {
            return await DapperDataAsync.QueryAsync<T>(sql, param, transaction, commandTimeout, commandType);
        }

        /// <summary>
        /// 动态类型查询 | 多映射动态查询
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="transaction"></param>
        /// <param name="buffered"></param>
        /// <returns></returns>
        public async Task<IEnumerable<dynamic>> QueryAsyncDynamic(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = default(int?), CommandType? commandType = default(CommandType?))
        {
            return await DapperDataAsync.QueryAsync(sql, param, transaction, commandTimeout, commandType);
        }
        #endregion

        #region 增删改系
        /// <summary>
        /// 增删改系
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public async Task<int> ExecuteAsync(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = default(int?), CommandType? commandType = default(CommandType?))
        {
            return await DapperDataAsync.ExecuteAsync(sql, param, transaction, commandTimeout, commandType);
        }
        #endregion

        #endregion

        #region 扩展方法

        #region 查询系
        /// <summary>
        /// 获取Model-Key为int类型
        /// </summary>
        /// <param name="id"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public async Task<T> GetAsync(int id, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            return await DapperDataAsync.GetAsync<T>(id, transaction, commandTimeout);
        }

        /// <summary>
        /// 获取Model-Key为long类型
        /// </summary>
        /// <param name="id"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public async Task<T> GetAsync(long id, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            return await DapperDataAsync.GetAsync<T>(id, transaction, commandTimeout);
        }

        /// <summary>
        /// 获取Model-Key为Guid类型
        /// </summary>
        /// <param name="id"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public async Task<T> GetAsync(System.Guid id, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            return await DapperDataAsync.GetAsync<T>(id, transaction, commandTimeout);
        }

        /// <summary>
        /// 获取Model-Key为String类型
        /// </summary>
        /// <param name="id"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public async Task<T> GetAsync(string id, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            return await DapperDataAsync.GetAsync<T>(id, transaction, commandTimeout);
        }

        /// <summary>
        /// 获取Model集合（没有Where条件）
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await DapperDataAsync.GetAllAsync<T>();
        }
        #endregion

        #region 增删改
        /// <summary>
        /// 插入一个Model
        /// </summary>
        /// <param name="model"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public async Task<int> InsertAsync(T model, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            return await DapperDataAsync.InsertAsync<T>(model, transaction, commandTimeout);
        }

        public async Task<bool> DeleteAsync(T model, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            return await DapperDataAsync.DeleteAsync<T>(model, transaction, commandTimeout);
        }

        /// <summary>
        /// 更新一个Model
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="entityToUpdate"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public async Task<T> UpdateAsync(T model, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            return await DapperDataAsync.UpdateAsync<T>(model, transaction, commandTimeout);
        }
        #endregion

        #region 分页查询
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="sql">查询语句</param>
        /// <param name="p">动态参数</param>
        /// <param name="sqlTotal">total语句</param>
        /// <param name="p2">Total动态参数</param>
        /// <returns></returns>
        public async Task<PageOutput> GetPageAsync(string sql, object p = null, string sqlTotal = "", object p2 = null)
        {
            return await DapperDataAsync.GetPageAsync<T>(sql, p, sqlTotal, p2);
        }

        public async Task<PageOutput> GetPageAsync(PageInput model, string tableName, string sqlWhere, dynamic pms1, dynamic pms2)
        {
            var sqlStr = new System.Text.StringBuilder("select * from ");
            var sqlCount = new System.Text.StringBuilder("select count(1) from ");

            sqlStr.Append(tableName);
            sqlCount.Append(tableName);
            sqlStr.Append(sqlWhere.ToString());
            sqlCount.Append(sqlWhere.ToString());

            #region 分页系列
            if (model.Offset == 0 && model.PageSize == 0)//不分页==》这时候两个条件是一样的
            {
                return await GetPageAsync(sqlStr.ToString(), pms1, sqlCount.ToString(), pms2);
            }
            if (model.Offset < 0) { model.Offset = 0; }
            if (model.PageSize < 1) { model.PageSize = 10; }
            model.PageIndex = model.Offset / model.PageSize + 1;

            pms1.PageIndex = model.PageIndex;
            pms1.PageSize = model.PageSize;

            sqlStr.Insert(0, string.Format("select * from(select row_number() over(order by {0}) Id,* from (", model.OrderStr));
            sqlStr.Append(") TempA) as TempInfo where Id<= @PageIndex * @PageSize and Id>(@PageIndex-1)*@PageSize");
            return await GetPageAsync(sqlStr.ToString(), pms1, sqlCount.ToString(), pms2);
            #endregion
        }

        #endregion

        #endregion
    }
}
