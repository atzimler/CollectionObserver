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

        [Test]
        public void SetTheSameSourceAsBeforeWithoutDetachingIt()
        {
            var testCollection = new TestCollection();

            var collectionObserver = new TestCollectionObserver
            {
                Source = testCollection
            };
            testCollection.AddCalled = false;
            Assert.IsFalse(testCollection.AddCalled);

            collectionObserver.Source = testCollection;
            Assert.IsFalse(testCollection.AddCalled);
        }

        [Test]
        public void NotCrashWhenSourceIsNullWhenAddingSourceAndTargetAtTheSameTime()
        {
            var collectionObserver = new TestCollectionObserver
            {
                Source = null,
                Target = new ObservableCollection<int>()
            };

            Assert.DoesNotThrow(() => collectionObserver.Add(13, 42));
        }

        [Test]
        public void NotCrashWhenTargetIsNullWhenAddingSourceAndTargetAtTheSameTime()
        {
            var collectionObserver = new TestCollectionObserver
            {
                Source = new ObservableCollection<int>(),
                Target = null
            };

            Assert.DoesNotThrow(() => collectionObserver.Add(13, 42));
        }
    }
}
