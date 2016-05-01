using System;
using System.Linq;

using TBCG_Card_Generator.DataModels.Enumerations;

namespace TBCG_Card_Generator.DataModels
{
    public sealed struct ZoneType
    {
        static readonly int REALM_MASK = (int)((ZoneRealm[])Enum.GetValues(typeof(ZoneRealm))).Aggregate((lVal, rVal) => lVal | rVal);
        static readonly int PROPS_MASK = (int)((ZoneProperties[])Enum.GetValues(typeof(ZoneProperties))).Aggregate((lval, rVal) => lval | rVal);


        public ZoneType() : this(default(ZoneRealm), default(ZoneProperties)) { }
        public ZoneType(ZoneRealm realm, ZoneProperties properties) {
            Realm = realm;
            Properties = properties;
        }


        public ZoneRealm Realm { get; private set; }
        public ZoneProperties Properties { get; private set; }



        public override bool Equals(object obj) {
            if (obj is ZoneType)
                return Realm == ((ZoneType)obj).Realm && Properties == ((ZoneType)obj).Properties;
            else if (obj is int || obj is long || obj is short || obj is byte)
                return Equals(((ZoneType)(int)obj));
            return false;
        }
        public override int GetHashCode() { return (int)Realm | (int)Properties; }
        public override string ToString() { return string.Format("{{{0}, {1}}}", Realm.ToString(), Properties.ToString()); }



        public static bool operator ==(ZoneType lVal, ZoneType rVal) { return lVal.Equals(rVal); }

        public static explicit operator int(ZoneType val) { return val.GetHashCode(); }
        public static explicit operator string(ZoneType val) { return val.ToString(); }

        public static explicit operator ZoneType(int val) { return new ZoneType((ZoneRealm)(val & REALM_MASK), (ZoneProperties)(val & PROPS_MASK)); }
    }
}
