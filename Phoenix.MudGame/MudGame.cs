using Microsoft.AspNet.SignalR;
using Phoenix.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Hosting;

namespace Phoenix.MudGame
{
    public class MudGame : World, IRegisteredObject
    {
        private static MudGame _instance = new MudGame();
        public static MudGame Instance { get { return _instance ?? (_instance = new MudGame()); } }
        public List<Room> Rooms { get; set; }
        public IHubContext Hub { get; private set; }
        private Dictionary<Guid, User> _users; 
        public MudGame()
        {
            HostingEnvironment.RegisterObject(this);
            Rooms = new List<Room>();
            Hub = GlobalHost.ConnectionManager.GetHubContext<GameHub>();
            _users = new Dictionary<Guid, User>();
        }

        public void BroadcastMessage(string msg)
        {
            Hub.Clients.All.broadcastMessage("SERVER MSG", msg);
        }

        public void BroadcastToRoom(string roomName, string msg)
        {
            Hub.Clients.Group(roomName).broadcastMessage("Server msg", msg);
        }

        public void BroadcastToUser(int entityId, string msg)
        {
            Hub.Clients.Client(_users.SingleOrDefault(x => x.Value.EntityId == entityId).Key.ToString()).broadcastMessage("Server Msg", msg);
        }

        public void Connect(Guid connectionId, string userName)
        {
            _users.Add(connectionId, new User { EntityId = new Entity().Id, Name = userName });
            BroadcastMessage(userName + " has entered the world.");
        }

        public void Stop(bool immediate)
        {
            throw new NotImplementedException();
        }
    }
}