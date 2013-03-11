using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Command.Server
{
    public class Connect : ICommand
    {
        public Object Value { get; set; }
    }
}
