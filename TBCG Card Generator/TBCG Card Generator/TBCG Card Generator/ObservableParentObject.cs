using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TBCG_Card_Generator
{
    internal class ObservableParentObject : ObservableObject
    {
        protected void OnChildCollectionChanged<T_Child, T_Parent>(NotifyCollectionChangedEventArgs e, ParentUpdater<T_Child, T_Parent> updateParentCallback) where T_Parent : ObservableParentObject {
            switch (e.Action) {
                case NotifyCollectionChangedAction.Add:
                    foreach (T_Child c in e.NewItems)
                        updateParentCallback(c, (T_Parent)this);
                    break;
                case NotifyCollectionChangedAction.Remove:
                    foreach (T_Child c in e.OldItems)
                        updateParentCallback(c, null);
                    break;
                case NotifyCollectionChangedAction.Replace:
                    foreach (T_Child c in e.OldItems)
                        updateParentCallback(c, null);
                    foreach (T_Child c in e.NewItems)
                        updateParentCallback(c, (T_Parent)this);
                    break;
                case NotifyCollectionChangedAction.Reset:
                    foreach (T_Child c in e.OldItems)
                        updateParentCallback(c, null);
                    break;
            }
        }


        protected void SetChildCollection<T_Child, T_Parent>(ObservableCollection<T_Child> value, ref ObservableCollection<T_Child> targetField, NotifyCollectionChangedEventHandler collectionWatcher, string propertyName, ParentUpdater<T_Child, T_Parent> itemParentUpdater) where T_Parent : ObservableParentObject {
            if (value == targetField) return;
            if (value == null) value = new ObservableCollection<T_Child>();
            value.CollectionChanged += collectionWatcher;
            targetField.CollectionChanged -= collectionWatcher;
            foreach (T_Child c in targetField) itemParentUpdater(c, null);
            targetField = value;
            foreach (T_Child c in targetField) itemParentUpdater(c, (T_Parent)this);
            OnPropertyChanged(propertyName);
        }
    }
}
