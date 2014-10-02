using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using System;
using WinRTUtils.Collections;

namespace WinRTUtils.Test.Collections
{
    [TestClass]
    public class RangeObservableCollectionTest
    {
        [TestMethod]
        public void AddRangeTest()
        {
            var list = new string[] { "item 1", "item 2", "item 3", "item 4" };
            var collection = new RangeObservableCollection<string>();

            collection.AddRange(list);

            Assert.AreEqual<int>(4, collection.Count);
        }

        [TestMethod]
        public void AddRangeEmptyListTest()
        {
            var list = new string[] { };
            var collection = new RangeObservableCollection<string>();

            collection.AddRange(list);

            Assert.AreEqual<int>(0, collection.Count);

            collection.AddRange(new string[] { "item 1", "item 2" });
            Assert.AreEqual<int>(2, collection.Count);

            collection.AddRange(list);
            Assert.AreEqual<int>(2, collection.Count);
        }

        [TestMethod]
        public void AddRangeTestNull()
        {
            var collection = new RangeObservableCollection<string>();

            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                collection.AddRange(null);
            });
        }

        [TestMethod]
        public void RemoveRangeTest()
        {
            var list = new string[] { "item 1", "item 2", "item 3", "item 4" };
            var collection = new RangeObservableCollection<string>();

            collection.AddRange(list);
            Assert.AreEqual<int>(4, collection.Count);

            collection.RemoveRange(list);
            Assert.AreEqual<int>(0, collection.Count);
        }

        [TestMethod]
        public void RemoveRangeEmptyListTest()
        {
            var list = new string[] { "item 1", "item 2", "item 3", "item 4" };
            var collection = new RangeObservableCollection<string>();

            collection.AddRange(list);
            Assert.AreEqual<int>(4, collection.Count);

            collection.RemoveRange(new string[] { });
            Assert.AreEqual<int>(4, collection.Count);
        }

        [TestMethod]
        public void RemoveRangeTestNull()
        {
            var collection = new RangeObservableCollection<string>();

            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                collection.RemoveRange(null);
            });
        }
    }
}
