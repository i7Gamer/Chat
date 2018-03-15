﻿using DatabaseNew;
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
    public class UserAccountBroker
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
                    command.CommandText = "SELECT * FROM useraccount WHERE useraccount_username = ?username AND useraccount_password = ?password";
                    command.AddParameter("username", DbType.String, username);
                    command.AddParameter("password", DbType.String, password);

                    IDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        User user = new User();
                        user.id = (int)reader["useraccount_id"];
                        user.firstName = (string)reader["useraccount_firstname"];
                        user.lastName = (string)reader["useraccount_lastname"];
                        user.userName = (string)reader["useraccount_username"];
                        user.password = (string)reader["useraccount_password"];
                        return user;
                    }
                    else
                    {
                        throw new Exception("Username or password did not match");
                    }
                }
            }
        }
    }
}