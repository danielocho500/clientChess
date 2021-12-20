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

namespace Cliente
{
    public partial class Stats : Window, IGetStatsServiceCallback
    {
        public GetStatsServiceClient server;
        public Stats(int id)
        {
            InitializeComponent();
            InstanceContext instanceContext = new InstanceContext(this);
            server = new GetStatsServiceClient(instanceContext);
            server.getStats(id);
        }

        public void ShowStats(int Matches_played, int Matches_win, decimal WinP, int eloMax, int eloActual)
        {
            lbMatchesPlayed.Content += " " + Matches_played;
            lbWinP.Content += " " + WinP + "%";
            lbEloA.Content += " " + eloActual;
            lbEloM.Content += " " + eloMax;

        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
