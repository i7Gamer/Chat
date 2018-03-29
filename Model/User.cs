using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Model
{
    public class User
    {
        public int id { get; set; }
        public string username { get;set; }
        public string password { get; set; }
        public string email { get; set; }
        public string firstName {get; set;}
        public string lastName { get; set; }
        public List<string> contacts { get; set; }
        public List<string> groups { get; set; }
        public string status { get; set; }
        public List<string> blockedUsers { get; set; }
        public object picture { get; set; }
        public BitmapImage image { get; set; }
    }
}
