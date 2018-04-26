using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Chat
    {
        public int id { get; set; }
        public string title { get; set; }
        public List<User> members { get; set; }
        public List<ChatMessage> messages { get; set; }
    }
}
