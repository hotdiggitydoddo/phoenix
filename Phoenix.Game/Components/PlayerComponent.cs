using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Phoenix.Core;

namespace Phoenix.Game
{
    public class PlayerComponent : Component
    {
        public PlayerState PlayerState { get; set; }
        public Guid ConnectionId { get; set; }
    }

    public enum PlayerState
    {
        LoggingIn,
        NewAdventurer,
        Prompt,
        Playing
    }

}