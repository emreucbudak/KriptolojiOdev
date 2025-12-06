using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KriptolojiOdev.Sifreleme.Interface
{
    public interface IDecryptorService
    {
        string DecryptorCaesar(string metin);
        string DecryptorSubstitiuion (string metin,string key);
        string DecryptorAffine(string metin, int a = 5, int b = 8);
        string DecryptorVigenere (string metin,string key);
        string ColumnarDecrypt(string metin, string key);
        string PolybiusDecrypt(string metin, string key);
        string PigpenDecrypt(string metin, string key);
        string HillDecrypt(string metin, string key);
        string RotaDecrypt(string metin, string key);
        string TrenRayiDecrypt (string metin, string key);
        string AesDecrypt(string metin, string key, string iv = null);
        string DesDecrypt(string metin, string key, string iv = null);
    }
}
