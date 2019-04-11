using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WP.Share.Data.Domain.Core.ShardingExtension
{
    /// <summary>
    /// 表信息
    /// </summary>
    public class TableInfo
    {
        public string DbName { get; set; }
        public string TableName { get; set; }

        public string ConnectionString { get; set; }

        public IDbConnection DbConnection
        {
            get
            {
                return new SqlConnection(ConnectionString);
            }
        }

        private IDbConnection _dbConnection = null;
        public IDbConnection SingleDbConnection
        {
            get
            {
                if(_dbConnection !=null)
                {
                    return _dbConnection;
                }

                return _dbConnection =new SqlConnection(ConnectionString);
            }
        }
    }
}
