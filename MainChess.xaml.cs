using Cliente.ChessService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
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

namespace Cliente
{
    public partial class MainChess : Window,IRequestServiceCallback, IConnectedFriendsServiceCallback
    {
        public RequestServiceClient server;
        public ConnectedFriendsServiceClient serverFriends;
        public int idUser;
        public MainChess(int idUser_)
        {
            InitializeComponent();
            InstanceContext instanceContext = new InstanceContext(this);
            server = new RequestServiceClient(instanceContext);
            idUser = idUser_;

            serverFriends = new ConnectedFriendsServiceClient(instanceContext);
            serverFriends.Connected(idUser);

        }

        private void NewGame_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("New Game... in development");
        }

        private void JoinToGame_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Join to game... in development");
        }

        private void Stats_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Stats..... in development");
        }

        private void Ranking_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Ranking... in development");
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            Login lg = new Login();
            lg.Show();
            MessageBox.Show("Sesion closed");
            this.Close();
        }

        private void Config_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Config .... in development");
        }

        private void Invitations_Click(object sender, RoutedEventArgs e)
        {
            Invitations invitations = new Invitations(idUser);
            invitations.Show();

        }

        private void AddUser_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtAddUser.Text))
            {
                server.sendRequest(txtAddUser.Text,idUser);
            }
            else{
                MessageBox.Show("Please, enter an username");
            }
        }

        public void sendRequestStatus(bool status, string msg)
        {
            MessageBox.Show(msg);
        }

        private void TestChatW_Click(object sender, RoutedEventArgs e)
        {
            Chat ch = new Chat();
            ch.Show();
        }

        public void getUser(string[] usernames)
        {
            string username = "";

            foreach (string user in usernames)
            {
                username += user;
            }
            MessageBox.Show(username);

        }

        public void newConection(string username)
        {
            throw new NotImplementedException();
        }

        public void newDisconection(string username)
        {
            throw new NotImplementedException();
        }
    }
}
