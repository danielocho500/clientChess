using Cliente.ChessService;
using Cliente.Properties.Langs;
using System;
using System.ServiceModel;
using System.Windows;

namespace Cliente
{
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

        public void ValidateCodeStatus(int status, string usernameRival, string username, string MatchCode, bool white)
        {
            switch (status)
            {
                case 0:
                    Play play = new Play(idUser, usernameRival, username, MatchCode, white);
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

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnJoin_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TBCode.Text))
            {
                MessageBox.Show(Lang.putCode);
                return;
            }
            string code = TBCode.Text;
            try
            {
                server.ValidateCodeInvitation(idUser, code);
            }
            catch (EndpointNotFoundException)
            {
                MessageBox.Show(Lang.noConecction);
                Connected.IsConnected = false;
                this.Close();
            }

        }

        void ISendInvitationServiceCallback.JoinMatch(string usernameRival, string username, string codeMatch, bool white)
        {
            throw new NotImplementedException();
        }
    }
}
