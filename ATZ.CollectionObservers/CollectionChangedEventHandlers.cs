using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace ATZ.CollectionObservers
{
    /// <summary>
    /// Generic implementation for handling collection change events in ObservableCollections when mirroring the collection.
    /// </summary>
    /// <typeparam name="TEventItem">The type of the items in the event from the source ObservableCollection.</typeparam>
    /// <typeparam name="TCollectionItem">The type of the items in the mirror ObservableCollection.</typeparam>
    public static class CollectionChangedEventHandlers<TEventItem, TCollectionItem>
    {
        [NotNull]
        private static readonly Dictionary<NotifyCollectionChangedAction, Action<ICollectionChangedEventSource<TEventItem, TCollectionItem>, NotifyCollectionChangedEventArgs>>
            EventHandlers = new Dictionary<NotifyCollectionChangedAction, Action<ICollectionChangedEventSource<TEventItem, TCollectionItem>, NotifyCollectionChangedEventArgs>> {
                    {NotifyCollectionChangedAction.Add, Add},
                    {NotifyCollectionChangedAction.Move, Move},
                    {NotifyCollectionChangedAction.Remove, Remove},
                    {NotifyCollectionChangedAction.Replace, Replace},
                    {NotifyCollectionChangedAction.Reset, Reset}
                };

        private static void Add([NotNull] ICollectionChangedEventSource<TEventItem, TCollectionItem> sender, [NotNull] NotifyCollectionChangedEventArgs e)
        {
            var insertPosition = e.NewStartingIndex;
            // ReSharper disable once PossibleNullReferenceException => e.NewItems != null <= NotifyCollectionChangedEventArgs.Constructors. for NotifyCollectionChangedAction.Add
            foreach (TEventItem model in e.NewItems)
            {
                sender.InsertItem(insertPosition++, sender.CreateItem(model));
            }
        }

        private static void Move([NotNull] ICollectionChangedEventSource<TEventItem, TCollectionItem> sender, [NotNull] NotifyCollectionChangedEventArgs e)
        {
            sender.MoveItem(e.OldStartingIndex, e.NewStartingIndex);
        }

        private static void Remove([NotNull] ICollectionChangedEventSource<TEventItem, TCollectionItem> sender, [NotNull] NotifyCollectionChangedEventArgs e)
        {
            // ReSharper disable once PossibleNullReferenceException => e.OldItems != null <= NotifyCollectionChangedEventArgs.Constructors. for NotifyCollectionChangedAction.Remove
            var itemsToRemove = e.OldItems.Count;
            while (itemsToRemove-- > 0)
            {
                sender.RemoveItem(e.OldStartingIndex);
            }
        }

        private static void Reset([NotNull] ICollectionChangedEventSource<TEventItem, TCollectionItem> sender, NotifyCollectionChangedEventArgs e)
        {
            sender.ClearCollection();
            if (sender.CollectionItemSource == null)
            {
                return;
            }

            foreach (var model in sender.CollectionItemSource)
            {
                sender.AddItem(sender.CreateItem(model));
            }
        }

        private static void Replace([NotNull] ICollectionChangedEventSource<TEventItem, TCollectionItem> sender, [NotNull] NotifyCollectionChangedEventArgs e)
        {
            // ReSharper disable once PossibleNullReferenceException => e.NewItems != null <= NotifyCollectionChangedEventArgs.Constructors. for NotifyCollectionChangedAction.Replace
            sender.ReplaceItem(e.NewStartingIndex, sender.CreateItem((TEventItem)e.NewItems[0]));
        }

        /// <summary>
        /// Handle INotifyCollectionChanged events.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The arguments of the event.</param>
        /// <exception cref="ArgumentNullException">The sender or the event argument is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">The event argument Action and other data properties are not in match.
        /// This generally does not occur if the collection change event argument is acquired from the system call. See exception
        /// message for the cases where the cause is the client API. These cases are also documented in the test code.</exception>
        public static void Handle(ICollectionChangedEventSource<TEventItem, TCollectionItem> sender, NotifyCollectionChangedEventArgs e)
        {
            if (sender == null)
            {
                throw new ArgumentNullException(nameof(sender));
            }
            if (e == null)
            {
                throw new ArgumentNullException(nameof(e));
            }

            var handler = EventHandlers[e.Action];
            // ReSharper disable once PossibleNullReferenceException => We don't add any null actions to the EventHandlers collection.
            handler(sender, e);
        }
    }
}
