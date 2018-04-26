using Database;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Webservice.Controllers
{
    public class ChatController : BaseController
    {
        ChatRepository chatRepository = new ChatRepository(provider);

        [HttpGet]
        [ActionName("getChatMessages")]
        public IHttpActionResult getChatMessages(string chatId)
        {
            using(IDbConnection connection = provider.getConnection())
            {
                List<ChatMessage> messages;
                try
                {
                    messages = chatRepository.getChatMessages(connection, chatId);
                }
                catch (Exception e)
                {
                    return BadRequest(e.ToString());
                }

                if (messages == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(messages);
                }
            }
        }

        [HttpGet]
        [ActionName("getChat")]
        public IHttpActionResult getChat(string userIdOne, string userIdTwo)
        {
            using (IDbConnection connection = provider.getConnection())
            {
                Chat chat;
                try
                {
                    chat = chatRepository.getChat(connection, userIdOne, userIdTwo);
                }
                catch (Exception e)
                {
                    return BadRequest(e.ToString());
                }

                if (chat == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(chat);
                }
            }
        }


        [HttpGet]
        [ActionName("getAllChats")]
        public IHttpActionResult getAllChats(string id)
        {
            using (IDbConnection connection = provider.getConnection())
            {
                List<Chat> chats;
                try
                {
                    chats = chatRepository.getAllChats(connection, id);
                }
                catch (Exception e)
                {
                    return BadRequest(e.ToString());
                }

                if (chats == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(chats);
                }
            }
        }

        [HttpGet]
        [ActionName("getChatMembers")]
        public IHttpActionResult getChatMembers(string id)
        {
            using (IDbConnection connection = provider.getConnection())
            {
                List<User> users;
                try
                {
                    users = chatRepository.getChatMembers(connection, id);
                }
                catch (Exception e)
                {
                    return BadRequest(e.ToString());
                }

                if (users == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(users);
                }
            }
        }

        [HttpPost]
        [ActionName("createNewChat")]
        public IHttpActionResult createNewChat([FromBody] TwoPersonChat chat)
        {
            using (IDbConnection connection = provider.getConnection())
            {
                Chat newChat;
                try
                {
                    newChat = chatRepository.createChat(connection, chat.title, chat.host, chat.member);
                }
                catch(Exception e)
                {
                    return BadRequest(e.ToString());
                }

                if (newChat == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(newChat);
                }
            }
        }

        [HttpPost]
        [ActionName("saveMessage")]
        public IHttpActionResult saveMessage([FromBody] ChatMessage message)
        {
            using (IDbConnection connection = provider.getConnection())
            {
                ChatMessage newMessage;
                try
                {
                    newMessage = chatRepository.saveMessage(connection, message.chatId, message.senderId, message.message, message.timestamp);
                }
                catch (Exception e)
                {
                    return BadRequest(e.ToString());
                }

                if (newMessage == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(newMessage);
                }
            }
        }
    }
}