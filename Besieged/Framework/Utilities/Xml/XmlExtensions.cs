using Framework.Commands;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Framework.Utilities.Xml
{
    public static class XmlExtensions
    {
        /// <summary>Serializes an object of type T in to an xml string</summary>
        /// <typeparam name="T">Any class type</typeparam>
        /// <param name="obj">Object to serialize</param>
        /// <returns>A string that represents Xml, empty otherwise</returns>
        public static string ToXml<T>(this T obj) where T : class, new()
        {
            if (obj == null) throw new ArgumentNullException("obj");

            XmlSerializer xmlSerializer;
            string typeName = typeof(T).Name;
            if (XmlCore.SerializerDictionary.ContainsKey(typeName))
            {
                xmlSerializer = XmlCore.SerializerDictionary[typeName];
            }
            else
            {
                xmlSerializer = new XmlSerializer(typeof(T));
                XmlCore.SerializerDictionary.Add(typeName, xmlSerializer);
            }

            try
            {
                using (StringWriter writer = new StringWriter())
                {
                    xmlSerializer.Serialize(writer, obj);
                    return writer.ToString();
                }
            }
            catch (Exception)   // try using a serializer that also knows about all other framework types
            {
                try
                {
                    xmlSerializer = XmlCore.GetFrameworkFallbackSerializer(typeof(T));
                    using (StringWriter writer = new StringWriter())
                    {
                        xmlSerializer.Serialize(writer, obj);
                        return writer.ToString();
                    }
                }
                catch (Exception)
                {
                    xmlSerializer = XmlCore.GetUberFallbackSerializer(typeof(T));   // try using a serializer that know about ALL types - this better freaking work
                    using (StringWriter writer = new StringWriter())
                    {
                        xmlSerializer.Serialize(writer, obj);
                        return writer.ToString();
                    }
                }
            }
        }

        /// <summary>Deserializes an xml string in to an object of Type T</summary>
        /// <typeparam name="T">Any class type</typeparam>
        /// <param name="xml">Xml as string to deserialize from</param>
        /// <returns>A new object of type T is successful, null if failed</returns>
        public static T FromXml<T>(this string xml) where T : class, new()
        {
            Type rootType = typeof(T);
            try
            {
                XDocument xDocument = XDocument.Parse(xml);
                string actualRootType = xDocument.Root.Name.ToString();
                if (XmlCore.SerializerDictionary.ContainsKey(actualRootType))
                {
                    using (StringReader stringReader = new StringReader(xml))
                    {
                        try 
                        {
                            return (T)XmlCore.SerializerDictionary[actualRootType].Deserialize(stringReader); 
                        }
                        catch (Exception)
                        { 
                            return null; 
                        }
                    }
                }
            }
            catch (Exception)
            {
                return null;
            }
            return null;
        }
    }
}
