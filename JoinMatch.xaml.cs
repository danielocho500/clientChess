/******************************************************************/
/* Archivo: JoinMatch.xaml.cs                                     */
/* Programador: Daniel Diaz Rossell                               */
/* Fecha: 2/nov/2021                                              */
/* Fecha modificación:  13/nov/2021                               */
/* Descripción: Ventana de registro para un nuevo usuario         */
/******************************************************************/

using Cliente.ChessService;
using Cliente.Properties.Langs;
using System;
using System.ServiceModel;
using System.Windows;

namespace Cliente

{
    /// <summary>
    /// Logica de interaccion para el archivo JoinMatch.xaml.cs
    /// </summary>
    public partial class JoinMatch : Window, ISendInvitationServiceCallback
    {
        public SendInvitationServiceClient server;
        public int idUser;

        /// <summary>
        /// Inicia la ventana JoinMatch
        /// </summary>
        /// <param name="idUser"></param>
        public JoinMatch(int idUser)
        {
            InitializeComponent();
            this.idUser = idUser;
            InstanceContext instanceContext = new InstanceContext(this);
            server = new SendInvitationServiceClient(instanceContext);
        }

        /// <summary>
        /// Método no usado por esta clase.
        /// </summary>
        /// <param name="status"></param>
        /// <param name="code"></param>
        public void GetCodeMatch(bool status, string code)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Regresa el estado del codigo de juego, invocado por el servidor.
        /// </summary>
        /// <param name="status"> estado del codigo</param>
        /// <param name="usernameRival"> usuario del rival</param>
        /// <param name="username"> usuario del solicitante</param>
        /// <param name="matchCode"> codigo de la partida</param>
        /// <param name="isWhite"> bool para saber si juega blancas</param>
        public void ValidateCodeStatus(int status, string usernameRival, string username, string matchCode, bool isWhite)
        {
            switch (status)
            {
                case 0:
                    Play play = new Play(idUser, usernameRival, username, matchCode, isWhite);
                    play.Show();
                    this.Close();
                    break;
                case 2:
                    MessageBox.Show(Lang.codeIncorrect);
                    break;
                default:
                    MessageBox.Show(Lang.errorOcurred);
                    break;
            }
           
        }

        /// <summary>
        /// Cierra la ventana
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExitClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Verifica el campo tbCode, la conexion y manda peticion de validacion de codigo al servidor.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void JoinClick(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(tbCode.Text))
            {
                if (CountSpaces(tbCode.Text) != 0)
                {
                    MessageBox.Show(Lang.putCode);
                    return;
                }
                else
                {
                    string code = tbCode.Text;
                    try
                    {
                        server.ValidateCodeInvitation(idUser, code);
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

        /// <summary>
        /// Método no usado por esta clase.
        /// </summary>
        /// <param name="usernameRival"></param>
        /// <param name="username"></param>
        /// <param name="codeMatch"></param>
        /// <param name="white"></param>
        void ISendInvitationServiceCallback.JoinMatch(string usernameRival, string username, string codeMatch, bool white)
        {
            throw new NotImplementedException();
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
