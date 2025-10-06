using KriptolojiOdev.Sifreleme.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KriptolojiOdev.Sifreleme.Class
{
    public class EncryptorService : IEncryptorService
    {
        private readonly string substitutionKey = "QWERTYUIOPASDFGHJKLZXCVBNM";
        public string CaesarEncrypt(string metin)
        {
            StringBuilder result = new StringBuilder();
            foreach (char c in metin)
            {
                if (char.IsLetter(c))
                {
                    char baseChar = char.IsUpper(c) ? 'A' : 'a';
                    result.Append((char)(((c - baseChar + 3) % 26) + baseChar));
                }
                else
                {
                    result.Append(c);
                }
            }
            return result.ToString();
        }

        public string SubstitutionEncrypt(string metin)
        {
            StringBuilder result = new StringBuilder();
            string upperKey = substitutionKey.ToUpper();

            foreach (char c in metin)
            {
                if (char.IsLetter(c))
                {
                    bool isUpper = char.IsUpper(c);
                    int index = char.ToUpper(c) - 'A';
                    char encryptedChar = upperKey[index];
                    result.Append(isUpper ? encryptedChar : char.ToLower(encryptedChar));
                }
                else
                {
                    result.Append(c);
                }
            }

            return result.ToString();
        }
    }
}

