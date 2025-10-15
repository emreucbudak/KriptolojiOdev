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

        public string ColumnarDecrypt(string metin, string key)
        {
            int len = key.Length;
            int rows = (int)Math.Ceiling((double)metin.Length / len);
            char[,] grid = new char[rows, len];

    
            var keyOrder = key
                .Select((c, i) => new { Char = c, Index = i })
                .OrderBy(x => x.Char)
                .ToArray();

            int k = 0;

      
            foreach (var colInfo in keyOrder)
            {
                int col = colInfo.Index;
                for (int r = 0; r < rows; r++)
                {
                    if (k < metin.Length)
                        grid[r, col] = metin[k++];
                    else
                        grid[r, col] = ' '; 
                }
            }


            StringBuilder sb = new StringBuilder();
            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < len; c++)
                {
                    sb.Append(grid[r, c]);
                }
            }


            return sb.ToString().TrimEnd();
        }

        public string PolybiusDecrypt(string metin, string key)
        {
     
            string polybiusSquare = "ABCDEFGHIKLMNOPQRSTUVWXYZ"; 
            int size = 5; // 5x5 kare

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < metin.Length; i += 2)
            {
                if (i + 1 >= metin.Length) break;

                char rowChar = metin[i];
                char colChar = metin[i + 1];

                if (!char.IsDigit(rowChar) || !char.IsDigit(colChar))
                    continue; 

                int row = (rowChar - '1');
                int col = (colChar - '1');

                if (row < 0 || row >= size || col < 0 || col >= size)
                    continue;

                int index = row * size + col;
                sb.Append(polybiusSquare[index]);
            }

            return sb.ToString();
        }

        public string PigpenDecrypt(string metin, string key)
        {
            
            Dictionary<char, char> pigpenDict = new Dictionary<char, char>()
    {
  
        {'A', 'A'}, {'B', 'B'}, {'C', 'C'}, {'D', 'D'}, {'E', 'E'},
        {'F', 'F'}, {'G', 'G'}, {'H', 'H'}, {'I', 'I'}, {'J', 'J'},
        {'K', 'K'}, {'L', 'L'}, {'M', 'M'}, {'N', 'N'}, {'O', 'O'},
        {'P', 'P'}, {'Q', 'Q'}, {'R', 'R'}, {'S', 'S'}, {'T', 'T'},
        {'U', 'U'}, {'V', 'V'}, {'W', 'W'}, {'X', 'X'}, {'Y', 'Y'}, {'Z', 'Z'}
    };

            StringBuilder sb = new StringBuilder();

            foreach (char c in metin)
            {
                if (pigpenDict.ContainsKey(c))
                {
                    sb.Append(pigpenDict[c]);
                }
                else
                {
                    sb.Append(c); 
                }
            }

            return sb.ToString();
        }

        public string HillDecrypt(string metin, string key)
        {
         
            metin = metin.ToUpper().Replace(" ", "");
            key = key.ToUpper().Replace(" ", "");

            
            if (key.Length != 4)
                throw new ArgumentException("Key uzunluğu 4 karakter olmalı (2x2 matris için).");

            int[,] k = new int[2, 2];
            for (int i = 0; i < 4; i++)
                k[i / 2, i % 2] = key[i] - 'A';

            
            int det = (k[0, 0] * k[1, 1] - k[0, 1] * k[1, 0]) % 26;
            if (det < 0) det += 26;

        
            int detInv = -1;
            for (int i = 0; i < 26; i++)
            {
                if ((det * i) % 26 == 1)
                {
                    detInv = i;
                    break;
                }
            }
            if (detInv == -1) throw new Exception("Matrisin tersi yok (determinant invertible değil).");

 
            int[,] invKey = new int[2, 2];
            invKey[0, 0] = (k[1, 1] * detInv) % 26;
            invKey[0, 1] = ((-k[0, 1] + 26) * detInv) % 26;
            invKey[1, 0] = ((-k[1, 0] + 26) * detInv) % 26;
            invKey[1, 1] = (k[0, 0] * detInv) % 26;

            StringBuilder sb = new StringBuilder();

       
            for (int i = 0; i < metin.Length; i += 2)
            {
                int a = metin[i] - 'A';
                int b = metin[i + 1] - 'A';

                int c1 = (invKey[0, 0] * a + invKey[0, 1] * b) % 26;
                int c2 = (invKey[1, 0] * a + invKey[1, 1] * b) % 26;

                sb.Append((char)(c1 + 'A'));
                sb.Append((char)(c2 + 'A'));
            }

            return sb.ToString();
        }
        public string RotaDecrypt(string metin, string key)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentException("Key boş olamaz.");

      
            if (!int.TryParse(key, out int shift))
                throw new ArgumentException("Key sayı olmalıdır.");

            StringBuilder sb = new StringBuilder();

            foreach (char c in metin)
            {
                if (char.IsLetter(c))
                {
                    char offset = char.IsUpper(c) ? 'A' : 'a';

                    char decryptedChar = (char)(((c - shift - offset + 26) % 26) + offset);
                    sb.Append(decryptedChar);
                }
                else
                {
                    sb.Append(c); 
                }
            }

            return sb.ToString();
        }
    }
}
