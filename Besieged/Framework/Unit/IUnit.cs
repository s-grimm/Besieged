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
        int MaxHealthPoints { get; }
        int Initiative { get; }
        int Skill { get; }
        int Range { get; }
        int Cost { get;  }
        int Movement { get;  }
        string Description { get; }
        string Owner { get; set; }
        int MovementLeft { get; set; }
        object GetSprite();
    }
}
