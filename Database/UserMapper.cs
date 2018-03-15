using Dapper.FluentMap.Mapping;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseNew
{
    class UserMapper : EntityMap<User>
    {
        public UserMapper()
        {
            Map(x => x.id).ToColumn("useraccount_id");
            Map(x => x.firstName).ToColumn("useraccount_firstname");
            Map(x => x.lastName).ToColumn("useraccount_lastname");
            Map(x => x.userName).ToColumn("useraccount_username");
            Map(x => x.password).ToColumn("useraccount_password");
        }
    }
}
