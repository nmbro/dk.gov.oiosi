using System;
using System.Collections.Generic;
using System.Text;

using NUnit.Framework;

using dk.gov.oiosi.common.cache;

namespace dk.gov.oiosi.test.unit.common.cache {

    [TestFixture]
    public class QuantityCacheTest 
    {
        private ICache<string, string> cache;

        [Test]
        public void SingleAddRemoveTest() 
        {
            Console.WriteLine("{0} Single Add Remove Test Started", DateTime.Now);

            this.cache = new QuantityCache<string, string>(4);
            this.cache.Add("test", "test");
            this.TestExists("test");
            this.cache.Remove("test");
            this.TestDoNotExists("test");

            Console.WriteLine("{0} Single Add Remove Test Completed", DateTime.Now);
        }

        [Test]
        public void MultipleAddRemoveTest() {
            Console.WriteLine("{0} Multiple Add Remove Test Stated", DateTime.Now);

            this.cache = new QuantityCache<string, string>(6);
            string[] a = new string[] { "a1", "a2", "a3" };
            string[] b = new string[] { "b1", "b2" };
            string[] c = new string[] { "c1", "c2", "c3" };

            this.AddStrings(a);
            this.AddStrings(b);
            this.CheckStringsExists(a);
            this.CheckStringsExists(b);

            this.RemoveStrings(b);
            this.CheckStringsExists(a);
            this.CheckStringsNotExists(b);

            this.AddStrings(c);
            this.CheckStringsExists(a);
            this.CheckStringsExists(c);

            Console.WriteLine("{0} Multiple Add Remove Test Completed", DateTime.Now);
        }

        [Test]
        public void MultipleAddAboveLimitTest() {
            Console.WriteLine("{0} Multiple Add Above Limit Test Stated", DateTime.Now);

            this.cache = new QuantityCache<string, string>(5);
            string[] a = new string[] { "a1", "a2", "a3" };
            string[] b = new string[] { "b1", "b2" };
            string[] c = new string[] { "c1", "c2", "c3" };

            this.AddStrings(a);
            this.AddStrings(b);
            this.CheckStringsExists(a);
            this.CheckStringsExists(b);

            this.AddStrings(c);
            this.CheckStringsExists(b);
            this.CheckStringsExists(c);
            this.CheckStringsNotExists(a);

            Console.WriteLine("{0} Multiple Add Above Limit Test Completed", DateTime.Now);
        }

        [Test]
        public void MultipleAddAboveLimitLastViewedTest() {
            Console.WriteLine("{0} Multiple Add Above Limit Last Viewed Test Stated", DateTime.Now);

            this.cache = new QuantityCache<string, string>(4);
            string[] a = new string[] { "a1", "a2" };
            string[] b = new string[] { "b1", "b2" };
            string[] c = new string[] { "c1", "c2" };

            this.AddStrings(a);
            this.AddStrings(b);
            this.CheckStringsExists(b);
            this.CheckStringsExists(a);

            this.AddStrings(c);
            this.CheckStringsExists(a);
            this.CheckStringsExists(c);
            this.CheckStringsNotExists(b);

            Console.WriteLine("{0} Multiple Add Above Limit Last Viewed Test Completed", DateTime.Now);
        }

        [Test]
        public void SetTest()
        {
            string a = "a";
            string b = "b";
            string c = "c";
            this.TestSet(a, b);
            this.TestSet(a, c);
            this.TestElement(a, c);
            this.TestRemove(a);
            this.TestDoNotExists(a);
        }

        private void NTest(int n) 
        {
            for (int i = 0; i < n; i++) 
            {
                string iString = i.ToString();
                this.TestAdd(iString, iString);
            }
            for (int i = 0; i < n; i++)
            {
                string iString = i.ToString();
                this.TestRemove(iString);
            }
        }

        private void AddStrings(string[] strings) 
        {
            foreach (string s in strings) {
                this.TestAdd(s, s);
            }
        }

        private void RemoveStrings(string[] strings) 
        {
            foreach (string s in strings) 
            {
                this.TestRemove(s);
            }
        }

        private void CheckStringsExists(string[] strings)
        {
            foreach (string s in strings) {
                this.TestExists(s);
            }
        }

        private void CheckStringsNotExists(string[] strings) 
        {
            foreach (string s in strings) 
            {
                this.TestDoNotExists(s);
            }
        }

        private void TestAdd(string key, string value) 
        {
            this.cache.Add(key, value);
            this.TestExists(key);
            this.TestElement(key, value);
        }

        private void TestSet(string key, string value)
        {
            this.cache.Set(key, value);
            this.TestExists(key);
            this.TestElement(key, value);
        }

        private void TestRemove(string key)
        {
            this.cache.Remove(key);
            this.TestDoNotExists(key);
        }

        private void TestExists(string key) 
        {
            string value = null;
            Assert.IsTrue(this.cache.TryGetValue(key, out value));
        }

        private void TestElement(string key, string expected)
        {
            string current = null;
            Assert.IsTrue(this.cache.TryGetValue(key, out current));
            Assert.AreEqual(expected, current);
        }

        private void TestDoNotExists(string key)
        {
            string current = null;
            Assert.IsFalse(cache.TryGetValue(key, out current));
        }
    }
}
