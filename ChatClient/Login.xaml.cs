using Model;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ChatClient
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public User user;
        public Login()
        {
            InitializeComponent();
        }
        
        private void Login_Click(object sender, RoutedEventArgs e)
        {
            user = new User();
            user.username = username.Text;
            user.password = password.Password;

            string body = "{ \"username\":\"" + user.username + "\" , \"password\":\"" + user.password + "\" }";

            var client = new RestClient(MainWindow.restURL);
            var request = new RestRequest("/user/login/", Method.POST);
            
            request.AddHeader("Accept", "application/json");
            request.Parameters.Clear();
            request.AddParameter("application/json", body, ParameterType.RequestBody);
            
            var response = client.Execute<User>(request);
            user = response.Data;

            if(user == null)
            {
                MessageBox.Show("Username or password are incorrect");
                return;
            }
            else
            {
                DialogResult = true;
            }
            
            Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
