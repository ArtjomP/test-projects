// See https://aka.ms/new-console-template for more information

using System.Security.Cryptography;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

var faders = new Fader[]
{
    new Fader { MappingName = "Hello"},
    new Fader { MappingName = "World"},
};

var fixtures = new FixtureGroup()
{
    Faders = faders
};

Serialize(fixtures, "fixtures.xml");

void Serialize<T>(T obj, String path)
{
    var serializer = new XmlSerializer(typeof(T));

    using var sww = new Utf8StringWriter();
    var xmlSettings = new XmlWriterSettings
    {
        Indent = true,
        // NewLineOnAttributes = true,
        Encoding = Encoding.UTF8
    };
    using var writer = XmlWriter.Create(sww, xmlSettings);

    serializer.Serialize(writer, obj);
    var xml = sww.ToString();
    File.WriteAllText(path, xml);
}

#region code just to keep AES
var original = "test dummy string;";
// Create a new instance of the RijndaelManaged 
// class.  This generates a new key and initialization  
// vector (IV). 
using (var myRijndael = Aes.Create())
{

    myRijndael.GenerateKey();
    myRijndael.GenerateIV();
    // Encrypt the string to an array of bytes. 
    byte[] encrypted = EncryptStringToBytes(original, myRijndael.Key, myRijndael.IV);

    // Decrypt the bytes to a string. 
    string roundtrip = DecryptStringFromBytes(encrypted, myRijndael.Key, myRijndael.IV);

    //Display the original data and the decrypted data.
    Console.WriteLine("Original:   {0}", original);
    Console.WriteLine("Round Trip: {0}", roundtrip);
}

static byte[] EncryptStringToBytes(string plainText, byte[] Key, byte[] IV)
{
    // Check arguments. 
    if (plainText == null || plainText.Length <= 0)
        throw new ArgumentNullException("plainText");
    if (Key == null || Key.Length <= 0)
        throw new ArgumentNullException("Key");
    if (IV == null || IV.Length <= 0)
        throw new ArgumentNullException("IV");
    byte[] encrypted;
    // Create an RijndaelManaged object 
    // with the specified key and IV. 
    using (var rijAlg = Aes.Create())
    {
        rijAlg.Key = Key;
        rijAlg.IV = IV;

        // Create a decryptor to perform the stream transform.
        ICryptoTransform encryptor = rijAlg.CreateEncryptor(rijAlg.Key, rijAlg.IV);

        // Create the streams used for encryption. 
        using (var msEncrypt = new MemoryStream())
        {
            using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
            {
                using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                {

                    //Write all data to the stream.
                    swEncrypt.Write(plainText);
                }
                encrypted = msEncrypt.ToArray();
            }
        }
    }


    // Return the encrypted bytes from the memory stream. 
    return encrypted;

}

static string DecryptStringFromBytes(byte[] cipherText, byte[] Key, byte[] IV)
{
    // Check arguments. 
    if (cipherText == null || cipherText.Length <= 0)
        throw new ArgumentNullException("cipherText");
    if (Key == null || Key.Length <= 0)
        throw new ArgumentNullException("Key");
    if (IV == null || IV.Length <= 0)
        throw new ArgumentNullException("IV");

    // Declare the string used to hold 
    // the decrypted text. 
    string plaintext = String.Empty;

    // Create an RijndaelManaged object 
    // with the specified key and IV. 
    using (var rijAlg = Aes.Create())
    {
        rijAlg.Key = Key;
        rijAlg.IV = IV;

        // Create a decrytor to perform the stream transform.
        ICryptoTransform decryptor = rijAlg.CreateDecryptor(rijAlg.Key, rijAlg.IV);

        // Create the streams used for decryption. 
        using (MemoryStream msDecrypt = new MemoryStream(cipherText))
        {
            using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
            {
                using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                {

                    // Read the decrypted bytes from the decrypting stream 
                    // and place them in a string.
                    plaintext = srDecrypt.ReadToEnd();
                }
            }
        }

    }

    return plaintext;

}
#endregion