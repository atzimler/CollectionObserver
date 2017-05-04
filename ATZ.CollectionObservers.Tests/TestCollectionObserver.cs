using System.Collections.ObjectModel;

namespace ATZ.CollectionObservers.Tests
{
    public class TestCollectionObserver : CollectionObserverBase<int, int, ObservableCollection<int>>
    {
        public ObservableCollection<int> Source
        {
            get => SourceCollection;
            set => SourceCollection = value;
        }

        public ObservableCollection<int> Target
        {
            get => TargetCollection;
            set => TargetCollection = value;
        }

        public override void ClearCollection()
        {
            TargetCollection?.Clear();
        }

        public override void AddItem(int item)
        {
            TargetCollection?.Add(item);
        }

        public override int CreateItem(int sourceItem)
        {
            return sourceItem;
        }

        public override void InsertItem(int index, int item)
        {
            TargetCollection?.Insert(index, item);
        }

        public override void MoveItem(int oldIndex, int newIndex)
        {
            TargetCollection?.Move(oldIndex, newIndex);
        }

        public override void RemoveItem(int index)
        {
            TargetCollection?.RemoveAt(index);
        }

        public override void ReplaceItem(int index, int newItem)
        {
            var collection = TargetCollection;
            if (collection == null)
            {
                return;
            }

            collection[index] = newItem;
        }
    }
}
