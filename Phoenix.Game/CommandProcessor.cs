using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Phoenix.Core;

namespace Phoenix.Game
{
    public class CommandProcessor
    {
        private bool _done;

        public void HandleInput(string name, string message)
        {
            var parts = message.Trim().Split(' ');
            var user = MudGame.Instance.Users.Single(x => x.Value.Name == name);

            switch (parts[0].ToLower())
            {
                //case "n":
                //    input.Action = Action.MoveNorth;
                //    break;
                //case "s":
                //    input.Action = Action.MoveSouth;
                //    break;
                //case "e":
                //    input.Action = Action.MoveEast;
                //    break;
                //case "w":
                //    input.Action = Action.MoveWest;
                //    break;
                //case "?":
                //    input.Action = Action.Help;
                //    break;
                //case "c":
                //    input.Action = Action.CreateElf;
                //    break;
                case "say":
                    break;
                //case "pickup":
                //    input.Action = Action.Pickup;
                //    break;
                //case "attack":
                //    input.Action = Action.Attack;
                //    break;
                //case "cast":
                //    if ((World.ComponentMask[entity] & ComponentType.Spellcaster) == 0)
                //    {
                //        ConsoleRenderer.Instance.MessageQueue.Add("|03You do not possess the knowledge of spellcasting.");
                //        break;
                //    }
                //    if (parts.Length == 1)
                //    {
                //        ConsoleRenderer.Instance.MessageQueue.Add("|03You must provide a spell to cast!");
                //        break;
                //    }
                //    input.Action = Action.Cast;
                //    break;
            }

            //if (parts.Length > 1)
            //    input.Parameters = parts.Skip(1).ToList();
            //keyboardControl.Input = string.Empty;
        }

        internal void HandleInput(Guid guid, string message)
        {
            var entity = MudGame.Instance.GetEntity(MudGame.Instance.Users[guid].EntityId);

            if (entity.GetComponent<PlayerComponent>().PlayerState == PlayerState.Prompt)
            {
                PromptModule.Instance.HandlePrompt(entity.Id, message);
            }

        }
    }

}
