using Model;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
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
        User user;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Login login = new Login();
            bool? success = login.ShowDialog();
            user = login.user;

            if (success.HasValue && success.Value)
            {
                var client = new RestClient(restURL + "user/getContacts");
                RestRequest request = new RestRequest();
                request.AddParameter("id", user.id);

                var response = client.Execute<List<User>>(request);
                List<User> users = response.Data;

                if(users != null)
                {
                    foreach(User u in users)
                    {
                        if(u.picture == null)
                        {
                            u.image = ToBitmapImage(Properties.Resources.Image1);
                        }
                        else
                        {
                            u.image = (BitmapImage)u.picture;
                        }
                    }
                    userList.ItemsSource = users;
                }
            }
            else
            {
                Close();
            }
        }

        public BitmapImage ToBitmapImage(Bitmap bitmap)
        {
            using (var memory = new MemoryStream())
            {
                bitmap.Save(memory, ImageFormat.Png);
                memory.Position = 0;

                var bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memory;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();
                bitmapImage.Freeze();

                return bitmapImage;
            }
        }

        private void userListSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // TODO show chat if already exists
            User user = (User)userList.SelectedItem;
            username.Text = user.username;
            userImage.Source = user.image;
            userstatus.Text = user.status;
        }
        
        private void onKeyDownChat(object sender, KeyEventArgs e)
        {
            // check if enter pressed if yes send message
            // also check if chat between the two users has been created yet
        }
    }
}
