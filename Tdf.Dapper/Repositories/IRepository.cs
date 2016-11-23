using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Tdf.Dapper.Repositories
{
    public interface IRepository<T> where T : class, new()
    {
        Task<T> ExecuteScalarAsync(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = default(int?), CommandType? commandType = default(CommandType?));
        Task<IEnumerable<T>> QueryAsync(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = default(int?), CommandType? commandType = default(CommandType?));
        Task<IEnumerable<dynamic>> QueryAsyncDynamic(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = default(int?), CommandType? commandType = default(CommandType?));
        Task<int> ExecuteAsync(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = default(int?), CommandType? commandType = default(CommandType?));
        Task<T> GetAsync(int id, IDbTransaction transaction = null, int? commandTimeout = null);
        Task<T> GetAsync(long id, IDbTransaction transaction = null, int? commandTimeout = null);
        Task<T> GetAsync(System.Guid id, IDbTransaction transaction = null, int? commandTimeout = null);
        Task<T> GetAsync(string id, IDbTransaction transaction = null, int? commandTimeout = null);
        Task<int> InsertAsync(T model, IDbTransaction transaction = null, int? commandTimeout = null);
        Task<bool> DeleteAsync(T model, IDbTransaction transaction = null, int? commandTimeout = null);
        Task<T> UpdateAsync(T model, IDbTransaction transaction = null, int? commandTimeout = null);
        Task<PageOutput> GetPageAsync(string sql, object p = null, string sqlTotal = "", object p2 = null);
        Task<PageOutput> GetPageAsync(PageInput model, string tableName, string sqlWhere, dynamic pms1, dynamic pms2);
    }
}
