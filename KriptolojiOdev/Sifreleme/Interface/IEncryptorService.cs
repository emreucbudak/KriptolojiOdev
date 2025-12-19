namespace KriptolojiOdev.Sifreleme.Interface
{
    public interface IEncryptorService
    {
        string CaesarEncrypt(string metin);
        string SubstitutionEncrypt(string metin, string key);
        string AffineEncrypt(string metin, int a = 5, int b = 8);
        string VigenereEncrypt(string metin, string key);
        string ColumnarEncrypt(string metin, string key);
        string PolybiusEncrypt(string metin, string key);
        string PigpenEncrypt(string metin, string key);
        string HillEncrypt(string metin, string key);
        string RotaEncrypt(string metin, string key);
        string TrenRayiEncrypt(string metin, string key);
        string AesEncrypt(string metin, string key, string iv = null);
        string DesEncrypt(string metin, string key, string iv = null);
        string RsaEncrypt(string metin, string publicKeyXml);
        string ManuelDesEncrypt(string metin, string key, string iv = null);
    }
}