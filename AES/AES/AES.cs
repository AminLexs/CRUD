using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Security.Cryptography;
using PluginInterface;

namespace AES
{
    public class AES:IPlugin
    {
        public byte[] AES_Encrypt(byte[] bytesToBeEncrypted, byte[] passwordBytes, byte[] saltBytes)
        {
            byte[] encryptedBytes = null;
            if (saltBytes == null)
            {
                saltBytes = new byte[] { 101, 111, 67, 85, 13, 1, 217, 99 };
            }
            if (passwordBytes == null)
            {
                SHA256 mySHA256 = SHA256Managed.Create();
                
                passwordBytes = mySHA256.ComputeHash(Encoding.ASCII.GetBytes("Сорок тысяч обезьян с жопой что-то там того"));
            }
            using (MemoryStream ms = new MemoryStream())
            {
                using (RijndaelManaged AES = new RijndaelManaged())
                {
                    AES.KeySize = 256;
                    AES.BlockSize = 128;

                    var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);
                    AES.Key = key.GetBytes(AES.KeySize / 8);
                    AES.IV = key.GetBytes(AES.BlockSize / 8);

                    AES.Mode = CipherMode.CBC;
                    AES.Padding = PaddingMode.PKCS7;
                    using (var cs = new CryptoStream(ms, AES.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(bytesToBeEncrypted, 0, bytesToBeEncrypted.Length);
                    }
                    encryptedBytes = ms.ToArray();
                }
            }

            return encryptedBytes;
        }

        public byte[] AES_Decrypt(byte[] bytesToBeDecrypted, byte[] passwordBytes, byte[] saltBytes)
        {
            byte[] decryptedBytes = null;

            if (saltBytes == null)
            {
                saltBytes = new byte[] { 101, 111, 67, 85, 13, 1, 217, 99 };
            }

            if (passwordBytes == null)
            {
                SHA256 mySHA256 = SHA256Managed.Create();
                passwordBytes = mySHA256.ComputeHash(Encoding.ASCII.GetBytes("Сорок тысяч обезьян с жопой что-то там того"));
            }
            using (MemoryStream ms = new MemoryStream())
            {
                using (RijndaelManaged AES = new RijndaelManaged())
                {
                    AES.KeySize = 256;
                    AES.BlockSize = 128;

                    var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);
                    AES.Key = key.GetBytes(AES.KeySize / 8);
                    AES.IV = key.GetBytes(AES.BlockSize / 8);

                    AES.Mode = CipherMode.CBC;
                    AES.Padding = PaddingMode.PKCS7;
                    try
                    {
                        using (var cs = new CryptoStream(ms, AES.CreateDecryptor(), CryptoStreamMode.Write))
                        {
                            cs.Write(bytesToBeDecrypted, 0, bytesToBeDecrypted.Length);
                        }
                        decryptedBytes = ms.ToArray();
                        return decryptedBytes;
                    }
                    catch
                    {
                        return null;
                    }

                }
            }
        }
        public static byte[] GetBytes(string str)
        {
            byte[] bytes = new byte[str.Length * sizeof(char)];
            System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }

        public static string GetString(byte[] bytes)
        {
            char[] chars = new char[bytes.Length / sizeof(char)];
            System.Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
            return new string(chars);
        }

        public string getExt()
        {
            return ".aes";
        }

        public byte[] Encrypt(string input,string keypath)
        {
            return AES_Encrypt(GetBytes(input), null, null);           
        }

        public string Decrypt(byte[] output,string keypath)
        {
            string decrypted = GetString(AES_Decrypt(output, null, null));
            return decrypted;
        }
    }
}
