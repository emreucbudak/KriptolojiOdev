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
            if (string.IsNullOrEmpty(key) || key.Length != 26)
                throw new ArgumentException("Key 26 harf uzunluğunda olmalı.");

            if (!key.All(char.IsLetter))
                throw new ArgumentException("Key yalnızca harflerden oluşmalı.");

            StringBuilder result = new StringBuilder();
            string upperKey = key.ToUpper();
            metin = metin.ToUpper(); 

            foreach (char c in metin)
            {
                if (c >= 'A' && c <= 'Z') 
                {
                    int index = c - 'A'; 
                    result.Append(upperKey[index]); 
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
        public string ColumnarEncrypt(string metin, string key)
        {
            int colCount = key.Length;
            int rowCount = (int)Math.Ceiling((double)metin.Length / colCount);

            char[,] grid = new char[rowCount, colCount];

        
            int index = 0;
            for (int r = 0; r < rowCount; r++)
            {
                for (int c = 0; c < colCount; c++)
                {
                    if (index < metin.Length)
                        grid[r, c] = metin[index++];
                    else
                        grid[r, c] = 'X'; 
                }
            }

  
            var keyOrder = key
                .Select((ch, idx) => new { Ch = ch, Index = idx })
                .OrderBy(x => x.Ch)
                .ToList();

            var cipherText = new StringBuilder();
            foreach (var k in keyOrder)
            {
                for (int r = 0; r < rowCount; r++)
                {
                    cipherText.Append(grid[r, k.Index]);
                }
            }

            return cipherText.ToString();
        }

        public string PolybiusEncrypt(string metin, string key)
        {

            string alphabet = "ABCDEFGHIKLMNOPQRSTUVWXYZ"; 
            string square = "";

       
            foreach (char c in key.ToUpper())
            {
                if (!square.Contains(c) && alphabet.Contains(c))
                    square += c;
            }
            foreach (char c in alphabet)
            {
                if (!square.Contains(c))
                    square += c;
            }

            StringBuilder cipherText = new StringBuilder();
            metin = metin.ToUpper().Replace("J", "I"); 

            foreach (char c in metin)
            {
                if (!alphabet.Contains(c))
                    continue; 

                int index = square.IndexOf(c);
                int row = index / 5 + 1; 
                int col = index % 5 + 1; 
                cipherText.Append(row).Append(col);
            }

            return cipherText.ToString(); 
        }

    }
}

