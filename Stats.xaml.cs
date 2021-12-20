/******************************************************************/
/* Archivo: Stats.xaml.cs                                         */
/* Programador: Daniel Diaz Rossell                               */
/* Fecha: 15/Oct/2021                                             */
/* Fecha modificación:  15/Nov/2021                               */
/* Descripción: Ventana para mostrar las estadisticas de cada     */
/* jugador                                                        */
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
    /// Logica de interaccion para el archivo Stats.xaml.cs
    /// </summary>
    public partial class Stats : Window, IGetStatsServiceCallback
    {
        public GetStatsServiceClient server;

        /// <summary>
        /// Inicializa la ventana con las datos recuperados del usuario
        /// </summary>
        /// <param name="idUser"></param>
        public Stats(int idUser)
        {
            InitializeComponent();
            InstanceContext instanceContext = new InstanceContext(this);
            server = new GetStatsServiceClient(instanceContext);
            try
            {
                server.GetStats(idUser);
            }
            catch (EndpointNotFoundException)
            {
                MessageBox.Show(Lang.noConecction);
                Connected.is_Connected = false;
                this.Close();
            }
        }

        /// <summary>
        /// Muestra los datos recuperados en pantalla.
        /// </summary>
        /// <param name="matchesPlayed"></param>
        /// <param name="matchesWin"></param>
        /// <param name="eloMax"></param>
        /// <param name="eloActual"></param>
        public void ShowStats(int matchesPlayed, int matchesWin, int eloMax, int eloActual)
        {
            lbMatchesPlayed.Content += " " + matchesPlayed;
            lbWinP.Content += " " + matchesWin;
            lbEloA.Content += " " + eloActual;
            lbEloM.Content += " " + eloMax;

        }

        /// <summary>
        /// Cierra la ventana Stats.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExitClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
