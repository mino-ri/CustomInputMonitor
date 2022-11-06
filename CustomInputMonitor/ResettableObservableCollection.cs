using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;

namespace CustomInputMonitor;

public class ResettableObservableCollection<T> : ObservableCollection<T>
{
    public void Reset(IEnumerable<T> values)
    {
        var items = (List<T>)Items;
        items.Clear();
        items.AddRange(values);
        OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        OnPropertyChanged(new PropertyChangedEventArgs(nameof(Count)));
    }
}
