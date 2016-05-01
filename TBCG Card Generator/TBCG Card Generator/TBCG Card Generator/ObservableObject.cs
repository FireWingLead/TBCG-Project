using System.ComponentModel;

namespace TBCG_Card_Generator
{
    class ObservableObject : INotifyPropertyChanged
    {
        protected void OnPropertyChanged(string propertyName) {
            PropertyChangedEventHandler evntLocal = PropertyChanged;
            if (evntLocal != null) evntLocal(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
