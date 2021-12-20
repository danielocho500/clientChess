/******************************************************************/
/* Archivo: Connected.xaml.cs                                     */
/* Programador: Daniel Diaz Rossell                               */
/* Fecha: 26/Oct/2021                                             */
/* Fecha modificación:  29/Oct/2021                               */
/* Descripción: Valida si sigues conectado o no                   */
/******************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cliente
{
    static class Connected
    {
        private static bool _isConnected = new bool();

        /// <summary>
        /// Variable para saber el estado de la conexion
        /// </summary>
        public static bool is_Connected
        {
            
                set { _isConnected = value; }
                get { return _isConnected; }
            
        }
    }
}
