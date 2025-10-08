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

        public string SubstitutionEncrypt(string metin, string key)
        {
            StringBuilder result = new StringBuilder();
            string upperKey = key.ToUpper();

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
        public string AffineEncrypt(string metin, int a = 5, int b = 8)
        {
            if (GCD(a, 26) != 1)
                throw new ArgumentException("a ve 26 aralarında asal olmalı!");

            StringBuilder result = new StringBuilder();

            foreach (char c in metin)
            {
                if (char.IsLetter(c))
                {
                    bool isUpper = char.IsUpper(c);
                    int x = char.ToUpper(c) - 'A';
                    int y = (a * x + b) % 26;
                    char encryptedChar = (char)(y + 'A');
                    result.Append(isUpper ? encryptedChar : char.ToLower(encryptedChar));
                }
                else
                {
                    result.Append(c);
                }
            }

            return result.ToString();
        }


        private int GCD(int x, int y)
        {
            while (y != 0)
            {
                int temp = y;
                y = x % y;
                x = temp;
            }
            return x;
        }
        public string VigenereEncrypt(string metin,string vigenereKey)
        {
            StringBuilder result = new StringBuilder();
            string key = vigenereKey.ToUpper();
            int keyIndex = 0;

            foreach (char c in metin)
            {
                if (char.IsLetter(c))
                {
                    bool isUpper = char.IsUpper(c);
                    int x = char.ToUpper(c) - 'A';
                    int k = key[keyIndex % key.Length] - 'A';
                    int y = (x + k) % 26;
                    char encryptedChar = (char)(y + 'A');
                    result.Append(isUpper ? encryptedChar : char.ToLower(encryptedChar));
                    keyIndex++;
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

