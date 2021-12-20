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
    /// <summary>
    /// Interaction logic for JoinMatch.xaml
    /// </summary>
    public partial class JoinMatch : Window, ISendInvitationServiceCallback
    {
        public SendInvitationServiceClient server;
        public int idUser;
        public JoinMatch(int idUser_)
        {
            InitializeComponent();

            idUser = idUser_;
            InstanceContext instanceContext = new InstanceContext(this);
            server = new SendInvitationServiceClient(instanceContext);

        }

        public void GetCodeMatch(bool status, string code)
        {
            throw new NotImplementedException();
        }

        public void ValidateCodeStatus(bool status, string usernameRival, string MatchCode, bool white)
        {
            if (!status)
            {
                MessageBox.Show("An error ocurre");
                return;
            }

            Partida partida = new Partida(idUser, usernameRival, MatchCode, white);
            partida.Show();
            this.Close();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnJoin_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TBCode.Text))
            {
                MessageBox.Show("Put the code");
                return;
            }
            string code = TBCode.Text;
            server.ValidateCodeInvitation(idUser,code);
            
        }

        void ISendInvitationServiceCallback.JoinMatch(string usernameRival, string codeMatch, bool white)
        {
            throw new NotImplementedException();
        }
    }
}
