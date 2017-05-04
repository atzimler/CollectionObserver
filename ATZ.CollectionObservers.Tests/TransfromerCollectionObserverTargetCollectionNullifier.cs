using System.Collections.ObjectModel;

namespace ATZ.CollectionObservers.Tests
{
    public class TransfromerCollectionObserverTargetCollectionNullifier : TransformerCollectionObserver<int, int>
    {
        public TransfromerCollectionObserverTargetCollectionNullifier()
            : base(n => n)
        {
            SourceCollection = new ObservableCollection<int> { 1, 2, 3 };
            TargetCollection = new ObservableCollection<int>();
        }

        public override void ReplaceItem(int index, int newItem)
        {
            TargetCollection = null;
            base.ReplaceItem(index, newItem);
        }
    }
}
