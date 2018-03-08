using Model;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    class UserAccountBroker
    {
        IDbConnectionProvider provider;
        public UserAccountBroker(IDbConnectionProvider provider)
        {
            this.provider = provider;
        }

        public User getUser(string username, string password)
        {
            using(IDbConnection connection = new getConncetion())
            {

            }
        }
    }
}
