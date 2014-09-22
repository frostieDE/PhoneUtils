using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using WinRTUtils.Collections;

namespace PhoneUtils.Test.Collections
{
    [TestClass]
    public class SortedObservableCollectionTest
    {
        [TestMethod]
        public void ConstructorTests()
        {
            var list = new string[] { "item 4", "item 2", "item 5", "item 9"};
            var collection = new SortedObservableCollection<string>(list);

            var collection2 = new SortedObservableCollection<string>();

            foreach (var item in list)
            {
                collection2.Add(item);
            }

            Assert.AreEqual<int>(4, collection.Count);
            Assert.AreEqual<int>(collection.Count, collection2.Count);

            Assert.AreEqual<int>(0, collection.IndexOf("item 2"));
            Assert.AreEqual<int>(1, collection.IndexOf("item 4"));
            Assert.AreEqual<int>(2, collection.IndexOf("item 5"));
            Assert.AreEqual<int>(3, collection.IndexOf("item 9"));
        }

        [TestMethod]
        public void AddTests()
        {
            var collection = new SortedObservableCollection<string>();
            collection.Add("item 1");

            Assert.AreEqual<int>(1, collection.Count);
            Assert.AreEqual<int>(0, collection.IndexOf("item 1"));

            collection.Add("item 0");
            Assert.AreEqual<int>(2, collection.Count);
            Assert.AreEqual<int>(0, collection.IndexOf("item 0"));
            Assert.AreEqual<int>(1, collection.IndexOf("item 1"));

            collection.Add("item 9");
            Assert.AreEqual<int>(3, collection.Count);
            Assert.AreEqual<int>(0, collection.IndexOf("item 0"));
            Assert.AreEqual<int>(1, collection.IndexOf("item 1"));
            Assert.AreEqual<int>(2, collection.IndexOf("item 9"));

            collection.Add("item 5");
            Assert.AreEqual<int>(4, collection.Count);
            Assert.AreEqual<int>(0, collection.IndexOf("item 0"));
            Assert.AreEqual<int>(1, collection.IndexOf("item 1"));
            Assert.AreEqual<int>(2, collection.IndexOf("item 5"));
            Assert.AreEqual<int>(3, collection.IndexOf("item 9"));
        }
    }
}
