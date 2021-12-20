/******************************************************************/
/* Archivo: Invitations.xaml.cs                                   */
/* Programador: Daniel Diaz                                       */
/* Fecha: 26/Oct/2021                                             */
/* Fecha modificación:  29/Oct/2021                               */
/* Descripción: Ventana para aceptar o rechazar solicitus de      */
/*              amistad                                           */
/******************************************************************/


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

    public partial class Invitations : Window, IRespondServiceCallback
    {
        public RespondServiceClient server;
        public Dictionary<int, string> request;
        public int idUserSend;

        public Invitations(int idUser)
        {
            InitializeComponent();
            /*InstanceContext instanceContext = new InstanceContext(this);
            server = new RespondServiceClient(instanceContext);
            server.GetRequests(idUser);
            idUserSend = idUser;*/
        }

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

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Accept_Click(object sender, RoutedEventArgs e)
        {
            string userIteamName;

            if (lboxRequest.SelectedIndex == -1)
            {
                MessageBox.Show("Select an user in the list");
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

                server.ConfirmRequest(true, idUserSend, idUserRecive);
                server.GetRequests(idUserSend);
            }
            
        }

        private void Deny_Click(object sender, RoutedEventArgs e)
        {
            string userIteamName;

            if (lboxRequest.SelectedIndex == -1)
            {
                MessageBox.Show("Select an user in the list");
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

                server.ConfirmRequest(false, idUserSend, idUserRecive);
                server.GetRequests(idUserSend);
            }
        }
    }
}
