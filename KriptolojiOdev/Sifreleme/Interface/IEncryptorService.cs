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
        string SubstitutionEncrypt(string metin);
    }
}
