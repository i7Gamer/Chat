using Database;
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
        string username = "useraccount_username";
        string password = "useraccount_password";
        string firstName = "useraccount_firstname";
        string lastName = "useraccount_lastname";

        // contacts
        string userId = "contact_lists_useraccount_id";
        string ownerId = "contact_lists_useraccount_owner";

        ConnectionProvider provider;
        public UserRepository(ConnectionProvider provider)
        {
            this.provider = provider;
        }

        public User login(IDbConnection connection, string username, string password)
        {
            return connection.Query<User>("SELECT * FROM useraccount WHERE "+this.username+" = @username AND "+ this.password +" = @password",
            new { username = username, password = password }).FirstOrDefault();
        }

        public User logout(IDbConnection connection, string username)
        {
            return connection.Query<User>("SELECT * FROM useraccount WHERE " + this.username + " = @username",
            new { username = username }).FirstOrDefault();
        }

        public User getUser(IDbConnection connection, string id)
        {
            return connection.Query<User>("SELECT * FROM useraccount WHERE "+ this.id +" = @id", new {id = id}).FirstOrDefault();
        }


        public List<User> getContacts(IDbConnection connection, string userId)
        {
            return connection.Query<User>("SELECT * FROM contact_lists INNER JOIN useraccount ON " + this.userId + " = useraccount.useraccount_id WHERE " + ownerId + " = " + userId).ToList();
        }

        public List<User> getUsers(IDbConnection connection)
        {
            return connection.Query<User>("SELECT * FROM useraccount").ToList();
        }

        public User createUser(IDbConnection connection, string username, string password, string firstName, string lastName)
        {
            return connection.Query<User>("INSERT INTO useraccount ("+ this.username +", "+ this.password +", "+ this.firstName +", "+ this.lastName +
                ") VALUES(@username, @password, @firstName, @lastName)", 
                new {username = username, password = password, firstName = firstName, lastName = lastName}).FirstOrDefault();
        }
    }
}
