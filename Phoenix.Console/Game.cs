using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Phoenix.Core;

namespace Phoenix.Console
{
    class Game : World
    {
        private static Game _instance = new Game();
        public static Game Instance { get { return _instance ?? (_instance = new Game()); } }

        public List<Room> Rooms { get; set; }
        

        public Game()
        {
            Rooms = new List<Room>();
        }
    }
}
