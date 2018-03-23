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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ChatClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static string restURL = "http://rzipas.win:8170/api/";

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Login login = new Login();
            bool? success = login.ShowDialog();

            if (success.HasValue && success.Value)
            {
                var client = new RestClient(restURL + "user");
                var response = client.Execute<List<User>>(new RestRequest());
                List<User> users = response.Data;

                if(users != null)
                {
                    foreach(User u in users)
                    {
                        userList.Items.Add(u.username);
                    }
                }
            }
            else
            {
                Close();
            }
        }

        private void userListSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // TODO show chat
        }
    }
}
