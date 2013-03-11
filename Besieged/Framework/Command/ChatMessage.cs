using System;
using System.Runtime.Serialization;

namespace Framework.Command
{
    public class ChatMessage : ICommand
    {
        public override Object Value { get; set; }
    }
}