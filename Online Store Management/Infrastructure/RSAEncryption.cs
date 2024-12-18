using System.Security.Cryptography;
using System.Text;

namespace Online_Store_Management.Infrastructure
{
    public static class RsaEncryption
    {
        public static (string PublicKey, string PrivateKey) GenerateKeys()
        {
            using var rsa = RSA.Create();
            return (
                PublicKey: Convert.ToBase64String(rsa.ExportSubjectPublicKeyInfo()),
                PrivateKey: Convert.ToBase64String(rsa.ExportPkcs8PrivateKey())
            );
        }

        public static RSA ImportPublicKey(string pemKey)
        {
            var base64Key = string.Join("\n", pemKey
                .Split('\n')
                .Where(line => !line.StartsWith("-----")));

            var keyBytes = Convert.FromBase64String(base64Key);

            var rsa = RSA.Create();
            rsa.ImportSubjectPublicKeyInfo(keyBytes, out _);

            return rsa;
        }

        public static string Encrypt(string plainText, string publicKey)
        {
            using var rsa = ImportPublicKey(publicKey);

            var plainBytes = Encoding.UTF8.GetBytes(plainText);
            var encryptedBytes = rsa.Encrypt(plainBytes, RSAEncryptionPadding.OaepSHA256);

            return Convert.ToBase64String(encryptedBytes);
        }

        public static string Decrypt(string encryptedText, string privateKey)
        {
            using var rsa = RSA.Create();
            rsa.ImportPkcs8PrivateKey(Convert.FromBase64String(privateKey), out _);
            var decryptedBytes = rsa.Decrypt(Convert.FromBase64String(encryptedText), RSAEncryptionPadding.OaepSHA256);
            return Encoding.UTF8.GetString(decryptedBytes);
        }
    }
}
