using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KriptolojiOdev.Sifreleme.Interface
{
    public interface ITransportSecurityService
    {

        string Encrypt(string plainText);

    
        string Decrypt(string cipherText);
    }
}
