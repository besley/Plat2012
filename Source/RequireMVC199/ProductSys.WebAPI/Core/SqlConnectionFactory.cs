using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace ProductSys.WebAPI.Core
{
    public enum DatabaseType
    {
        SqlServer,
        MySql,
        Oracle,
        DB2
    }
    public class SqlConnectionFactory
    {
        public static IDbConnection CreateSqlConnection(DatabaseType dbType, string strConn)
        {
            IDbConnection connection = null;
            switch (dbType)
            {
                case DatabaseType.SqlServer:
                    connection = new SqlConnection(strConn);
                    break;
                case DatabaseType.MySql:
                    connection = new MySqlConnection(strConn);
                    break;
                case DatabaseType.Oracle:
                case DatabaseType.DB2:
                    connection = new OleDbConnection(strConn);
                    break;
            }
            return connection;
        }
    }
}