using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Unit
{
    public interface IUnit
    {
        int HealthPoints { get; set; }
        int Initiative { get; set; }
        int Skill { get; set; }
        int Range { get; set; }
        int Cost { get; set; }
        string Description { get; set; }
        //object GetSprite();
    }
}
