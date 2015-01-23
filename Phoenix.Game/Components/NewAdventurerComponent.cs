using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Phoenix.Core;

namespace Phoenix.Game
{
    public class NewAdventurerComponent : Component
    {
        public string Name { get; set; }
        public Gender Gender { get; set; }
        public Race Race { get; set; }
        public Class Class { get; set; }
    }

    public enum Gender
    {
        Male = 1,
        Female
    }

    public enum Race
    {
        Human = 1,
        Elf,
        Dwarf,
        Gnome
    }

    public enum Class
    {
        Mage = 1,
        Warrior,
        Paladin,
        Cleric,
    }

    //public abstract class Race
    //{
    //    public byte StrModifier { get; protected set; }
    //    public byte IntModifier { get; protected set; }
    //    public byte AgiModifier { get; protected set; }
    //    public string Name { get; protected set; }
    //    public string Description { get; set; }
    //}

    //public abstract class CharacterClass
    //{
        
    //}
}