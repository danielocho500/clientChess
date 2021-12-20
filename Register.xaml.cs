/******************************************************************/
/* Archivo: Register.xaml.cs                                      */
/* Programador: Raul Peredo                                       */
/* Fecha: 18/Oct/2021                                             */
/* Fecha modificación:  22/Oct/2021                               */
/* Descripción: Ventana de registro para un nuevo usuario         */
/******************************************************************/


using Cliente.ChessService;
using Cliente.Properties.Langs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Logica de interaccion para el archivo Register.xaml.cs
    /// </summary>
    public partial class Register : Window, IRegisterServiceCallback
    {
        public RegisterServiceClient server;

        /// <summary>
        /// Inicializa la ventana Register.xaml
        /// </summary>
        public Register()
        {
            InitializeComponent();
            InstanceContext instanceContext = new InstanceContext(this);
            server = new RegisterServiceClient(instanceContext);
        }

        /// <summary>
        /// Evalua todos los campos y envia peticion de registro al servidor con la informacion evaluada.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RegisterClick(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtUsername.Text) || !string.IsNullOrEmpty(txtEmail.Text) || !string.IsNullOrEmpty(pbPassword1.Password) || !string.IsNullOrEmpty(pbPassword2.Password))
            {
                if (CountSpaces(txtUsername.Text) != 0 || CountSpaces(txtEmail.Text) != 0 || CountSpaces(pbPassword1.Password) != 0 || CountSpaces(pbPassword2.Password) != 0)
                {
                    MessageBox.Show(Lang.noSpaces);
                    return;
                }

                if (CheckPasswords () == true)
                {

                    if (ValidateEmail())
                    {

                        if (SafePassword(pbPassword1.Password))
                        {
                            try
                            {
                                server.GenerateCodeRegister(txtUsername.Text, pbPassword1.Password, txtEmail.Text);
                            }
                            catch (EndpointNotFoundException)
                            {
                                MainWindow mainWindow = new MainWindow();
                                mainWindow.Show();
                                this.Close();
                            }
                        }
                        else
                        {
                            MessageBox.Show(Lang.badPassword);
                        }
                    }
                    else
                    {
                        MessageBox.Show(Lang.badEmail);
                    }
                }
                else
                {
                    if (CheckPasswords() == false)
                    {
                        MessageBox.Show(Lang.samePass);
                    }
                }
            }
            else
            {
                MessageBox.Show(Lang.emptyFields);
            }
        }

        /// <summary>
        /// Cierra la ventana Register
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelClick(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        /// <summary>
        /// Pregunta al server si el codigo de verificacion es codigo es correcto. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ValidateClick(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtCode.Text))
            {
                if (CountSpaces(txtCode.Text) !=0 )
                {
                    try
                    {
                        server.VerificateCode(txtCode.Text);
                    }
                    catch (EndpointNotFoundException)
                    {
                        MainWindow mainWindow = new MainWindow();
                        mainWindow.Show();
                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show(Lang.emptyFields);
                }
            }
        }

        /// <summary>
        /// Verifica si las contraseñas son iguales
        /// </summary>
        /// <returns>Regresa true o false segun sea el caso</returns>
        private bool CheckPasswords()
        {
            string pass1 = pbPassword1.Password;
            string pass2 = pbPassword2.Password;
            if (pass1 == pass2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Evalua si el email cumple con las reglas escritas.
        /// </summary>
        /// <returns> regresa si el email es valído </returns>
        private bool ValidateEmail()
        {
            Regex regex = new Regex(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", RegexOptions.CultureInvariant | RegexOptions.Singleline);
            return regex.IsMatch(txtEmail.Text);
        }

        /// <summary>
        /// Evalua si la contraseña es segura o confiable.
        /// </summary>
        /// <param name="passWord"></param>
        /// <returns> regresa true o false dependiendo si la contraseña es o no segura</returns>
        static bool SafePassword(string passWord)
        {
            int validConditions = 0;
            foreach (char c in passWord)
            {
                if (c >= 'a' && c <= 'z')
                {
                    validConditions++;
                    break;
                }
            }
            foreach (char c in passWord)
            {
                if (c >= 'A' && c <= 'Z')
                {
                    validConditions++;
                    break;
                }
            }
            if (validConditions == 0) return false;
            foreach (char c in passWord)
            {
                if (c >= '0' && c <= '9')
                {
                    validConditions++;
                    break;
                }
            }
            if (validConditions == 1) return false;
            if (validConditions == 2)
            {
                char[] special = { '@', '#', '$', '%', '^', '&', '+', '=' };  
                if (passWord.IndexOfAny(special) == -1) return false;
            }
            return true;
        }

        /// <summary>
        /// El server manda el estatus del código.
        /// </summary>
        /// <param name="status"> indica el estado del código </param>
        public void ValidateCode(int status)
        {
            if (status == 0)
            {
                MessageBox.Show(Lang.codeSuccess);
                Login login = new Login();
                login.Show();

                this.Close();

            }
            else if (status == 1)
            {
                MessageBox.Show(Lang.codeFail);
            }
            else if (status == 2)
            {
                MessageBox.Show(Lang.codeIncorrect);
            }
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

        /// <summary>
        /// Regresa si se puedo generar el código o no, invocado por el servidor.
        /// </summary>
        /// <param name="status"> estado del código</param>
        public void CodeRecieve(int status)
        {
            if (status== 0)
            {
                lbCode.Visibility = Visibility.Visible;
                txtCode.Visibility = Visibility.Visible;
                btnValidate.Visibility = Visibility.Visible;

                txtUsername.Text = "";
                txtUsername.IsEnabled = false;

                txtEmail.Text = "";
                txtEmail.IsEnabled = false;

                pbPassword1.Password = "";
                pbPassword1.IsEnabled = false;

                pbPassword2.Password = "";
                pbPassword2.IsEnabled = false;
            }
            else if (status == 1)
            {
                MessageBox.Show(Lang.error);
            }
            else if (status == 2)
            {
                MessageBox.Show(Lang.emailRegistered);
            }
            else if (status == 3)
            {
                MessageBox.Show(Lang.userTaken);
            }
        }
    }
}
