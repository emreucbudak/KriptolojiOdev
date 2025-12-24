using KriptolojiOdev.Sifreleme.Interface;
using Org.BouncyCastle.Asn1.X9;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Pkcs;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.X509;
using System.Security.Cryptography;
using System.Text;
namespace KriptolojiOdev.Sifreleme.Class
{
    public class EncryptorService : IEncryptorService
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

        private static readonly byte[] AesRcon = new byte[] {
            0x00, 0x01, 0x02, 0x04, 0x08, 0x10, 0x20, 0x40, 0x80, 0x1B, 0x36
        };

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
                else result.Append(c);
            }
            return result.ToString();
        }

        public string SubstitutionEncrypt(string metin, string key)
        {
            if (string.IsNullOrEmpty(key) || key.Length != 26) throw new ArgumentException("Key 26 harf uzunluğunda olmalı.");
            if (!key.All(char.IsLetter)) throw new ArgumentException("Key yalnızca harflerden oluşmalı.");

            StringBuilder result = new StringBuilder();
            string upperKey = key.ToUpper();
            metin = metin.ToUpper();

            foreach (char c in metin)
            {
                if (c >= 'A' && c <= 'Z') result.Append(upperKey[c - 'A']);
                else result.Append(c);
            }
            return result.ToString();
        }

        public string AffineEncrypt(string metin, int a = 5, int b = 8)
        {
            if (GCD(a, 26) != 1) throw new ArgumentException("a ve 26 aralarında asal olmalı!");

            StringBuilder result = new StringBuilder();
            foreach (char c in metin)
            {
                if (char.IsLetter(c))
                {
                    bool isUpper = char.IsUpper(c);
                    int x = char.ToUpper(c) - 'A';
                    int y = (a * x + b) % 26;
                    char enc = (char)(y + 'A');
                    result.Append(isUpper ? enc : char.ToLower(enc));
                }
                else result.Append(c);
            }
            return result.ToString();
        }

        public string VigenereEncrypt(string metin, string vigenereKey)
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
                    char enc = (char)(y + 'A');
                    result.Append(isUpper ? enc : char.ToLower(enc));
                    keyIndex++;
                }
                else result.Append(c);
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
                    if (index < metin.Length) grid[r, c] = metin[index++];
                    else grid[r, c] = 'X';
                }
            }

            var keyOrder = key.Select((ch, idx) => new { Ch = ch, Index = idx }).OrderBy(x => x.Ch).ToList();
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
            string square = BuildPolybiusSquare(key, alphabet); // Yardımcı metod kullanıyoruz
            StringBuilder cipherText = new StringBuilder();
            metin = metin.ToUpper().Replace("J", "I");

            foreach (char c in metin)
            {
                int index = square.IndexOf(c);
                if (index == -1) continue;
                cipherText.Append(index / 5 + 1).Append(index % 5 + 1);
            }
            return cipherText.ToString();
        }


        private string BuildPolybiusSquare(string key, string alphabet)
        {
            string square = "";
            foreach (char c in (key ?? "").ToUpper())
                if (!square.Contains(c) && alphabet.Contains(c)) square += c;
            foreach (char c in alphabet)
                if (!square.Contains(c)) square += c;
            return square;
        }

        public string PigpenEncrypt(string metin, string key)
        {
            string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string symbols = "!@#$%^&*()abcdefghij123456"; // 26 karakter
            int shift = string.IsNullOrEmpty(key) ? 0 : key.Length;
            metin = metin.ToUpper();
            StringBuilder cipherText = new StringBuilder();

            foreach (char c in metin)
            {
                int idx = letters.IndexOf(c);
                if (idx != -1)
                {
                    int encryptedIdx = (idx + shift) % 26;
                    cipherText.Append(symbols[encryptedIdx]);
                }
                else cipherText.Append(c);
            }
            return cipherText.ToString();
        }

        public string HillEncrypt(string metin, string key)
        {
            metin = metin.ToUpper().Replace(" ", "");
            key = key.ToUpper().Replace(" ", "");
            if (key.Length != 4) throw new ArgumentException("Key uzunluğu 4 karakter olmalı");

            int[,] keyMatrix = new int[2, 2];
            for (int i = 0; i < 4; i++) keyMatrix[i / 2, i % 2] = key[i] - 'A';

            if (metin.Length % 2 != 0) metin += "X";

            StringBuilder cipherText = new StringBuilder();
            for (int i = 0; i < metin.Length; i += 2)
            {
                int a = metin[i] - 'A';
                int b = metin[i + 1] - 'A';
                int c1 = (keyMatrix[0, 0] * a + keyMatrix[0, 1] * b) % 26;
                int c2 = (keyMatrix[1, 0] * a + keyMatrix[1, 1] * b) % 26;
                cipherText.Append((char)(c1 + 'A'));
                cipherText.Append((char)(c2 + 'A'));
            }
            return cipherText.ToString();
        }

        public string RotaEncrypt(string metin, string key)
        {
            if (string.IsNullOrEmpty(key)) throw new ArgumentException("Key boş olamaz.");
            if (!int.TryParse(key, out int shift)) throw new ArgumentException("Key sayı olmalıdır.");

            StringBuilder sb = new StringBuilder();
            foreach (char c in metin)
            {
                if (char.IsLetter(c))
                {
                    char offset = char.IsUpper(c) ? 'A' : 'a';
                    char enc = (char)(((c + shift - offset) % 26) + offset);
                    sb.Append(enc);
                }
                else sb.Append(c);
            }
            return sb.ToString();
        }

        public string TrenRayiEncrypt(string metin, string key)
        {
            if (metin == null) return null;
            if (!int.TryParse(key, out int rails) || rails < 1) rails = 3;
            if (rails == 1) return metin;

            var rows = new StringBuilder[rails];
            for (int i = 0; i < rails; i++) rows[i] = new StringBuilder();

            int currentRow = 0;
            int direction = 1;

            foreach (char ch in metin)
            {
                rows[currentRow].Append(ch);
                currentRow += direction;
                if (currentRow == rails - 1) direction = -1;
                else if (currentRow == 0) direction = 1;
            }

            var result = new StringBuilder();
            foreach (var r in rows) result.Append(r);
            return result.ToString();
        }

        public string AesEncrypt(string metin, string key, string iv = null)
        {
            if (string.IsNullOrEmpty(key)) throw new ArgumentException("Anahtar boş olamaz.");

            byte[] keyBytes;
            using (SHA256 sha256 = SHA256.Create()) keyBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(key));
            byte[][] roundKeys = AesKeyExpansion(keyBytes);

            byte[] ivBytes;
            if (!string.IsNullOrEmpty(iv))
            {
                byte[] temp = Encoding.UTF8.GetBytes(iv);
                ivBytes = new byte[16];
                Array.Copy(temp, ivBytes, Math.Min(temp.Length, 16));
            }
            else
            {
                ivBytes = new byte[16];
                using (var rng = RandomNumberGenerator.Create()) rng.GetBytes(ivBytes);
            }

            byte[] plainBytes = Encoding.UTF8.GetBytes(metin);
            int padding = 16 - (plainBytes.Length % 16);
            byte[] padded = new byte[plainBytes.Length + padding];
            Array.Copy(plainBytes, padded, plainBytes.Length);
            for (int i = plainBytes.Length; i < padded.Length; i++) padded[i] = (byte)padding;

            List<byte> cipherText = new List<byte>();
            byte[] prev = ivBytes;

            for (int i = 0; i < padded.Length; i += 16)
            {
                byte[] block = new byte[16];
                Array.Copy(padded, i, block, 0, 16);
                for (int k = 0; k < 16; k++) block[k] ^= prev[k];
                byte[] encryptedBlock = AesCipher(block, roundKeys);
                cipherText.AddRange(encryptedBlock);
                prev = encryptedBlock;
            }

            byte[] finalResult = new byte[ivBytes.Length + cipherText.Count];
            Array.Copy(ivBytes, 0, finalResult, 0, ivBytes.Length);
            Array.Copy(cipherText.ToArray(), 0, finalResult, ivBytes.Length, cipherText.Count);

            return Convert.ToBase64String(finalResult);
        }

        public string DesEncrypt(string metin, string key, string iv = null)
        {
            if (string.IsNullOrEmpty(key)) throw new ArgumentException("Anahtar boş olamaz.");

            byte[] keyBytes;
            using (MD5 md5 = MD5.Create())
            {
                byte[] hash = md5.ComputeHash(Encoding.UTF8.GetBytes(key));
                keyBytes = new byte[8];
                Array.Copy(hash, 0, keyBytes, 0, 8);
            }

            byte[] ivBytes = new byte[8];
            if (!string.IsNullOrEmpty(iv))
            {
                byte[] temp = Encoding.UTF8.GetBytes(iv);
                Array.Copy(temp, ivBytes, Math.Min(temp.Length, 8));
            }
            else
            {
                using (var rng = RandomNumberGenerator.Create()) rng.GetBytes(ivBytes);
            }

            using (DES des = DES.Create())
            {
                des.Key = keyBytes;
                des.IV = ivBytes;
                des.Mode = CipherMode.CBC;
                des.Padding = PaddingMode.PKCS7;

                using (ICryptoTransform encryptor = des.CreateEncryptor())
                {
                    byte[] inputBytes = Encoding.UTF8.GetBytes(metin);
                    byte[] encryptedBytes = encryptor.TransformFinalBlock(inputBytes, 0, inputBytes.Length);
                    byte[] resultWithIV = new byte[des.IV.Length + encryptedBytes.Length];
                    Array.Copy(des.IV, 0, resultWithIV, 0, des.IV.Length);
                    Array.Copy(encryptedBytes, 0, resultWithIV, des.IV.Length, encryptedBytes.Length);
                    return Convert.ToBase64String(resultWithIV);
                }
            }
        }

        public string RsaEncrypt(string metin, string publicKeyXml)
        {
            try
            {
                byte[] dataToEncrypt = Encoding.UTF8.GetBytes(metin);
                using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
                {
                    rsa.FromXmlString(publicKeyXml);
                    byte[] encryptedData = rsa.Encrypt(dataToEncrypt, false);
                    return Convert.ToBase64String(encryptedData);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("RSA Şifreleme Hatası: " + ex.Message);
            }
        }

        public string ManuelDesEncrypt(string metin, string key, string iv = null)
        {
            if (string.IsNullOrEmpty(key) || key.Length != 8) throw new ArgumentException("Manuel DES için Key tam 8 karakter (64-bit) olmalıdır.");

            byte[] inputBytes = Encoding.UTF8.GetBytes(metin);
            byte[] keyBytes = Encoding.UTF8.GetBytes(key);
            byte[] ivBytes;

            if (!string.IsNullOrEmpty(iv) && iv.Length == 8) ivBytes = Encoding.UTF8.GetBytes(iv);
            else ivBytes = new byte[8];

            int blockSize = 8;
            int padding = blockSize - (inputBytes.Length % blockSize);
            byte[] paddedInput = new byte[inputBytes.Length + padding];
            Array.Copy(inputBytes, paddedInput, inputBytes.Length);
            for (int i = inputBytes.Length; i < paddedInput.Length; i++) paddedInput[i] = (byte)padding;

            byte[] output = new byte[paddedInput.Length];
            byte[] previousBlock = new byte[8];
            Array.Copy(ivBytes, previousBlock, 8);

            for (int i = 0; i < paddedInput.Length; i += blockSize)
            {
                byte[] block = new byte[8];
                Array.Copy(paddedInput, i, block, 0, 8);

                for (int j = 0; j < 8; j++) block[j] ^= previousBlock[j];

                UInt32 L = BitConverter.ToUInt32(block, 0);
                UInt32 R = BitConverter.ToUInt32(block, 4);
                UInt64 key64 = BitConverter.ToUInt64(keyBytes, 0);

                for (int round = 0; round < 16; round++)
                {
                    UInt32 temp = R;
                    UInt32 subKey = (UInt32)((key64 >> ((round * 3) % 32)) & 0xFFFFFFFF);
                    UInt32 fResult = (R ^ subKey);
                    fResult = (fResult << 3) | (fResult >> 29);
                    R = L ^ fResult;
                    L = temp;
                }

                byte[] encBlock = new byte[8];
                BitConverter.GetBytes(L).CopyTo(encBlock, 0);
                BitConverter.GetBytes(R).CopyTo(encBlock, 4);

                Array.Copy(encBlock, 0, output, i, 8);
                Array.Copy(encBlock, previousBlock, 8);
            }

            byte[] finalResult = new byte[ivBytes.Length + output.Length];
            Array.Copy(ivBytes, 0, finalResult, 0, ivBytes.Length);
            Array.Copy(output, 0, finalResult, ivBytes.Length, output.Length);

            return Convert.ToBase64String(finalResult);
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

        private static byte AesGMul2(byte a)
        {
            return (byte)(((a & 0x80) != 0) ? ((a << 1) ^ 0x1B) : (a << 1));
        }

        private static void AesSubBytes(byte[] state)
        {
            for (int i = 0; i < 16; i++) state[i] = AesSBox[state[i]];
        }

        private static void AesShiftRows(byte[] state)
        {
            byte temp = state[1]; state[1] = state[5]; state[5] = state[9]; state[9] = state[13]; state[13] = temp;
            temp = state[2]; state[2] = state[10]; state[10] = temp;
            temp = state[6]; state[6] = state[14]; state[14] = temp;
            temp = state[15]; state[15] = state[11]; state[11] = state[7]; state[7] = state[3]; state[3] = temp;
        }

        private static void AesMixColumns(byte[] state)
        {
            for (int i = 0; i < 16; i += 4)
            {
                byte s0 = state[i], s1 = state[i + 1], s2 = state[i + 2], s3 = state[i + 3];
                state[i] = (byte)(AesGMul2(s0) ^ (AesGMul2(s1) ^ s1) ^ s2 ^ s3);
                state[i + 1] = (byte)(s0 ^ AesGMul2(s1) ^ (AesGMul2(s2) ^ s2) ^ s3);
                state[i + 2] = (byte)(s0 ^ s1 ^ AesGMul2(s2) ^ (AesGMul2(s3) ^ s3));
                state[i + 3] = (byte)((AesGMul2(s0) ^ s0) ^ s1 ^ s2 ^ AesGMul2(s3));
            }
        }

        private static void AesAddRoundKey(byte[] state, byte[] roundKey)
        {
            for (int i = 0; i < 16; i++) state[i] ^= roundKey[i];
        }

        private static byte[][] AesKeyExpansion(byte[] key)
        {
            int Nk = key.Length / 4;
            int Nr = Nk + 6;
            int Nb = 4;
            byte[] expanded = new byte[Nb * (Nr + 1) * 4];
            Array.Copy(key, expanded, key.Length);
            int bytesGen = key.Length;
            int rconIter = 1;
            byte[] temp = new byte[4];

            while (bytesGen < expanded.Length)
            {
                for (int i = 0; i < 4; i++) temp[i] = expanded[bytesGen - 4 + i];
                if (bytesGen % key.Length == 0)
                {
                    byte t = temp[0]; temp[0] = temp[1]; temp[1] = temp[2]; temp[2] = temp[3]; temp[3] = t;
                    for (int i = 0; i < 4; i++) temp[i] = AesSBox[temp[i]];
                    temp[0] ^= AesRcon[rconIter++];
                }
                else if (Nk > 6 && bytesGen % key.Length == 16)
                {
                    for (int i = 0; i < 4; i++) temp[i] = AesSBox[temp[i]];
                }
                for (int i = 0; i < 4; i++)
                {
                    expanded[bytesGen] = (byte)(expanded[bytesGen - key.Length] ^ temp[i]);
                    bytesGen++;
                }
            }
            byte[][] roundKeys = new byte[Nr + 1][];
            for (int i = 0; i <= Nr; i++)
            {
                roundKeys[i] = new byte[16];
                Array.Copy(expanded, i * 16, roundKeys[i], 0, 16);
            }
            return roundKeys;
        }

        private static byte[] AesCipher(byte[] block, byte[][] roundKeys)
        {
            byte[] state = (byte[])block.Clone();
            int Nr = roundKeys.Length - 1;
            AesAddRoundKey(state, roundKeys[0]);
            for (int r = 1; r < Nr; r++)
            {
                AesSubBytes(state);
                AesShiftRows(state);
                AesMixColumns(state);
                AesAddRoundKey(state, roundKeys[r]);
            }
            AesSubBytes(state);
            AesShiftRows(state);
            AesAddRoundKey(state, roundKeys[Nr]);
            return state;
        }
        public string EccEncrypt(string metin, string publicKeyBase64)
        {
            try
            {
                if (string.IsNullOrEmpty(metin)) return "";
                if (string.IsNullOrEmpty(publicKeyBase64)) throw new Exception("Karşı tarafın anahtarı eksik!");

                byte[] inputData = Encoding.UTF8.GetBytes(metin);
                byte[] keyBytes = Convert.FromBase64String(publicKeyBase64);
                AsymmetricKeyParameter remotePublicKey = PublicKeyFactory.CreateKey(keyBytes);

                var ecP = Org.BouncyCastle.Asn1.X9.ECNamedCurveTable.GetByName("secp256r1")
                          ?? Org.BouncyCastle.Asn1.X9.ECNamedCurveTable.GetByName("prime256v1");

                var domainParams = new Org.BouncyCastle.Crypto.Parameters.ECDomainParameters(
                    ecP.Curve, ecP.G, ecP.N, ecP.H, ecP.GetSeed());

                var ephemeralKeyGen = new Org.BouncyCastle.Crypto.Generators.ECKeyPairGenerator();
                ephemeralKeyGen.Init(new Org.BouncyCastle.Crypto.Parameters.ECKeyGenerationParameters(
                    domainParams, new Org.BouncyCastle.Security.SecureRandom()));
                var ephemeralKeyPair = ephemeralKeyGen.GenerateKeyPair();

                var engine = new IesEngine(
                    new Org.BouncyCastle.Crypto.Agreement.ECDHBasicAgreement(),
                    new Org.BouncyCastle.Crypto.Generators.Kdf2BytesGenerator(new Org.BouncyCastle.Crypto.Digests.Sha256Digest()),
                    new Org.BouncyCastle.Crypto.Macs.HMac(new Org.BouncyCastle.Crypto.Digests.Sha256Digest()));

             
                var iesParams = new Org.BouncyCastle.Crypto.Parameters.IesParameters(new byte[0], new byte[0], 128);

                engine.Init(true, ephemeralKeyPair.Private, remotePublicKey, iesParams);

              
                byte[] encryptedData = engine.ProcessBlock(inputData, 0, inputData.Length);

                var ephPubEncoded = Org.BouncyCastle.X509.SubjectPublicKeyInfoFactory.CreateSubjectPublicKeyInfo(ephemeralKeyPair.Public).GetEncoded();
                byte[] result = new byte[ephPubEncoded.Length + encryptedData.Length];
                Array.Copy(ephPubEncoded, 0, result, 0, ephPubEncoded.Length);
                Array.Copy(encryptedData, 0, result, ephPubEncoded.Length, encryptedData.Length);

                return Convert.ToBase64String(result);
            }
            catch (Exception ex) { throw new Exception("ECC Şifreleme Hatası: " + ex.Message); }
        }
        public void EccKeyGenerate(out string pubBase64, out string privBase64)
        {
            
            
            var ecP = Org.BouncyCastle.Asn1.X9.ECNamedCurveTable.GetByName("secp256r1")
                      ?? Org.BouncyCastle.Asn1.X9.ECNamedCurveTable.GetByName("prime256v1");

         
            if (ecP == null)
                throw new Exception("HATA: ECC eğrisi (secp256r1/prime256v1) kütüphanede bulunamadı!");

           
            var domainParams = new Org.BouncyCastle.Crypto.Parameters.ECDomainParameters(
                ecP.Curve, ecP.G, ecP.N, ecP.H, ecP.GetSeed());

            
            var gen = new Org.BouncyCastle.Crypto.Generators.ECKeyPairGenerator();
            var keyGenParam = new Org.BouncyCastle.Crypto.Parameters.ECKeyGenerationParameters(
                domainParams,
                new Org.BouncyCastle.Security.SecureRandom()
            );

            gen.Init(keyGenParam);
            Org.BouncyCastle.Crypto.AsymmetricCipherKeyPair keyPair = gen.GenerateKeyPair();

          
            var pubInfo = Org.BouncyCastle.X509.SubjectPublicKeyInfoFactory.CreateSubjectPublicKeyInfo(keyPair.Public);
            pubBase64 = Convert.ToBase64String(pubInfo.GetEncoded());

            var privInfo = Org.BouncyCastle.Pkcs.PrivateKeyInfoFactory.CreatePrivateKeyInfo(keyPair.Private);
            privBase64 = Convert.ToBase64String(privInfo.GetEncoded());
        }
    }
}