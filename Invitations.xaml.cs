/******************************************************************/
/* Archivo: Invitations.xaml.cs                                   */
/* Programador: Daniel Diaz                                       */
/* Fecha: 30/Oct/2021                                             */
/* Fecha modificación:  10/Nov/2021                               */
/* Descripción: Ventana para aceptar o rechazar solicitus de      */
/*              amistad                                           */
/******************************************************************/


using Cliente.ChessService;
using Cliente.Properties.Langs;
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
    /// <summary>
    /// Logica de interaccion para el archivo Invitations.xaml.cs
    /// </summary>
    public partial class Invitations : Window, IRespondServiceCallback
    {
        public RespondServiceClient server;
        public Dictionary<int, string> request;
        public int idUserSend;

        /// <summary>
        /// inicia la ventana Invitations con las invitaciondes del id user que solicita y verifica la conexion
        /// </summary>
        /// <param name="idUser"> id del usuario</param>
        public Invitations(int idUser)
        {
            InitializeComponent();
            InstanceContext instanceContext = new InstanceContext(this);
            try
            {
                server = new RespondServiceClient(instanceContext);
                server.GetRequests(idUser);
            }
            catch (EndpointNotFoundException)
            {
                MessageBox.Show(Lang.noConecction);
                Connected.is_Connected = false;
                this.Close();
            }   
            idUserSend = idUser;
        }

        /// <summary>
        /// recibe una solicitud de amistad
        /// </summary>
        /// <param name="users"></param>
        public void ReciveRequest(Dictionary<int, string> users)
        {
            request = users;
            lboxRequest.Items.Clear();
            
            foreach (var userkey in users.Keys)
            {
                lboxRequest.Items.Add(users[userkey]);
                lboxRequest.ScrollIntoView(lboxRequest.Items.Count - 1);
            }
        }

        /// <summary>
        /// Cierra la ventana
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CloseClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Acepta una solicitud de amistad y envia la informacion al server
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AcceptClick(object sender, RoutedEventArgs e)
        {
            string userIteamName;

            if (lboxRequest.SelectedIndex == -1)
            {
                MessageBox.Show(Lang.selectUser);
            }
            else
            {
                int idUserRecive = -1;
                userIteamName = lboxRequest.SelectedItem.ToString();

                foreach (var requestKey in request.Keys)
                {

                    if (request[requestKey] == userIteamName)
                    {
                        idUserRecive = requestKey;
                        break;
                    }
                }

                try
                {
                    server.ConfirmRequest(true, idUserSend, idUserRecive);
                    server.GetRequests(idUserSend);
                }
                catch (CommunicationObjectFaultedException)
                {
                    MessageBox.Show(Lang.noConecction);
                    Connected.is_Connected = false;
                    this.Close();
                }
            }
            
        }


        /// <summary>
        /// Rechaza una solicitud de amistad y envia la informacion al server
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DenyClick(object sender, RoutedEventArgs e)
        {
            string userIteamName;

            if (lboxRequest.SelectedIndex == -1)
            {
                MessageBox.Show(Lang.selectUser);
            }
            else
            {
                int idUserRecive = -1;
                userIteamName = lboxRequest.SelectedItem.ToString();

                foreach (var requestKey in request.Keys)
                {
                    if (request[requestKey] == userIteamName)
                    {
                        idUserRecive = requestKey;
                        break;
                    }
                }

                try
                {
                    server.ConfirmRequest(false, idUserSend, idUserRecive);
                    server.GetRequests(idUserSend);
                }
                catch (EndpointNotFoundException)
                {
                    MessageBox.Show(Lang.noConecction);
                    Connected.is_Connected = false;
                    this.Close();
                }
            }
        }
    }
}
