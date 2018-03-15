using Dapper.FluentMap.Mapping;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    class ChatMemberMapper : EntityMap<ChatMember>
    {
        public ChatMemberMapper()
        {
            Map(x => x.id).ToColumn("chat_member_id");
            Map(x => x.chatId).ToColumn("chat_member_chat_id");
            Map(x => x.userId).ToColumn("chat_member_user_id");
        }
    }
}