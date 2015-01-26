using Microsoft.AspNet.SignalR;
using Phoenix.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Hosting;

namespace Phoenix.Game
{
    public class MudGame : World, IRegisteredObject
    {
        private static MudGame _instance;
        private Timer _timer;
        private RoomModule _roomModule;
        private NewAdventurerModule _newAdventurerModule;
        public static MudGame Instance { get { return _instance ?? (_instance = new MudGame()); } }
        public IHubContext Hub { get; private set; }

        public RoomModule RoomModule;
        public Dictionary<Guid, User> Users;
        private bool updating = false;
        public MudGame()
        {
            HostingEnvironment.RegisterObject(this);
            Hub = GlobalHost.ConnectionManager.GetHubContext<GameHub>();
            Users = new Dictionary<Guid, User>();
            _newAdventurerModule = new NewAdventurerModule();
            _roomModule = new RoomModule();
            _roomModule.InitializeRooms();
            _timer = new Timer(OnTimerElapsed, null, TimeSpan.FromSeconds(0), TimeSpan.FromSeconds(.1));
        }

        private void OnTimerElapsed(object state)
        {
            if (updating) return;
            updating = true;
            Update(.1f);
            updating = false;
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
            Hub.Clients.Client(Users.SingleOrDefault(x => x.Value.EntityId == entityId).Key.ToString()).broadcastMessage("Server Msg", msg);
        }

        public void Connect(Guid connectionId, string userName)
        {
            var player = CreatePlayer(connectionId, userName);
            AddEntityToWorld(player);
            Users.Add(connectionId, new User { EntityId = player.Id, Name = userName });
            //BroadcastMessage(userName + " has entered the world.");
            Renderer.Instance.Write(string.Format("|01{0} |02has entered |03the world.", userName));
            var message = new Message(player.Id, 0, 0, "NEW_ADVENTURER", 2);
            message.Value = connectionId;
            PostOffice.Instance.AddMessage(message);
        }

        public Entity CreatePlayer(Guid connectionId, string userName)
        {
            var player = new Entity();
            var playerState = new PlayerComponent
            {
                Owner = player,
                PlayerState = PlayerState.NewAdventurer
            };
            player.AddComponent(playerState);
            return player;
        }

        public void Stop(bool immediate)
        {
            throw new NotImplementedException();
        }
    }
}