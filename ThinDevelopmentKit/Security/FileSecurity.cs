using System.IO;
using System.Security.Cryptography;

namespace ThinDevelopmentKit.Security
{
    /// <summary>
    /// 文件加密、解密
    /// </summary>
    public static class FileSecurity
    {
        private static readonly byte[] IV = new byte[16] {
            36, 179, 238, 200, 42, 8, 226, 55,
            4, 29, 230, 91, 213, 121, 62, 134
        };

        private static readonly byte[] KEY = new byte[32] {
            154, 126, 163, 24, 48, 53, 181, 216,
            226, 99, 113, 219, 160, 56, 247, 100,
            115, 16, 60, 143, 191, 214, 82, 121,
            141, 94, 25, 206, 158, 0, 219, 49
        };

        /// <summary>
        /// 将指定文件加密
        /// </summary>
        /// <param name="file">要加密的文件</param>
        public static void EncryptFile(string file)
        {
            string content = string.Empty;
            FileStream readStream = File.OpenRead(file);
            using (StreamReader reader = new StreamReader(readStream))
            {
                content = reader.ReadToEnd();
            }

            RijndaelManaged security = new RijndaelManaged();
            security.IV = IV;
            security.Key = KEY;

            FileStream writeSteam = File.OpenWrite(file);
            CryptoStream cryptoStream = new CryptoStream(writeSteam,
                security.CreateEncryptor(), CryptoStreamMode.Write);
            using (StreamWriter writer = new StreamWriter(cryptoStream))
            {
                writer.Write(content);
            }
        }

        /// <summary>
        /// 获取用于写操作的加密文件流
        /// </summary>
        /// <param name="file">要加密的文件</param>
        /// <returns></returns>
        public static CryptoStream GetCryptoStreamForWrite(string file)
        {
            RijndaelManaged security = new RijndaelManaged();
            security.IV = IV;
            security.Key = KEY;

            FileStream writeSteam = File.OpenWrite(file);
            CryptoStream cryptoStream = new CryptoStream(writeSteam,
                security.CreateEncryptor(), CryptoStreamMode.Write);
            
            return cryptoStream;
        }

        /// <summary>
        /// 将指定的文件解密
        /// </summary>
        /// <param name="file">要解密的文件</param>
        public static void DecryptFile(string file)
        {
            RijndaelManaged security = new RijndaelManaged();
            security.IV = IV;
            security.Key = KEY;

            FileStream readStream = File.OpenRead(file);
            CryptoStream cryptoStream = new CryptoStream(readStream,
                security.CreateDecryptor(), CryptoStreamMode.Read);
            string content = string.Empty;
            using (StreamReader reader = new StreamReader(cryptoStream))
            {
                content = reader.ReadToEnd();
            }

            FileStream writeSteam = File.OpenWrite(file);
            using (StreamWriter writer = new StreamWriter(writeSteam))
            {
                writer.Write(content);
            }
        }

        /// <summary>
        /// 获取用于读操作的加密文件流
        /// </summary>
        /// <param name="file">要解密的文件</param>
        /// <returns></returns>
        public static CryptoStream GetCryptoStreamForRead(string file)
        {
            RijndaelManaged security = new RijndaelManaged();
            security.IV = IV;
            security.Key = KEY;

            FileStream readStream = File.OpenRead(file);
            CryptoStream cryptoStream = new CryptoStream(readStream,
                security.CreateDecryptor(), CryptoStreamMode.Read);

            return cryptoStream;
        }
    }
}
