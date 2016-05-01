using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace TBCG_Card_Generator.DataModels
{
    public class UnitComponent : ObservableParentObject
    {
        string name;
        UnitCard unitCard;
        UnitComponent parentComponent;
        ObservableCollection<UnitComponent> subComponents = new ObservableCollection<UnitComponent>();



        public UnitComponent() {
            subComponents.CollectionChanged += SubComponents_CollectionChanged;
        }



        public string Name { get { return name; } set { name = value; OnPropertyChanged("Name"); } }
        public UnitCard UnitCard {
            get { return unitCard; }
            set {
                if (unitCard == value) return;
                if (IsSubComponent)
                    parentComponent.SubComponents.Remove(this);
                else if (unitCard != null)
                    unitCard.UnitComponents.Remove(this);
                value.UnitComponents.Add(this);
            }
        }
        public UnitComponent ParentComponent {
            get { return parentComponent; }
            set {
                if (parentComponent == value) return;
                if (IsSubComponent) parentComponent.SubComponents.Remove(this);
                if (value == null) {
                    if (unitCard != null)//This component has been made a root component of its card.
                        unitCard.UnitComponents.Add(this);
                }
                if (value == null) return;
                value.SubComponents.Add(this);
            }
        }
        public ObservableCollection<UnitComponent> SubComponents {
            get { return subComponents; }
            set {
                SetChildCollection<UnitComponent, UnitComponent>(value, ref subComponents, SubComponents_CollectionChanged, "SubComponents", (child, newP) => { child.UpdateParentComponent(newP); });
            }
        }



        public bool IsRootComponent { get { return parentComponent == null; } }
        public bool IsSubComponent { get { return parentComponent != null; } }



        internal void UpdateUnitCard(UnitCard parent) {
            if (parent == unitCard) return;
            unitCard = parent;
            OnPropertyChanged("UnitCard");
        }
        internal void UpdateParentComponent(UnitComponent parent) {
            if (parent == parentComponent) return;
            bool wasRoot = IsRootComponent;
            if (wasRoot && unitCard != null)
                unitCard.UnitComponents.Remove(this);
            UpdateUnitCard(parent == null ? null : parent.unitCard);
            parentComponent = parent;
            OnPropertyChanged("ParentComponent");
            if (wasRoot ^ IsRootComponent){
                OnPropertyChanged("IsRootComponent");
                OnPropertyChanged("IsSubComponent");
            }
        }




        void SubComponents_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e) {
            OnChildCollectionChanged<UnitComponent, UnitComponent>(e, (child, newParent) => {
                child.UpdateParentComponent(newParent);
            });
        }
    }
}
