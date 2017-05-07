using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace ATZ.CollectionObservers.Tests
{
    [TestFixture]
    public class CollectionChangedEventHandlersShould
    {
        [Test]
        public void SenderNullCollectionItemSourceDoesNotThrowExceptionWhenChangeActionIsReset()
        {
            var sender = new Mock<ICollectionChangedEventSource<int, int>>();
            // ReSharper disable once PossibleNullReferenceException => How Moq initializes expected scenarios, not null.
            sender.Setup(s => s.CollectionItemSource).Returns((IEnumerable<int>)null);
            var notifyCollectionChangedEventArgs = new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset, null);
            CollectionChangedEventHandlers<int, int>.Handle(sender.Object, notifyCollectionChangedEventArgs);
        }

        [Test]
        public void ThrowCorrectExceptionIfSenderIsNull()
        {
            var ex = Assert.Throws<ArgumentNullException>(() => CollectionChangedEventHandlers<int, int>.Handle(null, null));
            Assert.IsNotNull(ex);
            Assert.AreEqual("sender", ex.ParamName);
        }

        [Test]
        public void ThrowCorrectExceptionIfEventArgsIsNull()
        {
            var sender = new Mock<ICollectionChangedEventSource<int, int>>();
            var ex = Assert.Throws<ArgumentNullException>(
                () => CollectionChangedEventHandlers<int, int>.Handle(sender.Object, null));
            Assert.IsNotNull(ex);
            Assert.AreEqual("e", ex.ParamName);
        }
    }
}
