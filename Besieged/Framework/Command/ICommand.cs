using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Command
{
    public abstract class ICommand
    {
        public abstract Object Value { get; set; }
    }
}
