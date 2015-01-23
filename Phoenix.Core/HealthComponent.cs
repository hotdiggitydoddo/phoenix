using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.Core
{
    public class HealthComponent : Component
    {
        /// <summary>Initializes a new instance of the <see cref="HealthComponent"/> class.</summary>
        public HealthComponent()
            : this(0)
        {
        }

        /// <summary>Initializes a new instance of the <see cref="HealthComponent"/> class.</summary>
        /// <param name="hp">The points.</param>
        public HealthComponent(int hp)
        {
            _currentHealth = MaximumHealth = hp;
        }


        private int _currentHealth;

        /// <summary>Gets or sets the health points.</summary>
        /// <value>The Points.</value>
        public int CurrentHealth
        {
            get
            {
                return _currentHealth;
            }
            set
            {
                _currentHealth =  value > MaximumHealth ? MaximumHealth : value;
            } 
        }

        /// <summary>Gets the health percentage.</summary>
        /// <value>The health percentage.</value>
        public double HealthPercentage
        {
            get { return Math.Round((float)_currentHealth / MaximumHealth * 100f); }
        }

        /// <summary>Gets a value indicating whether is alive.</summary>
        /// <value><see langword="true" /> if this instance is alive; otherwise, <see langword="false" />.</value>
        public bool IsAlive
        {
            get { return _currentHealth > 0; }
        }

        /// <summary>Gets the maximum health.</summary>
        /// <value>The maximum health.</value>
        public int MaximumHealth { get; set; }
    }
}