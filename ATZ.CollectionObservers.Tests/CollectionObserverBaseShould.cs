using NUnit.Framework;
using System.Collections.ObjectModel;

namespace ATZ.CollectionObservers.Tests
{
    [TestFixture]
    public class CollectionObserverBaseShould
    {
        [Test]
        public void AllowAddingToSourceAndTargetAtTheSameTime()
        {
            var sourceCollection = new ObservableCollection<int>();
            var targetCollection = new ObservableCollection<int>();

            var collectionObserver = new TestCollectionObserver
            {
                Source = sourceCollection,
                Target = targetCollection
            };

            collectionObserver.Add(13, 42);

            Assert.AreEqual(1, sourceCollection.Count);
            Assert.AreEqual(1, targetCollection.Count);
            Assert.AreEqual(13, sourceCollection[0]);
            Assert.AreEqual(42, targetCollection[0]);
        }

    }
}
