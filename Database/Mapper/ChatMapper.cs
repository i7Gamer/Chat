using Dapper.FluentMap.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace Database
{
    class ChatMapper : EntityMap<Chat>
    {
        public ChatMapper()
        {
            Map(x => x.id).ToColumn("chat_id");
            Map(x => x.title).ToColumn("chat_title");
            Map(x => x.host).ToColumn("chat_host");
        }
    }
}