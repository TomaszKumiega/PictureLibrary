namespace PictureLibraryModel.Services.StringEncryption
{
    public interface IStringEncryptionService
    {
        string Decrypt(string text);
        string Encrypt(string text);
    }
}