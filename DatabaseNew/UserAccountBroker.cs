using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    class UserAccountBroker
    {
        ConnectionProvider provider;
        public UserAccountBroker(ConnectionProvider provider)
        {
            this.provider = provider;
        }

        public User getUser(string username, string password)
        {
            using(IDbConnection connection = provider.getConnection())
            {
                using (IDbCommand command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM useraccount WHERE username = ?username AND password = ?password";
                    IDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        User user = new User();
                        user.id = (int)reader["id"];
                        user.firstName = (string)reader["firstname"];
                        user.lastName = (string)reader["lastname"];
                        user.userName = (string)reader["username"];
                        user.password = (string)reader["password"];
                        return user;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }
    }
}
