using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.Console
{
    class Room
    {
        private static int _nextId = 1;
        protected List<Exit> Exits;
        public int Id { get; private set; }
        public string Name { get; protected set; }
        public string ShortDescription { get; protected set; }

        public Room()
        {
            Id = _nextId;
            Exits = new List<Exit>();
            _nextId++;
        }

        //public virtual bool OkMessage(IEntity source, Message message)
        //{
        //    foreach (var mob in Mobs)
        //    {
        //        if (!mob.OkMessage(source, message))
        //            return false;
        //    }
        //    foreach (var item in Items)
        //    {
        //        if (!item.OkMessage(source, message))
        //            return false;
        //    }
        //}

        public Exit GetExit(Direction direction)
        {
            return Exits.SingleOrDefault(x => x.Direction == direction);
        }
    }
}
