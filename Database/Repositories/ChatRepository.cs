using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using System.Data;
using Dapper;

namespace Database
{
    public class ChatRepository
    {
        string chatId = "chat_messages_chat_id";
        string chatMemberId = "chat_member_user_id";

        ConnectionProvider provider;
        public ChatRepository(ConnectionProvider provider)
        {
            this.provider = provider;
        }

        public List<ChatMessage> getChatMessages(IDbConnection connection, string chatId)
        {
            return connection.Query<ChatMessage>("SELECT * FROM chat_messages WHERE " + this.chatId + " = @chatId",
            new { chatId = chatId }).ToList();
        }

        public Chat getChat(IDbConnection connection, string userIdOne, string userIdTwo)
        {
            return connection.Query<Chat>("SELECT chat.chat_id, chat.chat_title FROM fhv_chat.chat " +
                "INNER JOIN chat_member on chat.chat_id = chat_member.chat_member_chat_id "+
                "WHERE "+ chatMemberId + " = @userIdOne OR " + chatMemberId+ " = @userIdTwo HAVING count(*) = 2;",
            new { userIdOne = userIdOne, userIdTwo = userIdTwo }).FirstOrDefault();
        }

        public List<Chat> getAllChats(IDbConnection connection, string userId)
        {
            return connection.Query<Chat>("SELECT chat.chat_id, chat.chat_title FROM fhv_chat.chat " +
                "INNER JOIN chat_member on chat.chat_id = chat_member.chat_member_chat_id " +
                "WHERE " + chatMemberId + " = @userId;",
            new {userId = userId}).ToList();
        }

        public List<User> getChatMembers(IDbConnection connection, string id)
        {
            return connection.Query<User>("SELECT useraccount.* FROM chat_member " +
                "INNER JOIN useraccount ON chat_member_user_id = useraccount_id " +
                "WHERE " + chatMemberId + " = @id;",
            new { id = id }).ToList();
        }


        public Chat createChat(IDbConnection connection, string title, int hostId, int memberId)
        {
            // create chat
            string query = "INSERT INTO chat (chat_title) VALUES (\"" + title + "\")";
            connection.Query(query);

            string chatQuery = "SELECT * FROM chat WHERE chat.chat_id = LAST_INSERT_ID()";
            Chat chat = connection.Query<Chat>(chatQuery).FirstOrDefault();

            // add members
            connection.Query("INSERT INTO chat_member (chat_member_chat_id, chat_member_user_id, chat_member_user_is_admin)"+
                "VALUES (" + chat.id + ", " + hostId +",1)").FirstOrDefault();

            connection.Query("INSERT INTO chat_member (chat_member_chat_id, chat_member_user_id, chat_member_user_is_admin)" +
                "VALUES (" + chat.id + ", " + memberId + ",1)").FirstOrDefault();
            
            return chat;
        }

        public ChatMessage saveMessage(IDbConnection connection, int chatId, int senderId, string message, DateTime timestamp)
        {
            string query = "INSERT INTO chat_messages (chat_messages_chat_id, chat_messages_message, chat_messages_sender_id, " +
                "chat_messages_timestamp) VALUES (" + chatId + ", " + message + "," + senderId + ","
                + (timestamp.Subtract(new DateTime(1970, 1, 1))).TotalSeconds + ")";

            return connection.Query<ChatMessage>("INSERT INTO chat_messages (chat_messages_chat_id, chat_messages_message, chat_messages_sender_id, "+
                "chat_messages_timestamp) VALUES (" + chatId + ", \"" + message + "\", " + senderId+","
                + (timestamp.Subtract(new DateTime(1970, 1, 1))).TotalSeconds + ")").FirstOrDefault();
        }
    }
}
