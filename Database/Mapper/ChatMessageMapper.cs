using Dapper.FluentMap.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace Database
{
    class ChatMessageMapper : EntityMap<ChatMessage>
    {
        public ChatMessageMapper()
        {
            Map(x => x.id).ToColumn("chat_messages_id");
            Map(x => x.chatId).ToColumn("chat_messages_chat_id");
            Map(x => x.message).ToColumn("chat_messages_message");
            Map(x => x.senderId).ToColumn("chat_messages_sender_id");
            Map(x => x.timestamp).ToColumn("chat_messages_timestamp");
        }
    }
}