using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KriptolojiOdev.Baglanti.Interface
{
    public interface IConnectionService
    {
        string StartServer();
        void ConnectToServer();
        void StopServer();
    }
}
