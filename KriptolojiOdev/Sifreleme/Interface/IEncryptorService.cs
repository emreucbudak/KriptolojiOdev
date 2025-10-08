using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KriptolojiOdev.Sifreleme.Interface
{
    public interface IEncryptorService
    {
        string CaesarEncrypt(string metin);
        string SubstitutionEncrypt(string metin, string key);
        string AffineEncrypt(string metin, int a = 5, int b = 8);
        string VigenereEncrypt(string metin, string key);
    }
}
