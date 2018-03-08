using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Database
{
    public class ConnectionProvider
    {
        string connectionString = "Server=dbsrv.inteo.at;Database=fhv_chat;Uid=fhv_chat_user;Pwd=thi5i5incredibly5ecure";

        public IDbConnection getConnection()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            return connection;
        }
    }
}
