using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class ChatMessage
    {
        public int chatId { get; set; }
        public string message { get; set; }
        public int senderId { get; set; }
        public DateTime timestamp { get; set; }
    }
}
