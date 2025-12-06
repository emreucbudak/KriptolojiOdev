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
        private static readonly byte[] AesSBox = new byte[256] {
            0x63, 0x7C, 0x77, 0x7B, 0xF2, 0x6B, 0x6F, 0xC5, 0x30, 0x01, 0x67, 0x2B, 0xFE, 0xD7, 0xAB, 0x76,
            0xCA, 0x82, 0xC9, 0x7D, 0xFA, 0x59, 0x47, 0xF0, 0xAD, 0xD4, 0xA2, 0xAF, 0x9C, 0xA4, 0x72, 0xC0,
            0xB7, 0xFD, 0x93, 0x26, 0x36, 0x3F, 0xF7, 0xCC, 0x34, 0xA5, 0xE5, 0xF1, 0x71, 0xD8, 0x31, 0x15,
            0x04, 0xC7, 0x23, 0xC3, 0x18, 0x96, 0x05, 0x9A, 0x07, 0x12, 0x80, 0xE2, 0xEB, 0x27, 0xB2, 0x75,
            0x09, 0x83, 0x2C, 0x1A, 0x1B, 0x6E, 0x5A, 0xA0, 0x52, 0x3B, 0xD6, 0xB3, 0x29, 0xE3, 0x2F, 0x84,
            0x53, 0xD1, 0x00, 0xED, 0x20, 0xFC, 0xB1, 0x5B, 0x6A, 0xCB, 0xBE, 0x39, 0x4A, 0x4C, 0x58, 0xCF,
            0xD0, 0xEF, 0xAA, 0xFB, 0x43, 0x4D, 0x33, 0x85, 0x45, 0xF9, 0x02, 0x7F, 0x50, 0x3C, 0x9F, 0xA8,
            0x51, 0xA3, 0x40, 0x8F, 0x92, 0x9D, 0x38, 0xF5, 0xBC, 0xB6, 0xDA, 0x21, 0x10, 0xFF, 0xF3, 0xD2,
            0xCD, 0x0C, 0x13, 0xEC, 0x5F, 0x97, 0x44, 0x17, 0xC4, 0xA7, 0x7E, 0x3D, 0x64, 0x5D, 0x19, 0x73,
            0x60, 0x81, 0x4F, 0xDC, 0x22, 0x2A, 0x90, 0x88, 0x46, 0xEE, 0xB8, 0x14, 0xDE, 0x5E, 0x0B, 0xDB,
            0xE0, 0x32, 0x3A, 0x0A, 0x49, 0x06, 0x24, 0x5C, 0xC2, 0xD3, 0xAC, 0x62, 0x91, 0x95, 0xE4, 0x79,
            0xE7, 0xC8, 0x37, 0x6D, 0x8D, 0xD5, 0x4E, 0xA9, 0x6C, 0x56, 0xF4, 0xEA, 0x65, 0x7A, 0xAE, 0x08,
            0xBA, 0x78, 0x25, 0x2E, 0x1C, 0xA6, 0xB4, 0xC6, 0xE8, 0xDD, 0x74, 0x1F, 0x4B, 0xBD, 0x8B, 0x8A,
            0x70, 0x3E, 0xB5, 0x66, 0x48, 0x03, 0xF6, 0x0E, 0x61, 0x35, 0x57, 0xB9, 0x86, 0xC1, 0x1D, 0x9E,
            0xE1, 0xF8, 0x98, 0x11, 0x69, 0xD9, 0x8E, 0x94, 0x9B, 0x1E, 0x87, 0xE9, 0xCE, 0x55, 0x28, 0xDF,
            0x8C, 0xA1, 0x89, 0x0D, 0xBF, 0xE6, 0x42, 0x68, 0x41, 0x99, 0x2D, 0x0F, 0xB0, 0x54, 0xBB, 0x16
        };
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
            if (string.IsNullOrEmpty(key) || key.Length != 26) throw new ArgumentException("Key 26 harf uzunluğunda vermelisin");
            if (!key.All(char.IsLetter)) throw new ArgumentException("Key yalnızca harflerden vermelisin");
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
                else res.Append(c);
            }
            return res.ToString();
        }

        public string DecryptorAffine(string metin, int a = 5, int b = 8)
        {
            if (GCD(a, 26) != 1) throw new ArgumentException("a ve 26 aralarında asal bir sayı vermelisin.");
            StringBuilder result = new StringBuilder();
            int aInverse = ModInverse(a, 26);
            foreach (char c in metin)
            {
                if (char.IsLetter(c))
                {
                    bool isUpper = char.IsUpper(c);
                    int y = char.ToUpper(c) - 'A';
                    int x = (aInverse * (y - b + 26)) % 26;
                    char dec = (char)(x + 'A');
                    result.Append(isUpper ? dec : char.ToLower(dec));
                }
                else result.Append(c);
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
                    char dec = (char)(x + 'A');
                    result.Append(isUpper ? dec : char.ToLower(dec));
                    keyIndex++;
                }
                else result.Append(c);
            }
            return result.ToString();
        }

        public string ColumnarDecrypt(string metin, string key)
        {
            int len = key.Length;
            int rows = (int)Math.Ceiling((double)metin.Length / len);
            char[,] grid = new char[rows, len];
            var keyOrder = key.Select((c, i) => new { Char = c, Index = i }).OrderBy(x => x.Char).ToArray();
            int k = 0;
            foreach (var colInfo in keyOrder)
            {
                int col = colInfo.Index;
                for (int r = 0; r < rows; r++)
                {
                    if (k < metin.Length) grid[r, col] = metin[k++];
                    else grid[r, col] = ' ';
                }
            }
            StringBuilder sb = new StringBuilder();
            for (int r = 0; r < rows; r++) for (int c = 0; c < len; c++) sb.Append(grid[r, c]);
            return sb.ToString().TrimEnd();
        }

        public string PolybiusDecrypt(string metin, string key)
        {
            string polybiusSquare = "ABCDEFGHIKLMNOPQRSTUVWXYZ";
            int size = 5; // EKLENEN KISIM
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < metin.Length; i += 2)
            {
                if (i + 1 >= metin.Length) break;
                char rowChar = metin[i]; char colChar = metin[i + 1];
                if (!char.IsDigit(rowChar) || !char.IsDigit(colChar)) continue;
                int row = (rowChar - '1'); int col = (colChar - '1');
                if (row < 0 || row >= size || col < 0 || col >= size) continue;
                int index = row * size + col;
                sb.Append(polybiusSquare[index]);
            }
            return sb.ToString();
        }

        public string PigpenDecrypt(string metin, string key)
        {
            Dictionary<char, char> pigpenDict = new Dictionary<char, char>() {
                {'!', 'A'}, {'@', 'B'}, {'#', 'C'}, {'$', 'D'}, {'%', 'E'}, {'^', 'F'}, {'&', 'G'}, {'*', 'H'}, {'(', 'I'}, {')', 'J'}, {'a', 'K'}, {'b', 'L'}, {'c', 'M'}, {'d', 'N'}, {'e', 'O'}, {'f', 'P'}, {'g', 'Q'}, {'h', 'R'}, {'i', 'S'}, {'j', 'T'}, {'1', 'U'}, {'2', 'V'}, {'3', 'W'}, {'4', 'X'}, {'5', 'Y'}, {'6', 'Z'}
            };
            StringBuilder sb = new StringBuilder();
            foreach (char c in metin)
            {
                if (pigpenDict.ContainsKey(c)) sb.Append(pigpenDict[c]);
                else sb.Append(c);
            }
            return sb.ToString();
        }

        public string HillDecrypt(string metin, string key)
        {
            metin = metin.ToUpper().Replace(" ", "");
            key = key.ToUpper().Replace(" ", "");
            if (key.Length != 4) throw new ArgumentException("Key uzunluğu 4 karakter olmalı (2x2 matris için).");
            int[,] k = new int[2, 2];
            for (int i = 0; i < 4; i++) k[i / 2, i % 2] = key[i] - 'A';
            int det = (k[0, 0] * k[1, 1] - k[0, 1] * k[1, 0]) % 26;
            if (det < 0) det += 26;
            int detInv = -1;
            for (int i = 0; i < 26; i++) { if ((det * i) % 26 == 1) { detInv = i; break; } }
            if (detInv == -1) throw new Exception("Matrisin tersi yok.");
            int[,] invKey = new int[2, 2];
            invKey[0, 0] = (k[1, 1] * detInv) % 26;
            invKey[0, 1] = ((-k[0, 1] + 26) * detInv) % 26;
            invKey[1, 0] = ((-k[1, 0] + 26) * detInv) % 26;
            invKey[1, 1] = (k[0, 0] * detInv) % 26;
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < metin.Length; i += 2)
            {
                int a = metin[i] - 'A'; int b = metin[i + 1] - 'A';
                int c1 = (invKey[0, 0] * a + invKey[0, 1] * b) % 26;
                int c2 = (invKey[1, 0] * a + invKey[1, 1] * b) % 26;
                sb.Append((char)(c1 + 'A')); sb.Append((char)(c2 + 'A'));
            }
            return sb.ToString();
        }

        public string RotaDecrypt(string metin, string key)
        {
            if (string.IsNullOrEmpty(key)) throw new ArgumentException("Key boş olamaz.");
            if (!int.TryParse(key, out int shift)) throw new ArgumentException("Key sayı olmalıdır.");
            StringBuilder sb = new StringBuilder();
            foreach (char c in metin)
            {
                if (char.IsLetter(c)) { char offset = char.IsUpper(c) ? 'A' : 'a'; char dec = (char)(((c - shift - offset + 26) % 26) + offset); sb.Append(dec); }
                else sb.Append(c);
            }
            return sb.ToString();
        }

        public string TrenRayiDecrypt(string metin, string key)
        {
            if (metin == null) return null;
            if (!int.TryParse(key, out int rails) || rails < 1) rails = 3;
            if (rails == 1) return metin;
            int length = metin.Length; char[] decrypted = new char[length];
            int[] rowIndex = new int[length]; int currentRow = 0; int direction = 1;
            for (int i = 0; i < length; i++) { rowIndex[i] = currentRow; currentRow += direction; if (currentRow == rails - 1) direction = -1; else if (currentRow == 0) direction = 1; }
            int[] railCounts = new int[rails]; for (int i = 0; i < length; i++) railCounts[rowIndex[i]]++;
            int[] railStartIndex = new int[rails]; railStartIndex[0] = 0; for (int r = 1; r < rails; r++) railStartIndex[r] = railStartIndex[r - 1] + railCounts[r - 1];
            int[] railCurrentIndex = new int[rails]; Array.Copy(railStartIndex, railCurrentIndex, rails);
            for (int i = 0; i < length; i++) { int r = rowIndex[i]; decrypted[i] = metin[railCurrentIndex[r]]; railCurrentIndex[r]++; }
            return new string(decrypted);
        }

        public string AesDecrypt(string metin, string key, string iv = null)
        {
            if (string.IsNullOrEmpty(key)) throw new ArgumentException("AES için key gereklidir.");
            if (string.IsNullOrEmpty(metin)) return "";
            try
            {
                byte[] fullCipher = Convert.FromBase64String(metin);
                byte[] ivBytes = new byte[16];
                Array.Copy(fullCipher, 0, ivBytes, 0, 16);
                byte[] customIV = AesPrepareIV(iv);
                if (customIV != null) ivBytes = customIV;
                byte[] cipherText = new byte[fullCipher.Length - 16];
                Array.Copy(fullCipher, 16, cipherText, 0, cipherText.Length);
                byte[] keyBytes;
                using (SHA256 sha256 = SHA256.Create()) keyBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(key));
                byte[][] roundKeys = AesKeyExpansion(keyBytes);
                List<byte> plainTextBytes = new List<byte>();
                byte[] prev = ivBytes;
                for (int i = 0; i < cipherText.Length; i += 16) { byte[] block = new byte[16]; Array.Copy(cipherText, i, block, 0, 16); byte[] decryptedBlock = AesInvCipher(block, roundKeys); for (int k = 0; k < 16; k++) decryptedBlock[k] ^= prev[k]; plainTextBytes.AddRange(decryptedBlock); prev = block; }
                byte[] decryptedArray = plainTextBytes.ToArray();
                int padLen = decryptedArray[decryptedArray.Length - 1];
                if (padLen < 1 || padLen > 16) throw new Exception("Padding hatası!");
                byte[] finalPlain = new byte[decryptedArray.Length - padLen];
                Array.Copy(decryptedArray, finalPlain, finalPlain.Length);
                return Encoding.UTF8.GetString(finalPlain);
            }
            catch (Exception ex) { return "Hata: " + ex.Message; }
        }

        public string DesDecrypt(string metin, string key, string iv = null)
        {
            if (string.IsNullOrEmpty(key)) throw new ArgumentException("DES anahtarı boş olamaz.");
            try
            {
                byte[] fullCipher = Convert.FromBase64String(metin);
                byte[] ivBytes = new byte[8];
                Array.Copy(fullCipher, 0, ivBytes, 0, 8);
                if (!string.IsNullOrEmpty(iv))
                {
                    byte[] temp = Encoding.UTF8.GetBytes(iv);
                    byte[] finalIV = new byte[8];
                    Array.Copy(temp, finalIV, Math.Min(temp.Length, 8));
                    ivBytes = finalIV;
                }
                byte[] cipherText = new byte[fullCipher.Length - 8];
                Array.Copy(fullCipher, 8, cipherText, 0, cipherText.Length);
                byte[] keyBytes;
                using (MD5 md5 = MD5.Create()) { byte[] hash = md5.ComputeHash(Encoding.UTF8.GetBytes(key)); keyBytes = new byte[8]; Array.Copy(hash, 0, keyBytes, 0, 8); }
                using (DES des = DES.Create())
                {
                    des.Key = keyBytes;
                    des.IV = ivBytes;
                    des.Mode = CipherMode.CBC;
                    des.Padding = PaddingMode.PKCS7;
                    using (ICryptoTransform decryptor = des.CreateDecryptor())
                    {
                        byte[] plainBytes = decryptor.TransformFinalBlock(cipherText, 0, cipherText.Length);
                        return Encoding.UTF8.GetString(plainBytes);
                    }
                }
            }
            catch (CryptographicException) { return "Hata: Şifre (Key) yanlış veya IV uyumsuz."; }
            catch (Exception ex) { return "Hata: " + ex.Message; }
        }

        private int GCD(int x, int y) { while (y != 0) { int temp = y; y = x % y; x = temp; } return x; }
        private int ModInverse(int a, int m) { for (int i = 1; i < m; i++) { if ((a * i) % m == 1) return i; } throw new ArgumentException("Mod tersi bulamadım."); }
        private static byte AesGMul2(byte a) => (byte)(((a & 0x80) != 0) ? ((a << 1) ^ 0x1B) : (a << 1));
        private static void AesInvSubBytes(byte[] state) { for (int i = 0; i < 16; i++) state[i] = AesInvSBox[state[i]]; }
        private static void AesInvShiftRows(byte[] state) { byte temp = state[13]; state[13] = state[9]; state[9] = state[5]; state[5] = state[1]; state[1] = temp; temp = state[2]; state[2] = state[10]; state[10] = temp; temp = state[6]; state[6] = state[14]; state[14] = temp; temp = state[3]; state[3] = state[7]; state[7] = state[11]; state[11] = state[15]; state[15] = temp; }
        private static void AesInvMixColumns(byte[] state) { for (int i = 0; i < 16; i += 4) { byte s0 = state[i], s1 = state[i + 1], s2 = state[i + 2], s3 = state[i + 3]; byte u = AesGMul2(AesGMul2((byte)(s0 ^ s2))); byte v = AesGMul2(AesGMul2((byte)(s1 ^ s3))); s0 ^= u; s1 ^= v; s2 ^= u; s3 ^= v; state[i] = (byte)(AesGMul2(s0) ^ (AesGMul2(s1) ^ s1) ^ s2 ^ s3); state[i + 1] = (byte)(s0 ^ AesGMul2(s1) ^ (AesGMul2(s2) ^ s2) ^ s3); state[i + 2] = (byte)(s0 ^ s1 ^ AesGMul2(s2) ^ (AesGMul2(s3) ^ s3)); state[i + 3] = (byte)((AesGMul2(s0) ^ s0) ^ s1 ^ s2 ^ AesGMul2(s3)); } }
        private static void AesAddRoundKey(byte[] state, byte[] roundKey) { for (int i = 0; i < 16; i++) state[i] ^= roundKey[i]; }
        private static byte[][] AesKeyExpansion(byte[] key) { int Nk = key.Length / 4; int Nr = Nk + 6; int Nb = 4; byte[] expanded = new byte[Nb * (Nr + 1) * 4]; Array.Copy(key, expanded, key.Length); int bytesGen = key.Length; int rconIter = 1; byte[] temp = new byte[4]; while (bytesGen < expanded.Length) { for (int i = 0; i < 4; i++) temp[i] = expanded[bytesGen - 4 + i]; if (bytesGen % key.Length == 0) { byte t = temp[0]; temp[0] = temp[1]; temp[1] = temp[2]; temp[2] = temp[3]; temp[3] = t; for (int i = 0; i < 4; i++) temp[i] = AesSBox[temp[i]]; temp[0] ^= AesRcon[rconIter++]; } else if (Nk > 6 && bytesGen % key.Length == 16) { for (int i = 0; i < 4; i++) temp[i] = AesSBox[temp[i]]; } for (int i = 0; i < 4; i++) { expanded[bytesGen] = (byte)(expanded[bytesGen - key.Length] ^ temp[i]); bytesGen++; } } byte[][] roundKeys = new byte[Nr + 1][]; for (int i = 0; i <= Nr; i++) { roundKeys[i] = new byte[16]; Array.Copy(expanded, i * 16, roundKeys[i], 0, 16); } return roundKeys; }
        private static byte[] AesInvCipher(byte[] block, byte[][] roundKeys) { byte[] state = (byte[])block.Clone(); int Nr = roundKeys.Length - 1; AesAddRoundKey(state, roundKeys[Nr]); for (int r = Nr - 1; r > 0; r--) { AesInvShiftRows(state); AesInvSubBytes(state); AesAddRoundKey(state, roundKeys[r]); AesInvMixColumns(state); } AesInvShiftRows(state); AesInvSubBytes(state); AesAddRoundKey(state, roundKeys[0]); return state; }
        private static byte[] AesPrepareIV(string ivString) { if (string.IsNullOrEmpty(ivString)) return null; byte[] ivBytes = Encoding.UTF8.GetBytes(ivString); byte[] finalIV = new byte[16]; Array.Copy(ivBytes, finalIV, Math.Min(ivBytes.Length, 16)); return finalIV; }
    }
}