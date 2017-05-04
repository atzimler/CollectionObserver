using System.Collections.ObjectModel;

namespace ATZ.CollectionObservers
{
    /// <summary>
    /// Connects two ObservableCollection collections.
    /// </summary>
    /// <typeparam name="TSource">The type of the items in the source collection.</typeparam>
    /// <typeparam name="TTarget">The type of the items in the mirror collection.</typeparam>
    public abstract class CollectionObserver<TSource, TTarget> :
        CollectionObserverBase<TSource, TTarget, ObservableCollection<TTarget>>
    {
        /// <see cref="ICollectionChangedEventSource{TSourceItem,TCollectionItem}.AddItem"/>
        public override void AddItem(TTarget item)
        {
            TargetCollection?.Add(item);
        }

        /// <see cref="ICollectionChangedEventSource{TSourceItem,TCollectionItem}.InsertItem"/>
        public override void InsertItem(int index, TTarget item)
        {
            TargetCollection?.Insert(index, item);
        }

        /// <see cref="ICollectionChangedEventSource{TSourceItem,TCollectionItem}.MoveItem"/>
        public override void MoveItem(int oldIndex, int newIndex)
        {
            TargetCollection?.Move(oldIndex, newIndex);
        }
    }
}