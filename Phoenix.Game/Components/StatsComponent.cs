using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Phoenix.Core;

namespace Phoenix.Game
{
    public class StatsComponent : Component
    {
        public byte Strength { get; set; }
        public byte Intellect { get; set; }
        public byte Agility { get; set; }
    }
}