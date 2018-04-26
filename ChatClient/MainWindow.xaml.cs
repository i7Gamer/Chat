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
        User loggedInUser;
        User currentlySelectedUser;
        Chat currentChat;
        bool currentChatCreated = false;

        List<Chat> allChats;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Login login = new Login();
            bool? success = login.ShowDialog();
            loggedInUser = login.user;
            
            if (success.HasValue && success.Value)
            {
                loggedInUserName.Text = loggedInUser.username;

                if (loggedInUser.picture == null)
                {
                    loggedInUser.image = ToBitmapImage(Properties.Resources.Image1);
                }
                loggedInUserImage.Source = loggedInUser.image;

                var client = new RestClient(restURL + "user/getContacts");
                RestRequest request = new RestRequest();
                request.AddParameter("id", loggedInUser.id);

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

                client = new RestClient(restURL + "chat/getAllChats");
                request = new RestRequest();
                request.AddParameter("id", loggedInUser.id);

                var allChatsResponse = client.Execute<List<Chat>>(request);
                allChats = allChatsResponse.Data;
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
            chatList.Items.Clear();

            // show user data on top
            currentlySelectedUser = (User)userList.SelectedItem;
            username.Text = currentlySelectedUser.username;
            userImage.Source = currentlySelectedUser.image;
            userstatus.Text = currentlySelectedUser.status;


            // TODO show chat if already exists
            // find chat
            var client = new RestClient(restURL + "chat/getChat");
            RestRequest request = new RestRequest();
            request.AddParameter("userIdOne", currentlySelectedUser.id);
            request.AddParameter("userIdTwo", loggedInUser.id);
            var response1 = client.Execute<Chat>(request);
            currentChat = response1.Data;
            
            if (currentChat == null)
            {
                currentChatCreated = false;
                return;
            }

            currentChatCreated = true;

            // get chat messages
            client = new RestClient(restURL + "chat/getChatMessages");
            request = new RestRequest();
            request.AddParameter("chatId", currentChat.id);
            var response2 = client.Execute<List<ChatMessage>>(request);
            List<ChatMessage> messages = response2.Data;

            // insert chat messages
            foreach (ChatMessage msg in messages)
            {
                User currentUser;
                if(msg.senderId == currentlySelectedUser.id)
                {
                    currentUser = currentlySelectedUser;
                }
                else
                {
                    currentUser = loggedInUser;
                }
                chatList.Items.Add(currentUser.username + ": " + msg.message);
            }
        }
        
        private void onKeyDownChat(object sender, KeyEventArgs e)
        {
            if(userList.SelectedItem == null)
            {
                return;
            }

            // check if enter pressed if yes send message
            if(e.Key == Key.Enter)
            {
                // check if chat between the two users has been created yet, if not, create
                if(currentChatCreated == false)
                {
                    TwoPersonChat tpc = new TwoPersonChat();
                    tpc.host = loggedInUser.id;
                    tpc.member = currentlySelectedUser.id;
                    tpc.title = "Chat between " + loggedInUser.username + " and " + currentlySelectedUser.username;

                    // create chat
                    string bodyC = "{ \"title\":\"" + tpc.title + "\",\"host\":\"" + tpc.host + "\" , \"member\":\"" + tpc.member + "\"}";

                    var clientC = new RestClient(restURL);
                    var requestC = new RestRequest("/chat/createNewChat/", Method.POST);

                    requestC.AddHeader("Accept", "application/json");
                    requestC.Parameters.Clear();
                    requestC.AddParameter("application/json", bodyC, ParameterType.RequestBody);

                    var responseC = clientC.Execute<Chat>(requestC);
                    currentChat = responseC.Data;
                }

                TextBox textBox = (TextBox)sender;
                string message = textBox.Text;
                chatList.Items.Add(loggedInUser.username + ": " + message);

                ChatMessage cm = new ChatMessage();
                cm.chatId = currentChat.id;
                cm.message = message;
                cm.senderId = loggedInUser.id;
                cm.timestamp = DateTime.UtcNow;

                // save message
                string body = "{ \"chatId\":\"" + cm.chatId + "\" , \"message\":\"" + cm.message + "\", \"senderId\":\"" 
                    + cm.senderId + "\" , \"timestamp\":\"" + cm.timestamp + "\"}";

                var client = new RestClient(restURL);
                var request = new RestRequest("/chat/saveMessage/", Method.POST);

                request.AddHeader("Accept", "application/json");
                request.Parameters.Clear();
                request.AddParameter("application/json", body, ParameterType.RequestBody);

                var response = client.Execute<Chat>(request);
                currentChat = response.Data;

                // empty textbox
                textBox.Text = "";
            }
        }
    }
}
