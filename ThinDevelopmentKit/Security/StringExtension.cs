using System.Security.Cryptography;
using System.Text;

namespace ThinDevelopmentKit.Security
{
    /// <summary>
    /// 字符串扩张方法
    /// </summary>
    public static class StringExtension
    {
        /// <summary>
        /// 获取指定字符串的MD5编码（字母小写）
        /// </summary>
        /// <param name="text">要获取MD5码的字符串</param>
        /// <returns></returns>
        public static string MD5(this string text)
        {
            MD5 md5Hasher = System.Security.Cryptography.MD5.Create();
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(text));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            md5Hasher.Clear();
            return sBuilder.ToString();
        }
    }
}
