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
        public static Dictionary<string, XmlSerializer> SerializerDictionary { get; set; }
        private static List<Type> FrameworkTypes;
        private static List<Type> AllKnownTypes;

        static XmlCore()
        {
            FrameworkTypes = new List<Type>();
            AllKnownTypes = new List<Type>();
            SerializerDictionary = new Dictionary<string, XmlSerializer>();
            FrameworkTypes = Assembly.GetExecutingAssembly().GetTypes().ToList().Where(x => x.IsClass && x.Namespace.Contains("Framework")).ToList();
            AllKnownTypes = Assembly.GetExecutingAssembly().GetTypes().ToList();
            
            FrameworkTypes.ToList().ForEach(type =>
            {
                SerializerDictionary.Add(type.Name.ToString(), new XmlSerializer(type));
            });

            AppDomain.CurrentDomain.AssemblyLoad += (sender, args) =>   // Add any newly loaded types into the List of all known types
            {
                args.LoadedAssembly.GetTypes().ToList().ForEach(x => AllKnownTypes.Add(x));
            };

            //hard coded in here for Map Overrides
            if (!SerializerDictionary.ContainsKey(typeof(Framework.Map.Tile.ITile).Name.ToString()))
            {
                SerializerDictionary.Add(typeof(Framework.Map.Tile.ITile).Name.ToString(), null);
            }
            SerializerDictionary[typeof(Framework.Map.Tile.ITile).Name.ToString()] = new XmlSerializer(typeof(Framework.Map.Tile.ITile), Assembly.GetExecutingAssembly().GetTypes().ToList().Where(x => x.IsClass && x.Namespace == "Framework.Map.Tile").ToArray());
        }

        public static XmlSerializer GetFrameworkFallbackSerializer(Type type)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(type, FrameworkTypes.ToArray());
            SerializerDictionary[type.Name] = xmlSerializer;
            return xmlSerializer;
        }

        public static XmlSerializer GetUberFallbackSerializer(Type type)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(type, AllKnownTypes.ToArray());
            SerializerDictionary[type.Name] = xmlSerializer;
            return xmlSerializer;
        }
    }
}
