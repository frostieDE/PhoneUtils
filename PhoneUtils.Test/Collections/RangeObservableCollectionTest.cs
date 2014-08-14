using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using PhoneUtils.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneUtils.Test.Collections
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
