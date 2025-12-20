using KriptolojiOdev.Baglanti.Interface;
using KriptolojiOdev.Sifreleme.Interface;
using System;
using System.Text;

namespace KriptolojiOdev.Sifreleme.Class
{
    public class TransportSecurityService : ITransportSecurityService
    {
        private readonly byte[] TunnelKey = Encoding.UTF8.GetBytes("X7kP9mL2nQ5wE1rT4yU8iO");

        private byte[] XorProcess(byte[] data)
        {
            byte[] result = new byte[data.Length];
            for (int i = 0; i < data.Length; i++)
            {
                result[i] = (byte)(data[i] ^ TunnelKey[i % TunnelKey.Length]);
            }
            return result;
        }

        public string Encrypt(string plainText)
        {
            if (string.IsNullOrEmpty(plainText)) return "";
            byte[] data = Encoding.UTF8.GetBytes(plainText);
            byte[] xored = XorProcess(data);
            return Convert.ToBase64String(xored);
        }

        public string Decrypt(string cipherText)
        {
            if (string.IsNullOrEmpty(cipherText)) return "";
            try
            {
                byte[] data = Convert.FromBase64String(cipherText);
                byte[] xored = XorProcess(data);
                return Encoding.UTF8.GetString(xored);
            }
            catch
            {
                return cipherText;
            }
        }
    }
}