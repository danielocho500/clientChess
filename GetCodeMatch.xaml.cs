/******************************************************************/
/* Archivo: GetCodeMatch.xaml.cs                                 */
/* Programador: Raul Arturo Peredo Estudillo                     */
/* Fecha: 3/Oct/2021                                             */
/* Fecha modificación:  9/Nov/2021                               */
/* Descripción: Ventana para aceptar o rechazar solicitus de      */
/*              amistad                                           */
/******************************************************************/

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
using Cliente.ChessService;
using Cliente.Properties.Langs;

namespace Cliente
{
    /// <summary>
    /// logica de interaciion para GetCodeMatch.xaml
    /// </summary>
    public partial class GetCodeMatch : Window, ISendInvitationServiceCallback
    {
        public SendInvitationServiceClient server;
        public int idUser;
        public string codeMatch;

        /// <summary>
        /// Incia la ventana GetCodeMatch y verifica la conexion con el servidor.
        /// </summary>
        /// <param name="idUser"> id del usuario solicitante</param>
        public GetCodeMatch(int idUser)
        {
            InitializeComponent();
            this.idUser = idUser;
            InstanceContext instanceContext = new InstanceContext(this);
            server = new SendInvitationServiceClient(instanceContext);

            try
            {
                server.GenerateCodeInvitation(idUser);
            }
            catch (EndpointNotFoundException)
            {
                MessageBox.Show(Lang.noConecction);
                Connected.is_Connected = false;
                this.Close();
            }
            
        }

        /// <summary>
        /// Inicia la ventana Play.
        /// </summary>
        /// <param name="usernameRival"> nombre del rival</param>
        /// <param name="username"> nombre del solicitante</param>
        /// <param name="codeMatch"> codigo de la partida</param>
        /// <param name="isWhite"> juega blancas o no</param>
        public void JoinMatch(string usernameRival, string username, string codeMatch, bool isWhite)
        {
            Play play = new Play(idUser, usernameRival, username, codeMatch, isWhite);
            play.Show();
            this.Close();
        }

        /// <summary>
        /// Metodo no implementado.
        /// </summary>
        /// <param name="status"></param>
        /// <param name="usernameRival"></param>
        /// <param name="username"></param>
        /// <param name="codeMatch"></param>
        /// <param name="isWhite"></param>
        public void ValidateCodeStatus(int status, string usernameRival, string username, string codeMatch, bool isWhite)
        {
            throw new NotImplementedException();

        }

        /// <summary>
        /// Cierra la ventana y elimina el codigo del servidor
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExitClick(object sender, RoutedEventArgs e)
        {
            try
            {
                server.DeleteCodeInvitation(codeMatch);
            }
            catch (EndpointNotFoundException)
            {
                MessageBox.Show(Lang.noConecction);
                Connected.is_Connected = false;
                this.Close();
            }
            this.Close();
        }

        /// <summary>
        /// el servidor manda el codigo de la partida
        /// </summary>
        /// <param name="status"> status del codigo</param>
        /// <param name="code"> codigo de la partida</param>
        void ISendInvitationServiceCallback.GetCodeMatch(bool status, string code)
        {
            codeMatch = code;
            lbCode.Content = code;
        }
    }
}
