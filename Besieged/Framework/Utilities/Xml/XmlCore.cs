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

            Assembly.GetExecutingAssembly().GetTypes().ToList().Where(x => x.IsClass && x.Namespace.Contains("Framework")).ToList().ForEach(type =>
            {
                try
                {
                    XmlSerializer xmlSerializer = new XmlSerializer(type);
                    FrameworkTypes.Add(type);
                    SerializerDictionary.Add(type.Name.ToString(), new XmlSerializer(type));
                }
                catch (Exception)
                {
                    // type couldn't be serialized
                }
            });

            //Assembly.GetExecutingAssembly().GetTypes().ToList().Where(x => x.IsClass).ToList().ForEach(type =>
            //{
            //    //try
            //    //{
            //    //    XmlSerializer xmlSerializer = new XmlSerializer(type);
            //    //    AllKnownTypes.Add(type);
            //    //}
            //    //catch (Exception)
            //    //{
            //    //    // type couldn't be serialized
            //    //}
            //});
            
            //AppDomain.CurrentDomain.AssemblyLoad += (sender, args) =>   // Add any newly loaded types into the List of all known types
            //{
            //    args.LoadedAssembly.GetTypes().ToList().Where(x => x.IsClass && x.IsSerializable).ToList().ForEach(type =>
            //    {
            //        try
            //        {
            //            XmlSerializer xmlSerializer = new XmlSerializer(type);
            //            AllKnownTypes.Add(type);
            //        }
            //        catch (Exception)
            //        {
            //            // type couldn't be serialized
            //        }
            //    });
            //};
        }

        public static void Start()
        {
            Console.WriteLine("Preheating XmlCore");
        }

        public static XmlSerializer GetFrameworkFallbackSerializer(Type type)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(type, FrameworkTypes.ToArray());
            SerializerDictionary[type.Name] = xmlSerializer;
            return xmlSerializer;
        }

        public static XmlSerializer GetFrameworkFallbackSerializer(string typeName)
        {
            Type type = FrameworkTypes.Where(x => x.Name == typeName).FirstOrDefault();
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
