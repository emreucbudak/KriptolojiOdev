using KriptolojiOdev.Sifreleme.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace KriptolojiOdev.Sifreleme.Class
{
    public class DecryptorService : IDecryptorService
    {
        private static readonly byte[] AesSBox = new byte[256] { 0x63, 0x7C, 0x77, 0x7B, 0xF2, 0x6B, 0x6F, 0xC5, 0x30, 0x01, 0x67, 0x2B, 0xFE, 0xD7, 0xAB, 0x76, 0xCA, 0x82, 0xC9, 0x7D, 0xFA, 0x59, 0x47, 0xF0, 0xAD, 0xD4, 0xA2, 0xAF, 0x9C, 0xA4, 0x72, 0xC0, 0xB7, 0xFD, 0x93, 0x26, 0x36, 0x3F, 0xF7, 0xCC, 0x34, 0xA5, 0xE5, 0xF1, 0x71, 0xD8, 0x31, 0x15, 0x04, 0xC7, 0x23, 0xC3, 0x18, 0x96, 0x05, 0x9A, 0x07, 0x12, 0x80, 0xE2, 0xEB, 0x27, 0xB2, 0x75, 0x09, 0x83, 0x2C, 0x1A, 0x1B, 0x6E, 0x5A, 0xA0, 0x52, 0x3B, 0xD6, 0xB3, 0x29, 0xE3, 0x2F, 0x84, 0x53, 0xD1, 0x00, 0xED, 0x20, 0xFC, 0xB1, 0x5B, 0x6A, 0xCB, 0xBE, 0x39, 0x4A, 0x4C, 0x58, 0xCF, 0xD0, 0xEF, 0xAA, 0xFB, 0x43, 0x4D, 0x33, 0x85, 0x45, 0xF9, 0x02, 0x7F, 0x50, 0x3C, 0x9F, 0xA8, 0x51, 0xA3, 0x40, 0x8F, 0x92, 0x9D, 0x38, 0xF5, 0xBC, 0xB6, 0xDA, 0x21, 0x10, 0xFF, 0xF3, 0xD2, 0xCD, 0x0C, 0x13, 0xEC, 0x5F, 0x97, 0x44, 0x17, 0xC4, 0xA7, 0x7E, 0x3D, 0x64, 0x5D, 0x19, 0x73, 0x60, 0x81, 0x4F, 0xDC, 0x22, 0x2A, 0x90, 0x88, 0x46, 0xEE, 0xB8, 0x14, 0xDE, 0x5E, 0x0B, 0xDB, 0xE0, 0x32, 0x3A, 0x0A, 0x49, 0x06, 0x24, 0x5C, 0xC2, 0xD3, 0xAC, 0x62, 0x91, 0x95, 0xE4, 0x79, 0xE7, 0xC8, 0x37, 0x6D, 0x8D, 0xD5, 0x4E, 0xA9, 0x6C, 0x56, 0xF4, 0xEA, 0x65, 0x7A, 0xAE, 0x08, 0xBA, 0x78, 0x25, 0x2E, 0x1C, 0xA6, 0xB4, 0xC6, 0xE8, 0xDD, 0x74, 0x1F, 0x4B, 0xBD, 0x8B, 0x8A, 0x70, 0x3E, 0xB5, 0x66, 0x48, 0x03, 0xF6, 0x0E, 0x61, 0x35, 0x57, 0xB9, 0x86, 0xC1, 0x1D, 0x9E, 0xE1, 0xF8, 0x98, 0x11, 0x69, 0xD9, 0x8E, 0x94, 0x9B, 0x1E, 0x87, 0xE9, 0xCE, 0x55, 0x28, 0xDF, 0x8C, 0xA1, 0x89, 0x0D, 0xBF, 0xE6, 0x42, 0x68, 0x41, 0x99, 0x2D, 0x0F, 0xB0, 0x54, 0xBB, 0x16 };
        private static readonly byte[] AesInvSBox = new byte[256];
        private static readonly byte[] AesRcon = new byte[] { 0x00, 0x01, 0x02, 0x04, 0x08, 0x10, 0x20, 0x40, 0x80, 0x1B, 0x36 };

        static DecryptorService()
        {
            for (int i = 0; i < 256; i++) AesInvSBox[AesSBox[i]] = (byte)i;
        }

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
                else result.Append(c);
            }
            return result.ToString();
        }

        public string DecryptorSubstitiuion(string metin, string key)
        {
            if (string.IsNullOrEmpty(key) || key.Length != 26) return metin;
            StringBuilder res = new StringBuilder();
            string alfabe = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string upperKey = key.ToUpper();
            foreach (char c in metin.ToUpper())
            {
                if (c >= 'A' && c <= 'Z')
                {
                    int index = upperKey.IndexOf(c);
                    if (index != -1) res.Append(alfabe[index]);
                    else res.Append(c);
                }
                else res.Append(c);
            }
            return res.ToString();
        }

        public string DecryptorAffine(string metin, int a = 5, int b = 8)
        {
            StringBuilder result = new StringBuilder();
            int aInverse = ModInverse(a, 26);
            foreach (char c in metin)
            {
                if (char.IsLetter(c))
                {
                    char offset = char.IsUpper(c) ? 'A' : 'a';
                    int y = char.ToUpper(c) - 'A';
                    int x = (aInverse * (y - b + 26)) % 26;
                    char dec = (char)(x + 'A');
                    result.Append(char.IsUpper(c) ? dec : char.ToLower(dec));
                }
                else result.Append(c);
            }
            return result.ToString();
        }

        public string DecryptorVigenere(string metin, string key)
        {
            if (string.IsNullOrEmpty(key)) return metin;
            StringBuilder result = new StringBuilder();
            string upperKey = key.ToUpper();
            int index = 0;
            foreach (char c in metin)
            {
                if (char.IsLetter(c))
                {
                    char offset = char.IsUpper(c) ? 'A' : 'a';
                    int y = char.ToUpper(c) - 'A';
                    int k = upperKey[index % upperKey.Length] - 'A';
                    int x = (y - k + 26) % 26;
                    result.Append((char)(x + offset));
                    index++;
                }
                else result.Append(c);
            }
            return result.ToString();
        }

        public string ColumnarDecrypt(string metin, string key)
        {
            if (string.IsNullOrEmpty(key)) return metin;
            int cols = key.Length;
            int rows = (int)Math.Ceiling((double)metin.Length / cols);
            char[,] grid = new char[rows, cols];
            var order = key.Select((ch, i) => new { ch, i }).OrderBy(x => x.ch).ToList();
            int k = 0;
            foreach (var o in order)
                for (int r = 0; r < rows; r++)
                    if (k < metin.Length) grid[r, o.i] = metin[k++];
            StringBuilder sb = new StringBuilder();
            for (int r = 0; r < rows; r++)
                for (int c = 0; c < cols; c++)
                    if (grid[r, c] != '\0') sb.Append(grid[r, c]);
            return sb.ToString();
        }

        public string PolybiusDecrypt(string metin, string key)
        {
            string alpha = "ABCDEFGHIKLMNOPQRSTUVWXYZ";
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < metin.Length; i += 2)
            {
                if (i + 1 >= metin.Length) break;
                int row = (metin[i] - '1');
                int col = (metin[i + 1] - '1');
                if (row >= 0 && row < 5 && col >= 0 && col < 5) sb.Append(alpha[row * 5 + col]);
            }
            return sb.ToString();
        }

        public string PigpenDecrypt(string metin, string key)
        {
            Dictionary<char, char> dict = new Dictionary<char, char>() { { '!', 'A' }, { '@', 'B' }, { '#', 'C' }, { '$', 'D' }, { '%', 'E' }, { '^', 'F' }, { '&', 'G' }, { '*', 'H' }, { '(', 'I' }, { ')', 'J' }, { 'a', 'K' }, { 'b', 'L' }, { 'c', 'M' }, { 'd', 'N' }, { 'e', 'O' }, { 'f', 'P' }, { 'g', 'Q' }, { 'h', 'R' }, { 'i', 'S' }, { 'j', 'T' }, { '1', 'U' }, { '2', 'V' }, { '3', 'W' }, { '4', 'X' }, { '5', 'Y' }, { '6', 'Z' } };
            StringBuilder sb = new StringBuilder();
            foreach (char c in metin) sb.Append(dict.ContainsKey(c) ? dict[c] : c);
            return sb.ToString();
        }

        public string HillDecrypt(string metin, string key)
        {
            if (string.IsNullOrEmpty(key) || key.Length != 4) return metin;
            int[,] k = { { key[0] - 'A', key[1] - 'A' }, { key[2] - 'A', key[3] - 'A' } };
            int det = (k[0, 0] * k[1, 1] - k[0, 1] * k[1, 0]) % 26;
            if (det < 0) det += 26;
            int detInv = ModInverse(det, 26);
            int[,] invK = { { (k[1, 1] * detInv) % 26, ((-k[0, 1] + 26) * detInv) % 26 }, { ((-k[1, 0] + 26) * detInv) % 26, (k[0, 0] * detInv) % 26 } };
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < metin.Length; i += 2)
            {
                int a = metin[i] - 'A', b = metin[i + 1] - 'A';
                sb.Append((char)((invK[0, 0] * a + invK[0, 1] * b) % 26 + 'A'));
                sb.Append((char)((invK[1, 0] * a + invK[1, 1] * b) % 26 + 'A'));
            }
            return sb.ToString();
        }

        public string RotaDecrypt(string metin, string key)
        {
            int.TryParse(key, out int s);
            StringBuilder sb = new StringBuilder();
            foreach (char c in metin)
            {
                if (char.IsLetter(c))
                {
                    char b = char.IsUpper(c) ? 'A' : 'a';
                    sb.Append((char)((c - b - s + 26) % 26 + b));
                }
                else sb.Append(c);
            }
            return sb.ToString();
        }

        public string TrenRayiDecrypt(string metin, string key)
        {
            if (!int.TryParse(key, out int r) || r < 2) return metin;
            char[] dec = new char[metin.Length];
            int[] rowIdx = new int[metin.Length];
            int curr = 0, dir = 1;
            for (int i = 0; i < metin.Length; i++) { rowIdx[i] = curr; curr += dir; if (curr == r - 1 || curr == 0) dir *= -1; }
            int k = 0;
            for (int i = 0; i < r; i++)
                for (int j = 0; j < metin.Length; j++)
                    if (rowIdx[j] == i) dec[j] = metin[k++];
            return new string(dec);
        }

        public string AesDecrypt(string metin, string key, string iv = null)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(key));
                aes.IV = new byte[16];
                if (!string.IsNullOrEmpty(iv)) Array.Copy(Encoding.UTF8.GetBytes(iv), aes.IV, Math.Min(iv.Length, 16));
                using (var dec = aes.CreateDecryptor())
                {
                    byte[] data = Convert.FromBase64String(metin);
                    return Encoding.UTF8.GetString(dec.TransformFinalBlock(data, 0, data.Length));
                }
            }
        }

        public string DesDecrypt(string metin, string key, string iv = null)
        {
            using (DES des = DES.Create())
            {
                des.Key = MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(key)).Take(8).ToArray();
                des.IV = new byte[8];
                if (!string.IsNullOrEmpty(iv)) Array.Copy(Encoding.UTF8.GetBytes(iv), des.IV, Math.Min(iv.Length, 8));
                using (var dec = des.CreateDecryptor())
                {
                    byte[] data = Convert.FromBase64String(metin);
                    return Encoding.UTF8.GetString(dec.TransformFinalBlock(data, 0, data.Length));
                }
            }
        }

        public string RsaDecrypt(string metin, string privateKeyXml)
        {
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                rsa.FromXmlString(privateKeyXml);
                byte[] data = Convert.FromBase64String(metin);
                return Encoding.UTF8.GetString(rsa.Decrypt(data, false));
            }
        }

        public string ManuelDesDecrypt(string metin, string key, string iv = null) => DesDecrypt(metin, key, iv);

        public string DecryptByAlgorithm(string algorithm, string text, string key, string iv)
        {
            return algorithm.ToUpperInvariant() switch
            {
                "CAESAR" => DecryptorCaesar(text),
                "VIGENERE" => DecryptorVigenere(text, key),
                "SUBSTITUTION" => DecryptorSubstitiuion(text, key),
                "AFFINE" => DecryptorAffine(text),
                "ROTA" => RotaDecrypt(text, key),
                "COLUMNAR" => ColumnarDecrypt(text, key),
                "POLYBIUS" => PolybiusDecrypt(text, key),
                "PIGPEN" => PigpenDecrypt(text, key),
                "HILL" => HillDecrypt(text, key),
                "TRENRAYI" => TrenRayiDecrypt(text, key),
                "AES" => AesDecrypt(text, key, iv),
                "DES" => DesDecrypt(text, key, iv),
                "MANUEL_DES" => ManuelDesDecrypt(text, key, iv),
                "RSA" => RsaDecrypt(text, key),
                _ => text
            };
        }

        private int GCD(int a, int b) { while (b != 0) { int t = b; b = a % b; a = t; } return a; }
        private int ModInverse(int a, int m) { for (int i = 1; i < m; i++) if ((a * i) % m == 1) return i; return 1; }
    }
}