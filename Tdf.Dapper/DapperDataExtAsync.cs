using Dapper.Contrib.Extensions;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Tdf.Dapper
{
    /// <summary>
    /// 扩展方法
    /// </summary>
    public abstract partial class DapperDataAsync
    {
        #region 查询系
        /// <summary>
        /// 获取Model-Key为int类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public static async Task<T> GetAsync<T>(int id, IDbTransaction transaction = null, int? commandTimeout = null) where T : class, new()
        {
            using (var conn = ConnFactory.GetConnection())
            {
                return await conn.GetAsync<T>(id, transaction, commandTimeout);
            }
        }

        /// <summary>
        /// 获取Model-Key为long类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public static async Task<T> GetAsync<T>(long id, IDbTransaction transaction = null, int? commandTimeout = null) where T : class, new()
        {
            using (var conn = ConnFactory.GetConnection())
            {
                return await conn.GetAsync<T>(id, transaction, commandTimeout);
            }
        }

        /// <summary>
        /// 获取Model-Key为Guid类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public static async Task<T> GetAsync<T>(System.Guid id, IDbTransaction transaction = null, int? commandTimeout = null) where T : class, new()
        {
            using (var conn = ConnFactory.GetConnection())
            {
                return await conn.GetAsync<T>(id, transaction, commandTimeout);
            }
        }

        /// <summary>
        /// 获取Model-Key为string类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public static async Task<T> GetAsync<T>(string id, IDbTransaction transaction = null, int? commandTimeout = null) where T : class, new()
        {
            using (var conn = ConnFactory.GetConnection())
            {
                return await conn.GetAsync<T>(id, transaction, commandTimeout);
            }
        }

        /// <summary>
        /// 获取Model集合（没有Where条件）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static async Task<IEnumerable<T>> GetAllAsync<T>() where T : class, new()
        {
            using (var conn = ConnFactory.GetConnection())
            {
                return await conn.GetAllAsync<T>();
            }
        }
        #endregion

        #region 增删改
        /// <summary>
        /// 插入一个Model
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public static async Task<int> InsertAsync<T>(T model, IDbTransaction transaction = null, int? commandTimeout = null) where T : class, new()
        {
            using (var conn = ConnFactory.GetConnection())
            {
                return await conn.InsertAsync<T>(model, transaction, commandTimeout);
            }
        }

        public static async Task<bool> DeleteAsync<T>(T model, IDbTransaction transaction = null, int? commandTimeout = null) where T : class, new()
        {
            using (var conn = ConnFactory.GetConnection())
            {
                return await conn.DeleteAsync<T>(model, transaction, commandTimeout);
            }
        }

        /// <summary>
        /// 更新一个Model
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public static async Task<T> UpdateAsync<T>(T model, IDbTransaction transaction = null, int? commandTimeout = null) where T : class, new()
        {
            using (var conn = ConnFactory.GetConnection())
            {
                bool b = await conn.UpdateAsync<T>(model, transaction, commandTimeout);
                if (b) { return model; }
                else { return null; }
            }
        }

        #endregion

        #region 分页查询
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql">查询语句</param>
        /// <param name="p">动态参数</param>
        /// <param name="sqlTotal">total语句</param>
        /// <param name="p2">Total动态参数</param>
        /// <returns></returns>
        public static async Task<PageOutput> GetPageAsync<T>(string sql, object p = null, string sqlTotal = "", object p2 = null)
        {
            var rows = await QueryAsync(sql.ToString(), p);
            var total = rows.Count();
            if (!sqlTotal.IsNullOrWhiteSpace()) { total = await ExecuteScalarAsync<int>(sqlTotal, p2); }
            return new PageOutput() { Records = rows, Total = total };
        }
        #endregion





    }
}
