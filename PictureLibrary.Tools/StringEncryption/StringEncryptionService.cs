using System.Security.Cryptography;
using System.Text;

namespace PictureLibrary.Tools.StringEncryption
{
    public class StringEncryptionService : IStringEncryptionService
    {
        #region Private fields
        private readonly Aes _aesProvider;
        #endregion

        public StringEncryptionService()
        {
            var privateKey = Resources.EncryptionKey;
            _aesProvider = Aes.Create();

            _aesProvider.BlockSize = 128;
            _aesProvider.KeySize = 256;
            _aesProvider.GenerateIV();
            _aesProvider.Key = Encoding.ASCII.GetBytes(privateKey);
            _aesProvider.Mode = CipherMode.CBC;
            _aesProvider.Padding = PaddingMode.PKCS7;
        }

        #region Public methods
        public string Encrypt(string text)
        {
            ICryptoTransform transform = _aesProvider.CreateEncryptor();

            byte[] bytes = Encoding.ASCII.GetBytes(text);
            byte[] encryptedBytes = transform.TransformFinalBlock(bytes, 0, text.Length);

            string encryptedString = Convert.ToBase64String(encryptedBytes);

            return encryptedString;
        }

        public string Decrypt(string text)
        {
            ICryptoTransform transform = _aesProvider.CreateDecryptor();

            byte[] encryptedBytes = Convert.FromBase64String(text);
            byte[] decryptedBytes = transform.TransformFinalBlock(encryptedBytes, 0, encryptedBytes.Length);

            string decryptedString = Encoding.ASCII.GetString(decryptedBytes);

            return decryptedString;
        }
        #endregion
    }
}
