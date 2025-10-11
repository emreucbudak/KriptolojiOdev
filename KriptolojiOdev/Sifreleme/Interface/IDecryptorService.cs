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
    }
}
