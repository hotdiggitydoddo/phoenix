using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Phoenix.Core;

namespace Phoenix.Console
{
    class HealthModule : Module
    {
        public HealthModule() : base("DAMAGE_ENTITY", "HEAL_ENTITY") { }

        public override void HandleMessage(Message message)
        {
            switch (message.MessageType)
            {
                case "DAMAGE_ENTITY" :
                    var entityToDamage = Game.Instance.GetEntity(message.Target);
                    var amount = Math.Abs((int) message.Value);
                    var health = entityToDamage.GetComponent<HealthComponent>();
                    health.CurrentHealth -= amount;
                    if (!health.IsAlive)
                    {
                        var deathMessage = new Message(message.Source, message.Target, message.Tool, "ENTITY_KILLED");
                        PostOffice.Instance.AddMessage(deathMessage);
                        Game.Instance.RemoveEntityFromWorld(entityToDamage);
                    }
                    break;
                case "HEAL_ENTITY":
                    var entityToHeal = Game.Instance.GetEntity(message.Target);
                    var healAmount = Math.Abs((int)message.Value);
                    var healthComp = entityToHeal.GetComponent<HealthComponent>();
                    healthComp.CurrentHealth += healAmount;
                    break;
            }
        }
    }
}
