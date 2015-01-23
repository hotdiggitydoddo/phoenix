using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace Phoenix.Game
{
    public class GameHub : Hub
    {
        private CommandProcessor commandProcessor = new CommandProcessor();
        public void Send(string name, string message)
        {
            if (message.Contains("|Connect"))
            {
                MudGame.Instance.Connect(Guid.Parse(Context.ConnectionId), name);
                return;
            }

            commandProcessor.HandleInput(Guid.Parse(Context.ConnectionId), message);
           // commandProcessor.HandleInput(name, message);
            //MudGame.Instance.BroadcastToRoom(MudGame.Instance._roomProcessor.GetCuurentRoom(Game.Instance.Users.SingleOrDefault(x => x.Key == Guid.Parse(Context.ConnectionId)).Value.EntityId).Name, message);
            Clients.All.broadcastMessage(name, message);
            //Clients.All.broadcastMessage("Random number between 1-100 from Game: ", Game.Instance.GetRandom().ToString());

            message = message.ToLower().Trim();
            //if (message.StartsWith("/"))
              //  Game.Instance.CommandProcessor.ProcessCommand(Context.ConnectionId, message);
        }

        public override Task OnConnected()
        {
            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            MudGame.Instance.Users.Remove(Guid.Parse(Context.ConnectionId));
            Clients.All.broadcastMessage("Someone has disconnected");
            return base.OnDisconnected(stopCalled);
        }
    }
}