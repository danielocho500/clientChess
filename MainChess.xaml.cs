/******************************************************************/
/* Archivo: MainChess.xaml.cs                                     */
/* Programador: Daniel Diaz                                       */
/* Fecha: 26/Oct/2021                                             */
/* Fecha modificación:  29/Oct/2021                               */
/* Descripción: Ventana principal del sistema donde se accede a   */
/*              las diferentes funcionalidades del juego          */
/******************************************************************/

using Cliente.SuperChess;
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
    public partial class MainChess : Window, IFriendServiceCallback, IRequestServiceCallback
    {
        public RequestServiceClient server_Request;
        public FriendServiceClient server_friend;
        public int idUser;
        public string[] usersConnected;
        public string[] usersDisConnected;

        public MainChess(int idUser_)
        {
            InitializeComponent();
            InstanceContext instanceContext = new InstanceContext(this);
            server_Request = new RequestServiceClient(instanceContext);
            server_friend = new FriendServiceClient(instanceContext);
            idUser = idUser_;
            server_friend.Connected(idUser_);
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
            server_friend.Disconnected(idUser);
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
                server_Request.sendRequest(txtAddUser.Text,idUser);
            }
            else
            {
                MessageBox.Show("Please, enter an username");
            }
        }

        public void sendRequestStatus(bool status, string msg)
        {
            MessageBox.Show(msg);
        }

        public void getUsers(string[] usernamesConn, string[] usernamesDisc)
        {
            usersConnected = usernamesConn.ToArray();
            usersDisConnected = usernamesDisc.ToArray();
            updadteUsers(usernamesConn, usernamesDisc);
        }

        public void newConecction(string username)
        {
            List<string> usersConnectedAux = usersConnected.ToList();
            List<string> usersDisconectAux = usersDisConnected.ToList();
            usersConnectedAux.Add(username);
            usersDisconectAux.Remove(username);
            usersConnected = usersConnectedAux.ToArray();
            usersDisConnected = usersDisconectAux.ToArray();
            updadteUsers(usersConnected,usersDisConnected);
        }

        public void newDisconecction(string username)
        {
            List<string> usersConnectedAux = usersConnected.ToList();
            List<string> usersDisconectAux = usersDisConnected.ToList();
            usersConnectedAux.Remove(username);

            if (!usersDisconectAux.Contains(username))
            {
                usersDisconectAux.Add(username);
            }

            usersConnected = usersConnectedAux.ToArray();
            usersDisConnected = usersDisconectAux.ToArray();
            updadteUsers(usersConnected, usersDisConnected);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            server_friend.Disconnected(idUser); 
        }

        public void updadteUsers(string[] usernamesConneted, string[] usernamesDisconnected)
        {
            listViewUsers.Items.Clear();
            string con = "";
            string dis = "";

            foreach (string user in usernamesConneted)
            {
                listViewUsers.Items.Add("✅" + user);
                listViewUsers.ScrollIntoView(listViewUsers.Items.Count - 1);
                con += " " + user;
            }

            foreach (string user in usernamesDisconnected)
            {
                listViewUsers.Items.Add("[X]" + user);
                listViewUsers.ScrollIntoView(listViewUsers.Items.Count - 1);
                dis += " " + user; 
            }
        }
    }
}
