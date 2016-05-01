using System.ComponentModel;

using TBCG_Card_Generator.DataModels.Enumerations;

namespace TBCG_Card_Generator.DataModels
{
    public abstract class Card : ObservableParentObject
    {
        public abstract CardType CardType { get; }



        string name;
        string description;
        string flavorText;



        public string Name { get { return name; } set { name = value; OnPropertyChanged("Name"); } }
        public string Description { get { return description; } set { description = value; OnPropertyChanged("Description"); } }
        public string FlavorText { get { return flavorText; } set { flavorText = value; OnPropertyChanged("FlavorText"); } }
    }
}
