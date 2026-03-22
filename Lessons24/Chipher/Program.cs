using Cipher.Src.Data;
using Cipher.Src.Models;
using Cipher.Src.Services;

List<string> sourceStrings = DataSeed.GetSourceStrings();
CipherService cipherService = new CipherService();

List<ACipher> sortedACiphers = cipherService.GetSortedACiphers(sourceStrings, 1);
List<BCipher> sortedBCiphers = cipherService.GetSortedBCiphers(sourceStrings);

Console.WriteLine("=== Sorted ACipher objects ===");
foreach (ACipher cipher in sortedACiphers)
{
    Console.WriteLine($"Encoded: {cipher.Encode()}, Decoded: {cipher.Decode()}");
}

Console.WriteLine();

Console.WriteLine("=== Sorted BCipher objects ===");
foreach (BCipher cipher in sortedBCiphers)
{
    Console.WriteLine($"Encoded: {cipher.Encode()}, Decoded: {cipher.Decode()}");
}