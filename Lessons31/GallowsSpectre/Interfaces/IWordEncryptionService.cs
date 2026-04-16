namespace GallowsSpectre.Interfaces;

public interface IWordEncryptionService
{
    string Encrypt(string plainText);
    string Decrypt(string cipherText);
}
