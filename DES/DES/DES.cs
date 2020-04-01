using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;
using PluginInterface;
namespace DES
{
    public class DES:IPlugin
    {
        public string getExt()
        {
            return ".des";
        }
        SymmetricAlgorithm key = SymmetricAlgorithm.Create();
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

        public byte[] Encrypt(string plainText,string pathkey)
        {
            TripleDESCryptoServiceProvider cryptAlgorithm = new TripleDESCryptoServiceProvider();
            byte[] cryptedbytes;
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream csEncrypt = new CryptoStream(ms, cryptAlgorithm.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    byte[] buff = GetBytes(plainText);
                    csEncrypt.Write(buff, 0, buff.Length);
                }
                cryptedbytes = ms.ToArray();
            }       
                FileStream fsFileKey = File.Create(pathkey+".deskey");
                BinaryWriter bwFile = new BinaryWriter(fsFileKey);
                bwFile.Write(cryptAlgorithm.Key);
                bwFile.Write(cryptAlgorithm.IV);
                bwFile.Flush();
                bwFile.Close();

                return cryptedbytes;

        }

        public string Decrypt(byte[] cipherText,string pathkey)
        {
            FileStream fsKeyFile = File.OpenRead(pathkey+"key");
            TripleDESCryptoServiceProvider cryptAlgorithm = new TripleDESCryptoServiceProvider();
            BinaryReader brFile = new BinaryReader(fsKeyFile);
            cryptAlgorithm.Key = brFile.ReadBytes(24);
            cryptAlgorithm.IV = brFile.ReadBytes(8);
            byte[] decryptedBytes;
            using (MemoryStream ms = new MemoryStream())
            {
                using (var cs = new CryptoStream(ms, cryptAlgorithm.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(cipherText, 0, cipherText.Length);
                }
                decryptedBytes = ms.ToArray();
             }
            return GetString(decryptedBytes);
        }
    }
}
