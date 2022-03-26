using PictureLibraryModel.Resources;
using System;
using System.Security.Cryptography;
using System.Text;

namespace PictureLibraryModel.Services.StringEncryption
{
    public class StringEncryptionService : IStringEncryptionService
    {
        private Aes AesProvider { get; }

        public StringEncryptionService()
        {
            var privateKey = ModelResources.EncryptionPrivateKey;
            AesProvider = Aes.Create();

            AesProvider.BlockSize = 128;
            AesProvider.KeySize = 256;
            AesProvider.GenerateIV();
            AesProvider.Key = ASCIIEncoding.ASCII.GetBytes(privateKey);
            AesProvider.Mode = CipherMode.CBC;
            AesProvider.Padding = PaddingMode.PKCS7;
        }

        public string Encrypt(string text)
        {
            ICryptoTransform transform = AesProvider.CreateEncryptor();

            byte[] bytes = ASCIIEncoding.ASCII.GetBytes(text);
            byte[] encryptedBytes = transform.TransformFinalBlock(bytes, 0, text.Length);

            string encryptedString = Convert.ToBase64String(encryptedBytes);

            return encryptedString;
        }

        public string Decrypt(string text)
        {
            ICryptoTransform transform = AesProvider.CreateDecryptor();

            byte[] encryptedBytes = Convert.FromBase64String(text);
            byte[] decryptedBytes = transform.TransformFinalBlock(encryptedBytes, 0, encryptedBytes.Length);

            string decryptedString = ASCIIEncoding.ASCII.GetString(decryptedBytes);

            return decryptedString;
        }
    }
}
