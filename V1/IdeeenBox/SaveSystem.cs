using System.Security.Cryptography;
using System.Runtime.Serialization.Formatters.Binary;

namespace IdeeenBox;

public static class SaveSystem
{
    private const string EncryptedFilePath = "users.bin";
    private const string Password = "password";

    public static void Save(List<User> users)
    {
        var serializedData = SerializeList(users);
        var encryptedData = EncryptData(serializedData);
        File.WriteAllBytes(EncryptedFilePath, encryptedData);
    }

    [Obsolete("Obsolete")]
    private static byte[] SerializeList(List<User> users)
    {
        using var stream = new MemoryStream();
        var formatter = new BinaryFormatter();
        formatter.Serialize(stream, users);
        return stream.ToArray();
    }
    
    private static byte[] EncryptData(byte[] data)
    {
        using var encryptor = Aes.Create();
        
        var pdb = new Rfc2898DeriveBytes(Password, "Ideeen Box"u8.ToArray());
        encryptor.Key = pdb.GetBytes(32);
        encryptor.IV = pdb.GetBytes(16);

        using var memoryStream = new MemoryStream();
        using (var cryptoStream = new CryptoStream(memoryStream, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
        {
            cryptoStream.Write(data, 0, data.Length);
            cryptoStream.Close();
        }
        var encryptedData = memoryStream.ToArray();

        return encryptedData;
    }

    public static List<User> Read()
    {
        byte[] encryptedData;
        
        try { encryptedData = File.ReadAllBytes(EncryptedFilePath); }
        catch (Exception) { return new List<User>(); }
        
        var decryptedData = DecryptData(encryptedData);
        return DeserializeList(decryptedData);
    }

    private static byte[] DecryptData(byte[] data)
    {
        using var encryptor = Aes.Create();
        
        var pdb = new Rfc2898DeriveBytes(Password, "Ideeen Box"u8.ToArray());
        encryptor.Key = pdb.GetBytes(32);
        encryptor.IV = pdb.GetBytes(16);

        using var memoryStream = new MemoryStream();
        using (var cryptoStream = new CryptoStream(memoryStream, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
        {
            cryptoStream.Write(data, 0, data.Length);
            cryptoStream.Close();
        }
        var decryptedData = memoryStream.ToArray();

        return decryptedData;
    }

    [Obsolete("Obsolete")]
    private static List<User> DeserializeList(byte[] data)
    {
        using var stream = new MemoryStream(data);
        var formatter = new BinaryFormatter();
        return (List<User>)formatter.Deserialize(stream);
    }
}