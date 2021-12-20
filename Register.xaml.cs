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
    public partial class Register : Window, IRegisterServiceCallback
    {
        public RegisterServiceClient server;

        public Register()
        {
            InitializeComponent();
            InstanceContext instanceContext = new InstanceContext(this);
            server = new RegisterServiceClient(instanceContext);
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtUsername.Text) || !string.IsNullOrEmpty(txtEmail.Text) || !string.IsNullOrEmpty(pssPassword1.Password) || !string.IsNullOrEmpty(pssPassword2.Password))
            {
                if (CountSpaces(txtUsername.Text) != 0 || CountSpaces(txtEmail.Text) != 0 || CountSpaces(pssPassword1.Password) != 0 || CountSpaces(pssPassword2.Password) != 0)
                {
                    MessageBox.Show(Lang.noSpaces);
                    return;
                }
                if (Check_Passwords () == true)
                {
                    
                    if (Validate_Email())
                    {

                        if (Safe_Password(pssPassword1.Password))
                        {
                            try
                            {
                                server.GenerateCodeRegister(txtUsername.Text, pssPassword1.Password, txtEmail.Text);
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
                    if (Check_Passwords() == false)
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

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void Validate_Click(object sender, RoutedEventArgs e)
        {
            server.VerificateCode(txtCode.Text);
        }


        private bool Check_Passwords()
        {
            string pass1 = pssPassword1.Password;
            string pass2 = pssPassword2.Password;
            if (pass1 == pass2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool Validate_Email()
        {
            Regex regex = new Regex(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", RegexOptions.CultureInvariant | RegexOptions.Singleline);
            return regex.IsMatch(txtEmail.Text);
        }

        static bool Safe_Password(string passWord)
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

                pssPassword1.Password = "";
                pssPassword1.IsEnabled = false;

                pssPassword2.Password = "";
                pssPassword2.IsEnabled = false;
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
