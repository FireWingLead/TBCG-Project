using System.Collections.ObjectModel;
using System.Collections.Specialized;

using TBCG_Card_Generator.DataModels.Enumerations;

namespace TBCG_Card_Generator.DataModels
{
    public class UnitCard : Card
    {
        public override CardType CardType { get { return CardType.Unit; } }


        int deploymentCost;
        int power;
        int visionRange;
        int unitSize;
        ObservableCollection<UnitComponent> unitComponents = new ObservableCollection<UnitComponent>();
        ObservableCollection<Rule> rules = new ObservableCollection<Rule>();


        public UnitCard() {
            unitComponents.CollectionChanged += UnitComponents_CollectionChanged;
            rules.CollectionChanged += Rules_CollectionChanged;
        }



        public int DeploymentCost { get { return deploymentCost; } set { deploymentCost = value; OnPropertyChanged("DeploymentCost"); } }
        public int Power { get { return power; } set { power = value; OnPropertyChanged("Power"); } }
        public int VisionRange { get { return visionRange; } set { visionRange = value; OnPropertyChanged("VisionRange"); } }
        public int UnitSize { get { return unitSize; } set { unitSize = value; OnPropertyChanged("UnitSize"); } }
        public ObservableCollection<UnitComponent> UnitComponents {
            get { return unitComponents; }
            set {
                SetChildCollection<UnitComponent, UnitCard>(value, ref unitComponents, UnitComponents_CollectionChanged, "UnitComponents", (child, newP) => { child.UpdateUnitCard(newP); });
            }
        }
        public ObservableCollection<Rule> Rules {
            get { return rules; }
            set {
                SetChildCollection<Rule, UnitCard>(value, ref rules, Rules_CollectionChanged, "Rules", (child, newP) => { });
            }
        }



        void UnitComponents_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e) {
            OnChildCollectionChanged<UnitComponent, UnitCard>(e, (child, newParent) => { child.UpdateUnitCard(newParent); });
        }
        void Rules_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e) {
            //Rules do not track their parents in this build. We don't need to do anything here.
        }
    }
}
