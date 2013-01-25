using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FGJ2013
{
    public enum GameEvents
    {
        None = 0,
        PlayerStop,
        PlayerMove,
        PlayerDeath,
        DoorOpen,
    }
    public static class GameEventHandler
    {
        public static GameEvents currentEvent = GameEvents.None;        
    }
}
