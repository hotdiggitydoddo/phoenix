using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR.Messaging;

namespace Phoenix.Game
{
    public class PromptModule
    {
        private static PromptModule _instance = new PromptModule();
        public static PromptModule Instance { get { return _instance ?? (_instance = new PromptModule()); } }
        
        private Dictionary<int, Action<int, string>> _actions;

        public PromptModule()
        {
            _actions = new Dictionary<int, Action<int, string>>();
        }

        public void RegisterPrompt(int entityId, Action<int, string> action)
        {
            _actions.Add(entityId, action);
        }

        public void UnregisterPrompt(int entityId)
        {
            _actions.Remove(entityId);
        }

        public void HandlePrompt(int entityId, string message)
        {
            if (_actions.ContainsKey(entityId))
                _actions[entityId](entityId, message);
        }
    }
}