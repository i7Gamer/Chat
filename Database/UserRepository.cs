using DatabaseNew;
using Dapper;
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
    public class UserRepository
    {
        string id = "useraccount_id";
        string userName = "useraccount_username";
        string password = "useraccount_password";
        string fistName = "useraccount_firstname";
        string lastName = "useraccount_lastname";

        ConnectionProvider provider;
        public UserRepository(ConnectionProvider provider)
        {
            this.provider = provider;
        }

        public User login(IDbConnection connection, string username, string password)
        {
            return connection.Query<User>("SELECT * FROM useraccount WHERE "+username+" = @username AND "+password+" = @password",
            new { username = username, password = password }).FirstOrDefault();
        }

        public User getUser(IDbConnection connection, string id)
        {
            return connection.Query<User>("SELECT * FROM useraccount WHERE "+id+" = @id", new {id = id}).FirstOrDefault();
        }

        public List<User> getUsers(IDbConnection connection)
        {
            return connection.Query<User>("SELECT * FROM useraccount").ToList();
        }

        public User createUser(IDbConnection connection, string username, string password, string firstName, string lastName)
        {
            return connection.Query<User>("INSERT INTO useraccount ("+username+", "+password+", "+firstName+", "+lastName+
                ") VALUES(@username, @password, @firstName, @lastName)", 
                new {username = username, password = password, firstName = firstName, lastName = lastName}).FirstOrDefault();
        }
    }
}
