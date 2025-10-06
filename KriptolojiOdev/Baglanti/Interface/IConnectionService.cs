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
        Task<string> ConnectToServer();
        void StopServer();
        public Action<string> OnMessage { get; set; }
    }
}
