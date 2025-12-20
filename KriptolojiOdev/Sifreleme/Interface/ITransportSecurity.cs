namespace KriptolojiOdev.Sifreleme.Interface
{
    public interface ITransportSecurityService
    {

        string Encrypt(string plainText);

    
        string Decrypt(string cipherText);
    }
}
