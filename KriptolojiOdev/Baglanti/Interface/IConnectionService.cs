using System.Net.Sockets;

namespace KriptolojiOdev.Baglanti.Interface
{
    public interface IConnectionService
    {
        string StartServer();
        Task<(string message, TcpClient client)> ConnectToServer();

        public Action<string> OnMessage { get; set; }
        void Broadcast(string target, string operation, string algorithm, string text, string key, string iv, string secType);
    }
}
