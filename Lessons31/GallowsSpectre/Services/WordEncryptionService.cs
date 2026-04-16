using System.Text;
using GallowsSpectre.Interfaces;

namespace GallowsSpectre.Services;

public class WordEncryptionService : IWordEncryptionService
{
    private const string Key = "GallowsCourseKey";

    public string Encrypt(string plainText)
    {
        byte[] data = Encoding.UTF8.GetBytes(plainText);
        byte[] key = Encoding.UTF8.GetBytes(Key);
        byte[] result = new byte[data.Length];

        for (int i = 0; i < data.Length; i++)
        {
            result[i] = (byte)(data[i] ^ key[i % key.Length]);
        }

        return Convert.ToBase64String(result);
    }

    public string Decrypt(string cipherText)
    {
        byte[] data = Convert.FromBase64String(cipherText);
        byte[] key = Encoding.UTF8.GetBytes(Key);
        byte[] result = new byte[data.Length];

        for (int i = 0; i < data.Length; i++)
        {
            result[i] = (byte)(data[i] ^ key[i % key.Length]);
        }

        return Encoding.UTF8.GetString(result);
    }
}
