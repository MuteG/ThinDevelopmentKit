using System.IO;
using System.Xml;
using System.Xml.Serialization;
using ThinDevelopmentKit.Security;

namespace ThinDevelopmentKit.IO
{
    public class XMLHelper<T> where T : class, new()
    {
        /// <summary>
        /// 获取XML文件路径
        /// </summary>
        public string XMLFile { get; private set; }

        /// <summary>
        /// 获取是否需要加密
        /// </summary>
        public bool IsSerurity { get; private set; }

        public XMLHelper(string file, bool serurity)
        {
            this.XMLFile = file;
            this.IsSerurity = serurity;
        }

        public XMLHelper(string file)
            : this(file, false)
        {
        }

        public T Load()
        {
            Stream readStream = Stream.Null;
            if (this.IsSerurity)
            {
                readStream = FileSecurity.GetCryptoStreamForRead(this.XMLFile);
            }
            else
            {
                readStream = new FileStream(this.XMLFile, FileMode.Open, FileAccess.Read, FileShare.Read);
            }
            try
            {
                using (XmlReader reader = XmlReader.Create(readStream))
                {
                    XmlSerializer xs = XmlSerializer.FromTypes(new[] { typeof(T) })[0];
                    return xs.Deserialize(reader) as T;
                }
            }
            finally
            {
                if (null != readStream)
                {
                    readStream.Close();
                }
            }
        }

        public void Save(T obj)
        {
            if (!File.Exists(this.XMLFile))
            {
                File.Create(this.XMLFile).Close();
            }
            Stream writeStream = Stream.Null;
            if (this.IsSerurity)
            {
                writeStream = FileSecurity.GetCryptoStreamForWrite(this.XMLFile);
            }
            else
            {
                writeStream = new FileStream(this.XMLFile, FileMode.Open, FileAccess.Write, FileShare.Read);
            }
            try
            {
                using (XmlWriter writer = XmlWriter.Create(writeStream))
                {
                    XmlSerializer xs = XmlSerializer.FromTypes(new[] { typeof(T) })[0];
                    xs.Serialize(writer, obj);
                }
            }
            finally
            {
                if (null != writeStream)
                {
                    writeStream.Close();
                }
            }
        }
    }
}
