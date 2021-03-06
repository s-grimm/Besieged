﻿using Framework;
using Framework.ServiceContracts;
using Framework.Unit;
using Framework.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BesiegedServer
{
    public class Player : ConnectedClient
    {
        public PlayerColor.PlayerColorEnum PlayerColor { get; set; }
        public MonitoredValue<bool> IsReady { get; set; }
        public Army.ArmyTypeEnum ArmyType { get; set; }

        public Player (string name, string clientId, IClient callback, PlayerColor.PlayerColorEnum color) : base (name, clientId, callback)
        {
            PlayerColor = color;
            IsReady = new MonitoredValue<bool>(false);
        }
    }
}
