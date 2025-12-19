using KriptolojiOdev.Sifreleme.Interface;
using System.Text;

namespace KriptolojiOdev.Sifreleme.Class
{
    public class TransportSecurityService : ITransportSecurityService
    {
        private readonly string TunnelKey = "X7kP9mL2nQ5wE1rT4yU8iO";
        
        private string XorCipher(string input)
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < input.Length; i++)
            {
                char inputChar = input[i];
                char keyChar = TunnelKey[i % TunnelKey.Length];

                char encryptedChar = (char)(inputChar ^ keyChar);
                sb.Append(encryptedChar);
            }

            return sb.ToString();
        }

        public string Encrypt(string plainText)
        {
            if (string.IsNullOrEmpty(plainText)) return "";

            string xored = XorCipher(plainText);
            byte[] bytes = Encoding.UTF8.GetBytes(xored);
            return Convert.ToBase64String(bytes);
        }

        public string Decrypt(string cipherText)
        {
            if (string.IsNullOrEmpty(cipherText)) return "";

            try
            {
                byte[] bytes = Convert.FromBase64String(cipherText);
                string xored = Encoding.UTF8.GetString(bytes);
                return XorCipher(xored);
            }
            catch
            {
                return "Hata: Veri çözülemedi";
            }
        }
    }
}