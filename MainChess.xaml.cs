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
    /// <summary>
    /// Logica de interaccion para el archivo MainChess.xaml.cs
    /// </summary>
    public partial class MainChess : Window, IFriendServiceCallback, IRequestServiceCallback
    {
        public RequestServiceClient serverRequest;
        public FriendServiceClient serverFriend;
        public int idUser;

        public string[] usersConnected;
        public string[] usersDisConnected;

        /// <summary>
        /// Inicia la ventana MainChess y conecta con el servidor.
        /// </summary>
        /// <param name="idUser"> agrega el id a la lista de usuarios conectados </param>
        public MainChess(int idUser)
        {
            InitializeComponent();
            InstanceContext instanceContext = new InstanceContext(this);
            serverRequest = new RequestServiceClient(instanceContext);
            serverFriend = new FriendServiceClient(instanceContext);
            this.idUser = idUser;

            try
            {
                serverFriend.Connected(idUser);
            }
            catch (EndpointNotFoundException)
            {
                MessageBox.Show(Lang.noConecction);
                Connected.is_Connected = false;

                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();

                this.Close();
            }
            Connected.is_Connected = true;
        }

        /// <summary>
        /// Incia la ventana GetCodeMatch y verifica la conexión al server
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NewGameClick(object sender, RoutedEventArgs e)
        {
            GetCodeMatch getCodeMatch = new GetCodeMatch(idUser);
            try
            {
                getCodeMatch.ShowDialog();
            }
            catch (InvalidOperationException)
            {
                Console.WriteLine("Eror");
            }

            if (!Connected.is_Connected)
            {
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close();
            }
        }

        /// <summary>
        /// Inia la ventana JoinMatch y verifica la conexión al server
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void JoinToGameClick(object sender, RoutedEventArgs e)
        {
            JoinMatch joinMatch = new JoinMatch(idUser);

            try
            {
                joinMatch.ShowDialog();
            }
            catch (InvalidOperationException)
            {
                Console.WriteLine("Eror");
            }

            if (!Connected.is_Connected)
            {
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close();
            }
        }

        /// <summary>
        /// Incia la ventana Stats y verifica la conexión al server
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StatsClick(object sender, RoutedEventArgs e)
        {
            Stats stats = new Stats(idUser);

            try
            {
                stats.ShowDialog();
            }
            catch (InvalidOperationException)
            {
                Console.WriteLine("Eror");
            }

            if (!Connected.is_Connected)
            {
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close();
            }
        }

        /// <summary>
        /// Inicia la ventana Ranking y verifica la conexión al server
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RankingClick(object sender, RoutedEventArgs e)
        {
            try
            {
                new Ranking(idUser).ShowDialog();
            }
            catch (InvalidOperationException)
            {
                Console.WriteLine("Eror");
            }

            if (!Connected.is_Connected)
            {
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close();
            }
        }

        /// <summary>
        /// Quita el idUser de los usuarios conectados y cierra la ventana MainChess
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LogoutClick(object sender, RoutedEventArgs e)
        {
            Login login = new Login();
            login.Show();

            try
            {
                serverFriend.Disconnected(idUser);
            }
            catch (EndpointNotFoundException)
            {
                Console.WriteLine("Eror");
            }
            catch (CommunicationObjectFaultedException)
            {
                Console.WriteLine("Eror");
            }
            this.Close();
        }

        /// <summary>
        /// Inicia la ventana Configuracion y verifica la conexión al server.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConfigClick(object sender, RoutedEventArgs e)
        {
            Configuration config = new Configuration(idUser);
            config.Show();
            this.Close();
        }

        /// <summary>
        /// Inicia la ventana Invitation y verifica la conexión al server
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InvitationsClick(object sender, RoutedEventArgs e)
        {
            Invitations invitations = new Invitations(idUser);

            try
            {
                invitations.ShowDialog();
            }
            catch (InvalidOperationException)
            {
                Console.WriteLine("Eror");
            }

            if (!Connected.is_Connected)
            {
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close();
            }
        }

        /// <summary>
        /// Verifica el campo txtAddUser , la conexión al server y manda la peticion al servidor.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddUserClick(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtAddUser.Text))
            {
                if(CountSpaces(txtAddUser.Text) != 0)
                {
                    MessageBox.Show(Lang.noSpaces);
                }
                else
                {
                    try
                    {
                        serverRequest.SendRequest(txtAddUser.Text, idUser);
                    }
                    catch (EndpointNotFoundException)
                    {
                        MessageBox.Show(Lang.noConecction);
                        Connected.is_Connected = false;
                        MainWindow mainWindow = new MainWindow();
                        mainWindow.Show();
                        this.Close();
                    }
                }
            }
            else
            {
                MessageBox.Show(Lang.enterUsername);
            }
        }

        /// <summary>
        /// Lo invoca el server con el estado de la peticion de amistad.
        /// </summary>
        /// <param name="status"></param>
        public void SendRequestStatus(int status)
        {
            switch(status) {
                case 0:
                    MessageBox.Show(Lang.successRequest);
                    break;
                case 1:
                    MessageBox.Show(Lang.requestError);
                    break;
                case 2:
                    MessageBox.Show(Lang.requestFriendAlready);
                    break;
                case 3:
                    MessageBox.Show(Lang.requestAlready);
                    break;
                case 4:
                    MessageBox.Show(Lang.requestRejected);
                    break;
                case 5:
                    MessageBox.Show(Lang.userNotFound);
                    break;
                case 6:
                    MessageBox.Show(Lang.requestAuto);
                    break;
            }
            txtAddUser.Text = "";
        }

        /// <summary>
        /// Recupera tus amigos conectados y los desconectados.
        /// </summary>
        /// <param name="usernamesConnected"> amigos conectados</param>
        /// <param name="usernamesDisconnected">amigos desconectados</param>
        public void GetUsers(string[] usernamesConnected, string[] usernamesDisconnected)
        {
            usersConnected = usernamesConnected.ToArray();
            usersDisConnected = usernamesDisconnected.ToArray();
            UpdadteUsers(usernamesConnected, usernamesDisconnected);
        }

        /// <summary>
        /// Pone a un amigo que estaba en lista desconectado lo pone en la lista conectado.
        /// </summary>
        /// <param name="username"></param>
        public void NewConecction(string username)
        {
            List<string> usersConnectedAux = usersConnected.ToList();
            List<string> usersDisconectAux = usersDisConnected.ToList();
            usersConnectedAux.Add(username);
            usersDisconectAux.Remove(username);
            usersConnected = usersConnectedAux.ToArray();
            usersDisConnected = usersDisconectAux.ToArray();
            UpdadteUsers(usersConnected,usersDisConnected);
        }

        /// <summary>
        /// Pone a un amigo que estaba en lista conectado lo pone en la lista desconectado.
        /// </summary>
        /// <param name="username"></param>
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

        /// <summary>
        /// Metodo al evento cuandos se cierra la ventana, borra al id del usuario de los usuarios conectados. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                serverFriend.Disconnected(idUser);
            }
            catch (CommunicationObjectFaultedException)
            {
                Console.WriteLine("Eror");
            }
            
        }

        /// <summary>
        /// Actualiza los amigos conectados y desconectados de la tabla.
        /// </summary>
        /// <param name="usernamesConnected"> amigos conectados</param>
        /// <param name="usernamesDisconnected">amigos desconectados</param>
        public void UpdadteUsers(string[] usernamesConnected, string[] usernamesDisconnected)
        {
            listViewUsers.Items.Clear();

            foreach (string user in usernamesConnected)
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

        /// <summary>
        /// Se usa para verificar que el cliente tiene conexion con el server.
        /// </summary>
        public void SeeConecction()
        {
        }

        /// <summary>
        /// Cuando agregas a alguien lo añade a la lista de conectados.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="isConnected"></param>
        public void newFriend(string username, bool isConnected)
        {
            if (isConnected)
                NewConecction(username);
            else
                NewDisconecction(username);
        }

        /// <summary>
        /// Cuenta los espacios en blanco de un texto.
        /// </summary>
        /// <param name="text"> texto a evaluar</param>
        /// <returns>regresa count con el numero de espacios en blanco.</returns>
        public int CountSpaces(string text)
        {
            int cont = 0;
            string letter;

            for (int i = 0; i < text.Length; i++)
            {
                letter = text.Substring(i, 1);

                if (letter == " ")
                {
                    cont++;
                }
            }
            return cont;
        }
    }
}
