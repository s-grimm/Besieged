using Framework.Commands;
using Framework.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BesiegedClient
{
    public static class GlobalResources
    {
        public static ObservableCollection<CommandNotifyGame> GameLobbyCollection { get; set; }
        public static ObservableCollection<string> GameSpecificChatMessages { get; set; }
        public static Canvas GameWindow { get; set; }
        public static IBesiegedServer BesiegedServer { get; set; }
        public static string ClientId { get; set; }
        public static string GameId { get; set; }
        
        static GlobalResources()
        {
            GameLobbyCollection = new ObservableCollection<CommandNotifyGame>();
            GameSpecificChatMessages = new ObservableCollection<string>();
        }

        public static void SendMessageToServer(string command)
        {
            Task.Factory.StartNew(() => BesiegedServer.SendCommand(command));
        }
    }
}
