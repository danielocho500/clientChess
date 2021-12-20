/******************************************************************/
/* Archivo: Login.xaml.cs                                         */
/* Programador: Raul Peredo                                       */
/* Fecha: 18/Oct/2021                                             */
/* Fecha modificación:  22/Oct/2021                               */
/* Descripción: Ventana para iniciar sesion en el sistema         */
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
    /// Logica de interaccion para el archivo Login.xaml.cs
    /// </summary>
    public partial class Login: Window, ILoginServiceCallback
    {
        public LoginServiceClient server;

        /// <summary>
        /// Incializa la ventana Login
        /// </summary>
        public Login()
        {
            InitializeComponent();
            InstanceContext instanceContext = new InstanceContext(this);
            server = new LoginServiceClient(instanceContext);
        }

        /// <summary>
        /// Regresa el estatus del login, invocado por el servidor
        /// </summary>
        /// <param name="status"> estado del logueo</param>
        /// <param name="idUser"> id del usuario solicitante </param>
        public void LoginStatus(int status, int idUser) 
        {
            btnBack.IsEnabled = true;
            btnLogin.IsEnabled = true;

            switch (status)
            {
                case 0:
                    MainChess mainchess = new MainChess(idUser);
                    mainchess.Show();
                    this.Close();
                    break;
                case 1:
                    MessageBox.Show(Lang.errorLogin);
                    break;
                case 2:
                    MessageBox.Show(Lang.incorrectCredentials);
                    break;
                case 3:
                    MessageBox.Show(Lang.alreadyLogged);
                    break;
            }
        }

        /// <summary>
        /// Verifica los campos, verifica la conexión y manda petición del logueo al servidor
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoginClick(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtUsername.Text) && (!string.IsNullOrEmpty(pbPassword.Password)))
            {
                if ((CountSpaces(txtUsername.Text) != 0) || (CountSpaces(pbPassword.Password) != 0))
                {
                    MessageBox.Show(Lang.noSpaces);
                }
                else
                {
                    btnBack.IsEnabled = false;
                    btnLogin.IsEnabled = false;
                    try
                    {
                        server.Login(txtUsername.Text, pbPassword.Password);
                    }
                    catch (EndpointNotFoundException)
                    {
                        MessageBox.Show(Lang.noConecction);
                        MainWindow mainWindow = new MainWindow();
                        mainWindow.Show();
                        this.Close();
                    }
                }
                
            }
            else
            {
                MessageBox.Show(Lang.emptyFields);
            }
        }

        /// <summary>
        /// Cierra la ventana y abre MainWindow
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BackClick(object sender, RoutedEventArgs e)
        {
            MainWindow mainw = new MainWindow();
            mainw.Show();
            this.Close();
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
