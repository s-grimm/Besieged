using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Unit
{
    public interface IUnit
    {
        int X_Position { get; set; }
        int Y_Position { get; set; }
        int CurrentHealthPoints { get; set; }
        int MaxHealthPoints { get; set; }
        int Initiative { get; set; }
        int Skill { get; set; }
        int Range { get; set; }
        int Cost { get; set; }
        int Movement { get; set; }
        string Description { get; set; }
        object GetSprite();
    }
}
