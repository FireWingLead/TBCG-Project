using System;

namespace TBCG_Card_Generator.DataModels.Enumerations
{
    public enum CardType
    {
        Unit, Zone
    }

    public enum ZoneRealm
    {
        Ground = 0, Air = 0x1, Space = 0x2
    }

    [Flags]
    public enum ZoneProperties
    {
        Open = 0,
        MovementRestrictive = ZoneRealm.Space * 2,//This MUST be 2 * the highest ZoneRealm value rounded down to the nearest power of 2, or ZoneType.GetHashCode() will get screwed up.
        VisionRestrictive = MovementRestrictive * 2,
        Terminal = VisionRestrictive * 2
    }
}