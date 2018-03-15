using Database;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                ConnectionProvider provider = new ConnectionProvider();
                UserRepository broker = new UserRepository(provider);
                User user = broker.getUser("test","test");
                if(user != null)
                {
                    Console.WriteLine(user.firstName);
                    Console.WriteLine(user.lastName);
                    Console.WriteLine(user.userName);
                    Console.WriteLine(user.password);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            Console.In.Read();
        }
    }
}
