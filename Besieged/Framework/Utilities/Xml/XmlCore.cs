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
        private static bool IsInstantiated = false;

        private static Dictionary<string, KeyValuePair<Type, XmlSerializer>> _serializationDictionary;

        public static Dictionary<string, KeyValuePair<Type, XmlSerializer>> SerializationDictionary 
        { 
            get
            {
                if (!IsInstantiated)
                {
                    _serializationDictionary = new Dictionary<string, KeyValuePair<Type, XmlSerializer>>();
                    PopulateDictionary();
                }
                return _serializationDictionary;
            }  
        }

        private static void PopulateDictionary()
        {
            if (!IsInstantiated)
            {
                Assembly.GetExecutingAssembly().GetTypes().ToList().Where(x => x.IsClass && x.Namespace == "Framework.Commands").ToList().ForEach(type =>
                {
                    _serializationDictionary.Add(type.Name.ToString(), new KeyValuePair<Type, XmlSerializer>(type, new XmlSerializer(type)));
                });
                IsInstantiated = true;
            }
        }
    }
}
