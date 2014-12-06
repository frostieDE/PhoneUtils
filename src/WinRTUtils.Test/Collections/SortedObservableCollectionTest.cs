using GalaSoft.MvvmLight;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;
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

        [TestMethod]
        public void MoveTest()
        {
            var collection = new SortedObservableCollection<TestModel>();

            var item1 = new TestModel { Value = 1 };
            var item2 = new TestModel { Value = 42 };
            var item3 = new TestModel { Value = 10 };
            var item4 = new TestModel { Value = 50 };

            collection.ItemHasChanged = property => property == "Value";

            collection.Add(item1);
            collection.Add(item2);
            collection.Add(item3);
            collection.Add(item4);

            item2.Value = 2;
            item3.Value = 3;
            item4.Value = 4;

            Assert.AreEqual<int>(0, collection.IndexOf(item1));
            Assert.AreEqual<int>(1, collection.IndexOf(item2));
            Assert.AreEqual<int>(2, collection.IndexOf(item3));
            Assert.AreEqual<int>(3, collection.IndexOf(item4));

            item2.Value = 2;
            item3.Value = 3;
            item4.Value = 0;

            Assert.AreEqual<int>(1, collection.IndexOf(item1));
            Assert.AreEqual<int>(2, collection.IndexOf(item2));
            Assert.AreEqual<int>(3, collection.IndexOf(item3));
            Assert.AreEqual<int>(0, collection.IndexOf(item4));

            item3.Value = 2;

            Assert.AreEqual<int>(1, collection.IndexOf(item1));
            Assert.AreEqual<int>(2, collection.IndexOf(item2));
            Assert.AreEqual<int>(3, collection.IndexOf(item3));
            Assert.AreEqual<int>(0, collection.IndexOf(item4));
        }

        private class TestModel : ObservableObject, IComparable<TestModel>
        {
            private int _value;

            public int Value
            {
                get { return _value; }
                set { Set(() => Value, ref _value, value); }
            }

            public int CompareTo(TestModel other)
            {
                return Value.CompareTo(other.Value);
            }

            public override string ToString()
            {
                return Value.ToString();
            }
        }
    }
}
