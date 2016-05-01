using System.Collections.ObjectModel;
using System.Collections.Specialized;
using TBCG_Card_Generator.DataModels.Enumerations;

namespace TBCG_Card_Generator.DataModels
{
    public class ZoneCard : Card
    {
        public override Enumerations.CardType CardType { get { return CardType.Zone; } }


        ZoneType zoneType;
        ObservableCollection<Rule> rules = new ObservableCollection<Rule>();



        public ZoneCard() {
            rules.CollectionChanged += Rules_CollectionChanged;
        }



        ZoneType ZoneType { get { return zoneType; } set { zoneType = value; OnPropertyChanged("ZoneType"); } }
        ObservableCollection<Rule> Rules {
            get { return rules; }
            set {
                SetChildCollection<Rule, ZoneCard>(value, ref rules, Rules_CollectionChanged, "Rules", (child, newP) => { });
            }
        }




        void Rules_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e) {
            //Rules do not track parents in this build. No need to do anything here.
        }
    }
}
