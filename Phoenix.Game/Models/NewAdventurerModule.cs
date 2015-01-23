﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;
using Phoenix.Core;

namespace Phoenix.Game
{
    public class NewAdventurerModule : Module
    {
        public NewAdventurerModule() : base("ADDED_ENTITY") { }

        //public override void OnEntityAdded(Entity entity)
        //{
        //    var msg = new Message(0, entity.Id, 0, "SAY", .1f) { Value = "|00Welcome to the world, new player!" };
        //    PostOffice.Instance.AddMessage(msg);
        //    var msg2 = new Message(0, entity.Id, 0, "SAY", 2.4f)
        //    {
        //        Value = "|05What would you like to be called?"
        //    };
        //    PostOffice.Instance.AddMessage(msg2);
        //    var player = entity.GetComponent<PlayerComponent>();
        //    player.PlayerState = PlayerState.Prompt;
        //    var newAdventurer = new NewAdventurerComponent { Owner = entity };
        //    entity.AddComponent(newAdventurer);
        //    PromptModule.Instance.RegisterPrompt(entity.Id, ChooseGender);
        //}

        public override void HandleMessage(Message message)
        {
            var entity = MudGame.Instance.GetEntity(message.Source);
            if (!entity.HasComponent<PlayerComponent>()) return;
            
            var msg = new Message(0, entity.Id, 0, "SAY", .1f) { Value = "|00Welcome to the world, new player!" };
            PostOffice.Instance.AddMessage(msg);
            var msg2 = new Message(0, entity.Id, 0, "SAY", 2.4f)
            {
                Value = "|05What would you like to be called?"
            };
            PostOffice.Instance.AddMessage(msg2);
            var player = entity.GetComponent<PlayerComponent>();
            player.PlayerState = PlayerState.Prompt;
            var newAdventurer = new NewAdventurerComponent { Owner = entity };
            entity.AddComponent(newAdventurer);
            PromptModule.Instance.RegisterPrompt(message.Source, ChooseGender);

        }

        private void ChooseGender(int entityId, string message)
        {
            var player = MudGame.Instance.GetEntity(entityId);
            var newAdventurer = player.GetComponent<NewAdventurerComponent>();
            var msg = new Message(0, player.Id, 0, "SAY", 1.5f);

            msg.Value = string.Format("|03{0} is a fine choice.", message);
            PostOffice.Instance.AddMessage(msg);
            newAdventurer.Name = message;
            var nextStep = new Message(0, player.Id, 0, "SAY", 3.5f);

            nextStep.Value = "|04Are you |05(m)|04ale or |05(f)|04emale?";
            PostOffice.Instance.AddMessage(nextStep);
            PromptModule.Instance.UnregisterPrompt(entityId);
            PromptModule.Instance.RegisterPrompt(entityId, ChooseRace);

        }

        private void ChooseRace(int entityId, string message)
        {
            var player = MudGame.Instance.GetEntity(entityId);
            var newAdventurer = player.GetComponent<NewAdventurerComponent>();

            if (message == "m" || message == "male")
            {
                var msg = new Message(0, player.Id, 0, "SAY", 1.5f);
                msg.Value = string.Format("|02{0}, you have chosen the stronger of the species!", newAdventurer.Name);
                PostOffice.Instance.AddMessage(msg);
                newAdventurer.Gender = Gender.Male;
            }
            else if (message == "f" || message == "female")
            {
                var msg = new Message(0, player.Id, 0, "SAY", 1.5f);
                msg.Value = string.Format("|02{0}, you have chosen the prettier of the species!", newAdventurer.Name);
                PostOffice.Instance.AddMessage(msg);
                newAdventurer.Gender = Gender.Female;
            }

            var nextStep = new Message(0, player.Id, 0, "SAY", 3.5f);
                
            nextStep.Value = "|08Choose a race";
            PostOffice.Instance.AddMessage(nextStep);
            PromptModule.Instance.UnregisterPrompt(entityId);
            PromptModule.Instance.RegisterPrompt(entityId, ChooseClass);
        }

        void ChooseClass(int entityId, string message)
        {
            
        }
    }
}
