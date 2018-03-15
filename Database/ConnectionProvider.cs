using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;

namespace Database
{
    public class ConnectionProvider
    {
        string connectionString = "Server=dbsrv.infeo.at;Database=fhv_chat;Uid=fhv_chat_user;Pwd=test;SslMode=None";

        public IDbConnection getConnection()
        {
            IDbConnection connection = new MySqlConnection(connectionString);
            connection.Open();
            return connection;
        }
    }
}
