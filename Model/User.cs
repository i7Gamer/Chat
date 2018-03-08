using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class User
    {
        private string username
        {
            get { return username; }
            set { username = value; }
        }
        private string password { get; set; }
        private string email { get; set; }
        private string firstName {get; set;}
        private string lastName { get; set; }
        private List<User> contacts { get; set; }
        private List<Group> groups { get; set; }
        private string status { get; set; }
        private List<User> blockedUsers { get; set; }
        private object picture { get; set; }
    }
}
