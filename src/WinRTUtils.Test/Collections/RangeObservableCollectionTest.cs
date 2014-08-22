using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
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
        public void AddRangeTestNull()
        {

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
        public void RemoveRangeTestNull()
        {

        }
    }
}
