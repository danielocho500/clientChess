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

        public static bool IsConnected
        {
            
                set { _isConnected = value; }
                get { return _isConnected; }
            
        }
    }
}
