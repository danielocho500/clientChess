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
    /// Interaction logic for Ranking.xaml
    /// </summary>
    public partial class Ranking : Window, IRankingServiceCallback
    {
        public RankingServiceClient server;
        public Ranking()
        {
            InitializeComponent();
            /*InstanceContext instanceContext = new InstanceContext(this);
            server = new RankingServiceClient(instanceContext);
            server.getRanking();*/
        }

        public void ShowRanking(Tuple<string, int>[] rank)
        {
            foreach (Tuple<string, int> element in rank)
            {
                var player = new { UserName = element.Item1, Elo = element.Item2 };
                lvRank.Items.Add(player);
            }
        }
    }
}
