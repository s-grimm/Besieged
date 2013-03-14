using Framework.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;


namespace Framework.Utilities.Xml
{
    public static class XmlCore
    {
        public static Dictionary<string, KeyValuePair<Type, XmlSerializer>> SerializationDictionary { get; set; }
        private static Type[] CommandTypes;

        static XmlCore()
        {
            SerializationDictionary = new Dictionary<string, KeyValuePair<Type, XmlSerializer>>();
            CommandTypes = Assembly.GetExecutingAssembly().GetTypes().ToList().Where(x => x.IsClass && x.Namespace == "Framework.Commands").ToArray();
            
            CommandTypes.ToList().ForEach(type =>
            {
                if (type == typeof(CommandAggregate))
                {
                    SerializationDictionary.Add(type.Name.ToString(), new KeyValuePair<Type, XmlSerializer>(type, new XmlSerializer(type, CommandTypes)));
                }
                else
                {
                    SerializationDictionary.Add(type.Name.ToString(), new KeyValuePair<Type, XmlSerializer>(type, new XmlSerializer(type)));
                }
            });
        }
    }
}
