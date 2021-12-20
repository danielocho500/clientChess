/******************************************************************/
/* Archivo: MainChess.xaml.cs                                     */
/* Programador: Daniel Diaz                                       */
/* Fecha: 26/Oct/2021                                             */
/* Fecha modificación:  13/dic/2021                               */
/* Descripción: Ventana principal del sistema donde se accede a   */
/*              las diferentes funcionalidades del juego          */
/******************************************************************/


using Cliente.ChessService;
using Cliente.Properties.Langs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Windows;

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

            try
            {
                server_friend.Connected(idUser_);
            }
            catch (EndpointNotFoundException)
            {
                MessageBox.Show(Lang.noConecction);
                Connected.IsConnected = false;

                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();

                this.Close();
            }

            Connected.IsConnected = true;
        }

        private void NewGame_Click(object sender, RoutedEventArgs e)
        {
            GetCodeMatch getCodeMatch = new GetCodeMatch(idUser);
            try
            {
                getCodeMatch.ShowDialog();
            }
            catch (InvalidOperationException)
            {
            }

            if (!Connected.IsConnected)
            {
                MainWindow mainWindow = new MainWindow();

                mainWindow.Show();
                
                this.Close();
            }
        }

        private void JoinToGame_Click(object sender, RoutedEventArgs e)
        {
            JoinMatch joinMatch = new JoinMatch(idUser);

            try
            {
                joinMatch.ShowDialog();
            }
            catch (InvalidOperationException)
            {
            }

            if (!Connected.IsConnected)
            {
                MainWindow mainWindow = new MainWindow();

                mainWindow.Show();

                this.Close();
            }
        }

        private void Stats_Click(object sender, RoutedEventArgs e)
        {
            Stats stats = new Stats(idUser);
            

            try
            {
                stats.ShowDialog();
            }
            catch (InvalidOperationException)
            {
            }

            if (!Connected.IsConnected)
            {
                MainWindow mainWindow = new MainWindow();

                mainWindow.Show();

                this.Close();
            }
        }

        private void Ranking_Click(object sender, RoutedEventArgs e)
        {
            

            try
            {
                new Ranking(idUser).ShowDialog();
            }
            catch (InvalidOperationException)
            {
            }

            if (!Connected.IsConnected)
            {
                MainWindow mainWindow = new MainWindow();

                mainWindow.Show();

                this.Close();
            }
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            Login lg = new Login();
            lg.Show();
            try
            {
                server_friend.Disconnected(idUser);
            }
            catch (EndpointNotFoundException)
            {
            }
            catch (CommunicationObjectFaultedException) { }

            this.Close();
        }

        private void Config_Click(object sender, RoutedEventArgs e)
        {
            Configuration config = new Configuration(idUser);
            config.Show();
            this.Close();
        }

        private void Invitations_Click(object sender, RoutedEventArgs e)
        {
            Invitations invitations = new Invitations(idUser);

            try
            {
                invitations.ShowDialog();
            }
            catch (InvalidOperationException)
            {
            }

            if (!Connected.IsConnected)
            {
                MainWindow mainWindow = new MainWindow();

                mainWindow.Show();

                this.Close();
            }
        }

        private void AddUser_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtAddUser.Text))
            {
                try
                {
                    server_Request.SendRequest(txtAddUser.Text, idUser);
                }
                catch (EndpointNotFoundException)
                {
                    MessageBox.Show(Lang.noConecction);
                    Connected.IsConnected = false;

                    MainWindow mainWindow = new MainWindow();
                    mainWindow.Show();
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show(Lang.enterUsername);
            }
        }

        public void SendRequestStatus(int status)
        {
            if (status == 0)
            {
                MessageBox.Show(Lang.successRequest);
            }
            else if (status == 1)
            {
                MessageBox.Show(Lang.requestError);
            }
            else if (status == 2)
            {
                MessageBox.Show(Lang.requestFriendAlready);
            }
            else if (status == 3)
            {
                MessageBox.Show(Lang.requestAlready);
            }
            else if (status == 4)
            {
                MessageBox.Show(Lang.requestRejected);
            }
            else if (status == 5)
            {
                MessageBox.Show(Lang.userNotFound);
            }
            else if (status == 6)
            {
                MessageBox.Show(Lang.requestAuto);
            }

            txtAddUser.Text = "";
        }

        public void GetUsers(string[] usernamesConn, string[] usernamesDisc)
        {
            usersConnected = usernamesConn.ToArray();
            usersDisConnected = usernamesDisc.ToArray();
            UpdadteUsers(usernamesConn, usernamesDisc);
        }

        public void NewConecction(string username)
        {
            //Agrega a conectados el usuario
            List<string> usersConnectedAux = usersConnected.ToList();
            List<string> usersDisconectAux = usersDisConnected.ToList();
            usersConnectedAux.Add(username);
            usersDisconectAux.Remove(username);
            usersConnected = usersConnectedAux.ToArray();
            usersDisConnected = usersDisconectAux.ToArray();
            UpdadteUsers(usersConnected,usersDisConnected);
        }

        public void NewDisconecction(string username)
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
            UpdadteUsers(usersConnected, usersDisConnected);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                server_friend.Disconnected(idUser);
            }
            catch (CommunicationObjectFaultedException) { }
            
        }

        public void UpdadteUsers(string[] usernamesConneted, string[] usernamesDisconnected)
        {
            listViewUsers.Items.Clear();

            foreach (string user in usernamesConneted)
            {
                listViewUsers.Items.Add("✅" + user);
                listViewUsers.ScrollIntoView(listViewUsers.Items.Count - 1);
            }

            foreach (string user in usernamesDisconnected)
            {
                listViewUsers.Items.Add("[X]" + user);
                listViewUsers.ScrollIntoView(listViewUsers.Items.Count - 1);
            }
        }

        public void SeeConecction()
        {
        }

        public void newFriend(string username, bool connected)
        {
            if (connected)
                NewConecction(username);
            else
                NewDisconecction(username);
        }
    }
}
