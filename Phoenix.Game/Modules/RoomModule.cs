using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Phoenix.Core;

namespace Phoenix.Game
{
    public class RoomModule : Module
    {
        private Dictionary<int, Dictionary<Direction, int>> _exitsToAdd;
        private Dictionary<int, Dictionary<Direction, int>> _exitsToRemove;
        private List<Room> _rooms;

        public RoomModule() : base("ENTITY_MOVE", "SPAWN_ENTITY")
        {
            _rooms = new List<Room>();
            _exitsToAdd = new Dictionary<int, Dictionary<Direction, int>>();
            _exitsToRemove = new Dictionary<int, Dictionary<Direction, int>>();
        }

        public override void HandleMessage(Message message)
        {
            Entity entity;

            switch (message.MessageType)
            {

                case "ENTITY_MOVE":
                    entity = MudGame.Instance.GetEntity(message.Source);
                    if (!entity.OkMessage(message)) return;

                    var room = _rooms.SingleOrDefault(x => x.Entities.Contains(entity));
                    if (room == null) return;

                    foreach (var entityInRoom in room.Entities)
                    {
                        if (entityInRoom == entity) continue;
                        if (!entityInRoom.OkMessage(message)) return;
                    }
                    RemoveEntityFromRoom(entity, room);
                    AddEntityToRoom(entity, (int) message.Value);
                    break;

                case "SPAWN_ENTITY":
                    entity = MudGame.Instance.GetEntity(message.Source);
                    AddEntityToRoom(entity, (int) message.Value);
                    break;
            }
        }


        private void RemoveEntityFromRoom(Entity entity, Room room)
        {
            room.Entities.Remove(entity);
            var message = new Message(entity.Id, 0, 0, "ENTITY_LEFT_ROOM");

        }

        private void AddEntityToRoom(Entity entity, int roomId)
        {
            var room = _rooms.Single(x => x.Id == roomId);
            room.Entities.Add(entity);
            DisplayRoom(entity, room);
            var message = new Message(entity.Id, 0, 0, "ENTITY_ENTERED_ROOM");
            PostOffice.Instance.AddMessage(message);
        }

        public void DisplayRoom(Entity entity, Room room)
        {
            var sb = new StringBuilder();
            sb.AppendFormat("|03{0}<br/>", room.Name);
            sb.AppendFormat("|02{0}<br/>", room.ShortDescription);
            Renderer.Instance.Write(sb.ToString(), true);
        }

        public void InitializeRooms()
        {
            var room1 = new Room
            {
                Name = "Main Room",
                ShortDescription = "This is the starting room for all new adventurers."
            };

            _rooms.Add(room1);
        }


        //    public override void Update(double dt)
        //    {
        //        RoomComponent room;
        //        DescriptionComponent description;
        //        TransformComponent transform;

        //        uint entity;

        //        for (entity = 0; entity < World.EntityCount; ++entity)
        //        {
        //            if ((World.ComponentMask[entity] & RelevantComponents) != RelevantComponents) continue;

        //            description = World.DescriptionComponents[entity];
        //            transform = World.TransformComponents[entity];
        //            room = World.RoomComponents[entity];
        //            HandleInputFromEntities(room);
        //        }
        //    }

        //    private void HandleInputFromEntities(RoomComponent room)
        //    {
        //        InputComponent input;

        //        foreach (var entity in room.Entities)
        //        {
        //            if ((World.ComponentMask[entity] & ComponentType.Input) == 0) continue;

        //            input = World.InputComponents[entity];
        //            if (input.Action == Action.Say)
        //            {
        //                ConsoleRenderer.Instance.MessageQueue.Add(string.Format("|02{0} |01says, \"|08{1}\"", World.DescriptionComponents[entity].Name, string.Join(" ", input.Parameters)));
        //            }
        //        }
        //    }

        //    public List<uint> GetEntitiesInRoom(Point p)
        //    {
        //        var roomId = GetRoom(p);
        //        return World.RoomComponents[roomId].Entities;
        //    }

        //    public void ListEntitiesInRoom(uint room)
        //    {
        //        var transform = World.TransformComponents[room];
        //        var roomComponent = World.RoomComponents[room];
        //        ConsoleRenderer.Instance.MessageQueue.Add(string.Format("|02Entities in Room |06{0}", transform.Position));
        //        ConsoleRenderer.Instance.MessageQueue.Add("|01-=-=-=-=-=-=-=-=-=-=-");
        //        foreach (var entity in roomComponent.Entities)
        //        {
        //            if ((World.ComponentMask[entity] & ComponentType.Description) == 0) continue;
        //            ConsoleRenderer.Instance.MessageQueue.Add(string.Format("|04{0}", World.DescriptionComponents[entity].Name));
        //        }
        //    }


        //    public uint GetRoom(Point p)
        //    {
        //        uint entity;
        //        for (entity = 0; entity < World.EntityCount; ++entity)
        //        {
        //            if ((World.ComponentMask[entity] & RelevantComponents) != RelevantComponents) continue;

        //            var transform = World.TransformComponents[entity];
        //            if (transform.Position == p)
        //                return entity;
        //        }
        //        return World.EntityCount;
        //    }

        //    public Direction? GetExit(uint room, Point location)
        //    {
        //        var roomComponent = World.RoomComponents[room];
        //        if (roomComponent.Exits.ContainsValue(GetRoom(location)))
        //            return roomComponent.Exits.Single(x => x.Value == GetRoom(location)).Key;
        //        return null;
        //    }

        //    public void AddExit(Point location, Direction direction, Point exit, bool bidirectional = true)
        //    {
        //        var room1 = GetRoom(location);
        //        if (room1 == World.EntityCount) return;
        //        var room2 = GetRoom(exit);
        //        if (room2 == World.EntityCount) return;

        //        if (!World.RoomComponents[room1].Exits.ContainsKey(direction))
        //            World.RoomComponents[room1].Exits.Add(direction, room2);
        //        if (!bidirectional) return;

        //        var oppositeDirection = (Direction)((int)direction * -1);

        //        if (!World.RoomComponents[room2].Exits.ContainsKey(oppositeDirection))
        //            World.RoomComponents[room2].Exits.Add(oppositeDirection, room1);
        //    }
        //    public void RemoveExit(Point location, Direction direction, bool bidirectional = true)
        //    {
        //        var room = GetRoom(location);
        //        if (room == World.EntityCount) return;

        //        if (!World.RoomComponents[room].Exits.ContainsKey(direction)) return;

        //        var otherRoom = World.RoomComponents[room].Exits[direction]; 
        //        World.RoomComponents[room].Exits.Remove(direction);

        //        if (!bidirectional) return;

        //        var oppositeDirection = (Direction)((int)direction * -1);
        //        if (World.RoomComponents[otherRoom].Exits.ContainsKey(oppositeDirection))
        //            World.RoomComponents[otherRoom].Exits.Remove(oppositeDirection);
        //    }

        //    private void AddExits(uint room, Direction direction, uint exit)
        //    {
        //        var roomComponent = World.RoomComponents[room];
        //        if (!roomComponent.Exits.ContainsKey(direction))
        //            roomComponent.Exits.Add(direction, exit);
        //    }
        //    private void RemoveExits(uint room, Direction direction)
        //    {
        //        var roomComponent = World.RoomComponents[room];
        //        if (!roomComponent.Exits.ContainsKey(direction))
        //            roomComponent.Exits.Remove(direction);
        //    }
        //}
    }
}
