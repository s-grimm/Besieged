using Framework.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BesiegedClient
{
    public static class GlobalVariables
    {
        public static ObservableCollection<CommandNotifyGame> GameLobbyCollection { get; set; }
        public static Canvas GameWindow { get; set; }
        
        
        static GlobalVariables()
        {
            GameLobbyCollection = new ObservableCollection<CommandNotifyGame>();
        }

    }
}
