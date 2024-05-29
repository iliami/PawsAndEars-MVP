using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Web;

namespace PawsAndEars
{
    public static class ExtensionMethods
    {
        public static T DeepCopy<T>(this T self) where T : class
        {
            if (!typeof(T).IsSerializable) throw new ArgumentException("Type must be serializable");

            if (self == null) return default(T);

            var formatter = new BinaryFormatter();
            using (var stream = new MemoryStream())
            {
                formatter.Serialize(stream, self);
                stream.Position = 0;
                return (T)formatter.Deserialize(stream);
            }
        }
    }
}