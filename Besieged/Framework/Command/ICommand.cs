using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Command
{
    public interface ICommand
    {
        Object Value { get; set; }
    }
}
