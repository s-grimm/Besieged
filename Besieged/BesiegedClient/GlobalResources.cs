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
        public static ObservableCollection<string> GameSpecificChatMessages { get; set; }
        public static Canvas GameWindow { get; set; }
        public static IBesiegedServer BesiegedServer { get; set; }
        public static string ClientId { get; set; }
        public static string GameId { get; set; }
        public static bool m_IsServerConnectionEstablished = false;
        public static TaskScheduler m_TaskScheduler = TaskScheduler.FromCurrentSynchronizationContext();

        public enum MenuState { Menu, Loading };
        private static MenuState m_currentMenuState = MenuState.Menu;

        public static EventHandler MenuStateChanged;

        public static MenuState currentMenuState
        {
            get { return m_currentMenuState; }
            set { m_currentMenuState = value;
            //event handler
            if (MenuStateChanged != null)
                MenuStateChanged(null, new EventArgs());
            }
        }

        static GlobalResources()
        {
            GameSpecificChatMessages = new ObservableCollection<string>();
        }

        public static void SendMessageToServer(string command)
        {
            Task.Factory.StartNew(() => BesiegedServer.SendMessage(command));
        }
    }
}
