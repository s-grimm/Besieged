using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Command
{
    public class ChatMessage : ICommand
    {
        public string Message { get; set; }

        public void execute()
        {
            throw new NotImplementedException();
        }
    }
}
