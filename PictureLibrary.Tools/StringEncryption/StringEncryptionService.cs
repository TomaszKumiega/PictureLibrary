using System.Security.Cryptography;
using System.Text;

namespace PictureLibrary.Tools.StringEncryption
{
    public class StringEncryptionService : IStringEncryptionService
    {
        private Aes AesProvider { get; }

        public StringEncryptionService()
        {
            var privateKey = Resources.EncryptionKey;
            AesProvider = Aes.Create();

            AesProvider.BlockSize = 128;
            AesProvider.KeySize = 256;
            AesProvider.GenerateIV();
            AesProvider.Key = Encoding.ASCII.GetBytes(privateKey);
            AesProvider.Mode = CipherMode.CBC;
            AesProvider.Padding = PaddingMode.PKCS7;
        }

        public string Encrypt(string text)
        {
            ICryptoTransform transform = AesProvider.CreateEncryptor();

            byte[] bytes = Encoding.ASCII.GetBytes(text);
            byte[] encryptedBytes = transform.TransformFinalBlock(bytes, 0, text.Length);

            string encryptedString = Convert.ToBase64String(encryptedBytes);

            return encryptedString;
        }

        public string Decrypt(string text)
        {
            ICryptoTransform transform = AesProvider.CreateDecryptor();

            byte[] encryptedBytes = Convert.FromBase64String(text);
            byte[] decryptedBytes = transform.TransformFinalBlock(encryptedBytes, 0, encryptedBytes.Length);

            string decryptedString = Encoding.ASCII.GetString(decryptedBytes);

            return decryptedString;
        }
    }
}
