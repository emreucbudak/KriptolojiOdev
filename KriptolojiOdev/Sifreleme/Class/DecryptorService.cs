using KriptolojiOdev.Sifreleme.Interface;
using System;
using System.Linq;
using System.Text;

namespace KriptolojiOdev.Sifreleme.Class
{
    public class DecryptorService : IDecryptorService
    {
        public string DecryptorCaesar(string metin)
        {
            StringBuilder result = new StringBuilder();

            foreach (char c in metin)
            {
                if (char.IsLetter(c))
                {
                    char harf = char.IsUpper(c) ? 'A' : 'a';
                
                    result.Append((char)(((c - harf - 3 + 26) % 26) + harf));
                }
                else
                {
                    result.Append(c);
                }
            }

            return result.ToString();
        }

        public string DecryptorSubstitiuion(string metin, string key)
        {
            if (string.IsNullOrEmpty(key) || key.Length != 26)
                throw new ArgumentException("Key 26 harf uzunluğunda vermelisin");

            if (!key.All(char.IsLetter))
                throw new ArgumentException("Key yalnızca harflerden vermelisin");

            StringBuilder res = new StringBuilder();
            string alfabe = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string upperKey = key.ToUpper();
            metin = metin.ToUpper();

            foreach (char c in metin)
            {
                if (c >= 'A' && c <= 'Z')
                {
                    int index = upperKey.IndexOf(c);
                    res.Append(alfabe[index]);
                }
                else
                {
                    res.Append(c);
                }
            }

            return res.ToString();
        }

        public string DecryptorAffine(string metin, int a = 5, int b = 8)
        {
            if (GCD(a, 26) != 1)
                throw new ArgumentException("a ve 26 aralarında asal bir sayı vermelisin.");

            StringBuilder result = new StringBuilder();
            int aInverse = ModInverse(a, 26); 

            foreach (char c in metin)
            {
                if (char.IsLetter(c))
                {
                    bool isUpper = char.IsUpper(c);
                    int y = char.ToUpper(c) - 'A';
                    int x = (aInverse * (y - b + 26)) % 26;
                    char decryptedChar = (char)(x + 'A');
                    result.Append(isUpper ? decryptedChar : char.ToLower(decryptedChar));
                }
                else
                {
                    result.Append(c);
                }
            }

            return result.ToString();
        }

        public string DecryptorVigenere(string metin, string key)
        {


            StringBuilder result = new StringBuilder();
            string upperKey = key.ToUpper();
            int keyIndex = 0;

            foreach (char c in metin)
            {
                if (char.IsLetter(c))
                {
                    bool isUpper = char.IsUpper(c);
                    int y = char.ToUpper(c) - 'A';
                    int k = upperKey[keyIndex % upperKey.Length] - 'A';
                    int x = (y - k + 26) % 26;
                    char decryptedChar = (char)(x + 'A');
                    result.Append(isUpper ? decryptedChar : char.ToLower(decryptedChar));
                    keyIndex++;
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

        private int ModInverse(int a, int m)
        {
            for (int i = 1; i < m; i++)
            {
                if ((a * i) % m == 1)
                    return i;
            }
            throw new ArgumentException("Mod tersi bulamadım.");
        }
    }
}
