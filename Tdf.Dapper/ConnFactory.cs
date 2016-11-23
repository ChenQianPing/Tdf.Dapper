using System.Configuration;
using System.Data;
using System.Data.Common;

namespace Tdf.Dapper
{
    /// <summary>
    /// 定义统一的IDbConnection访问入口
    /// </summary>
    public class ConnFactory
    {
        // 得到web.config里配置项的数据库连接字符串。
        private static readonly string ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
        // 得到工厂提供器类型
        private static readonly string ProviderFactoryString = ConfigurationManager.AppSettings["DBProvider"].ToString();
        private static DbProviderFactory df = null;

        /// <summary>
        /// DbService工厂用于实例化对应的IDbConnection对象，传递给Dapper。
        /// </summary>
        /// <returns></returns>
        public static IDbConnection GetConnection()
        {
            if (df == null)
                df = DbProviderFactories.GetFactory(ProviderFactoryString);
            var connection = df.CreateConnection();

            connection.ConnectionString = ConnectionString;
            connection.Open();
            return connection;
        }

    }
}
