using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Framework.Unit
{
    public class Army
    {
        public enum ArmyTypeEnum
        {
            [XmlEnum(Name = "Undead")]
            Undead,
            [XmlEnum(Name = "Alliance")]
            Alliance,
            [XmlEnum(Name = "Beast")]
            Beast,            
            None
        };

    }
}
