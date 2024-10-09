using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace LegacyLibrary
{
    public class LegacyHelper
    {
        public DateTime ConvertDate(string dateString)
        {
            // This method is marked as obsolete and will cause a compilation error in .NET 5+
            return XmlConvert.ToDateTime(dateString);
        }

        public object DeserializeBinary(Stream stream)
        {
            var binaryFormatter = new BinaryFormatter();
            stream.Seek(0, SeekOrigin.Begin);
            return binaryFormatter.UnsafeDeserialize(stream, null);
        }
    }
}
