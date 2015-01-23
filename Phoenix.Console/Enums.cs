using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.Console
{
    [Flags]
    public enum Mask
    {
        Hands = 1 << 0,
        Move = 1 << 1,
        Eyes = 1 << 2,
        Mouth = 1 << 3,
        Sound = 1 << 4,
        Always = 1 << 5,
        Magic = 1 << 6,
        Delicate = 1 << 7,
        Malicious = 1 << 8,
        Channel = 1 << 9,
        Optimize = 1 << 10,
        ControlMsg = 1 << 11,
        InterMsg = 1 << 12
    }

    public enum Typ
    {
        AreaAffect = 1,
        Push,
        Pull,
        Recall,
        Open,
        Close,
        Put,
        Get,
        Unlock,
        Lock,
        Wield,
        Give,
        Buy,
        Sell,
        Drop,
        Wear,
        Fill,
        DelicateHandsAct,
        Value,
        Hold,
        NoisyMovement,
        QuietMovement,
        WeaponAttack,
        Look,
        Read,
        Noise,
        Speak,
        CastSpell,
        List,
        Eat,
        Enter,
        Follow,
        Leave,
        Sleep,
        Sit,
        Stand,
        Flee,
        NoFollow,
        Write,
        Fire,
        Cold,
        Water,
        Gas,
        Mind,
        General,
        Justice,
        Acid,
        Electric,
        Poison,
        Undead,
        Dismount,
        OkAction,
        OkVisual,
        Drink,
        Hands
    }

    public enum Msg
    {
        NoEffect,
        AreaAffect = Mask.Always | Typ.AreaAffect,
        Push = Mask.Hands | Typ.Push,
        Pull = Mask.Hands | Typ.Pull,
    }

    public enum Direction
    {
        North,
        South,
        East,
        West,
    }
}
