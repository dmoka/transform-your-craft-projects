using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace WarehousingService.Tests.Asserters
{
    public abstract class AbstractAsserter<TEntity, TSpecificAsserter>
        where TEntity : class
        where TSpecificAsserter : AbstractAsserter<TEntity, TSpecificAsserter>
    {
        protected readonly TEntity Actual;

        protected AbstractAsserter(TEntity actual)
        {
            Actual = actual;
            And = (TSpecificAsserter)this;
        }

        public TSpecificAsserter And { get; }

        protected static void AssertEntries(Func<TEntity, TSpecificAsserter> asserterInitializer,
            IEnumerable<TEntity> actualEntities, params Action<TSpecificAsserter>[] expectedEntriesAsserters)
        {
            IList<TEntity> actualList = actualEntities.ToList();
            var numberOfActualOrders = expectedEntriesAsserters.Length;

            Assert.True(
                actualList.Count == numberOfActualOrders,
                string.Format(
                    $"Lists (containing {typeof(TEntity).Name}s) have different sizes. Expected list size: {0}, actual list size: {1}",
                    numberOfActualOrders, actualList.Count));

            for (var i = 0; i < actualList.Count; i++)
                expectedEntriesAsserters[i].Invoke(asserterInitializer.Invoke(actualList[i]));
        }
    }
}
