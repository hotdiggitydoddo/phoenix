using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using Phoenix.Core;

namespace Phoenix.Game
{
    public class Room
    {
        private static int _nextId = 1;
        private List<Entity> _entities; 
        public int Id { get; private set; }
        public string Name { get; protected set; }
        public string ShortDescription { get; protected set; }

        public Room()
        {
            Id = _nextId;
            _nextId++;
            _entities = new List<Entity>();
        }


        //public Exit GetExit(Direction direction)
        //{
        //    return Exits.SingleOrDefault(x => x.Direction == direction);
        //}
    }

    public class Exit
    {
        public Direction Direction { get; set; }
        public int RoomId { get; set; }
    }

    public enum Direction
    {
        North,
        South,
        East,
        West,
        Northwest,
        Southwest,
        Northeast,
        Southeast,
        Up,
        Down
    }
}
