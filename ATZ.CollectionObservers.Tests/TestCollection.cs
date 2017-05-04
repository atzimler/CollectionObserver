using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace ATZ.CollectionObservers.Tests
{
    public class TestCollection : ObservableCollection<int>
    {
        public bool AddCalled { get; set; }
        public bool RemoveCalled { get; set; }

        public override event NotifyCollectionChangedEventHandler CollectionChanged
        {
            add
            {
                AddCalled = true;
                base.CollectionChanged += value;
            }
            remove
            {
                RemoveCalled = true;
                base.CollectionChanged -= value;
            }
        }
    }
}
