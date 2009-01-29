using System;
using System.Collections.Generic;
using System.Text;

using NUnit.Framework;

using dk.gov.oiosi.common.cache;

namespace dk.gov.oiosi.test.nunit.library.common.cache {

    [TestFixture]
    public class QuantityCacheTest {

        private QuantityCache<string, string> _cache;

        [Test]
        public void SingleAddRemoveTest() {
            Console.WriteLine("{0} Single Add Remove Test Started", DateTime.Now);

            _cache = new QuantityCache<string, string>(4);
            _cache.Add("test", "test");
            TestExists("test");
            _cache.Remove("test");
            TestDoNotExists("test");

            Console.WriteLine("{0} Single Add Remove Test Completed", DateTime.Now);
        }

        [Test]
        public void MultipleAddRemoveTest() {
            Console.WriteLine("{0} Multiple Add Remove Test Stated", DateTime.Now);

            _cache = new QuantityCache<string, string>(6);
            string[] a = new string[] { "a1", "a2", "a3" };
            string[] b = new string[] { "b1", "b2" };
            string[] c = new string[] { "c1", "c2", "c3" };

            AddStrings(a);
            AddStrings(b);
            CheckStringsExists(a);
            CheckStringsExists(b);

            RemoveStrings(b);
            CheckStringsExists(a);
            CheckStringsNotExists(b);

            AddStrings(c);
            CheckStringsExists(a);
            CheckStringsExists(c);

            Console.WriteLine("{0} Multiple Add Remove Test Completed", DateTime.Now);
        }

        [Test]
        public void MultipleAddAboveLimitTest() {
            Console.WriteLine("{0} Multiple Add Above Limit Test Stated", DateTime.Now);

            _cache = new QuantityCache<string, string>(5);
            string[] a = new string[] { "a1", "a2", "a3" };
            string[] b = new string[] { "b1", "b2" };
            string[] c = new string[] { "c1", "c2", "c3" };

            AddStrings(a);
            AddStrings(b);
            CheckStringsExists(a);
            CheckStringsExists(b);

            AddStrings(c);
            CheckStringsExists(b);
            CheckStringsExists(c);
            CheckStringsNotExists(a);

            Console.WriteLine("{0} Multiple Add Above Limit Test Completed", DateTime.Now);
        }

        [Test]
        public void MultipleAddAboveLimitLastViewedTest() {
            Console.WriteLine("{0} Multiple Add Above Limit Last Viewed Test Stated", DateTime.Now);

            _cache = new QuantityCache<string, string>(4);
            string[] a = new string[] { "a1", "a2" };
            string[] b = new string[] { "b1", "b2" };
            string[] c = new string[] { "c1", "c2" };

            AddStrings(a);
            AddStrings(b);
            CheckStringsExists(b);
            CheckStringsExists(a);

            AddStrings(c);
            CheckStringsExists(a);
            CheckStringsExists(c);
            CheckStringsNotExists(b);

            Console.WriteLine("{0} Multiple Add Above Limit Last Viewed Test Completed", DateTime.Now);
        }

        private void NTest(int n) {
            for (int i = 0; i < n; i++) {
                string iString = i.ToString();
                TestAdd(iString, iString);
            }
            for (int i = 0; i < n; i++) {
                string iString = i.ToString();
                TestRemove(iString);
            }
        }

        private void AddStrings(string[] strings) {
            foreach (string s in strings) {
                TestAdd(s, s);
            }
        }

        private void RemoveStrings(string[] strings) {
            foreach (string s in strings) {
                TestRemove(s);
            }
        }

        private void CheckStringsExists(string[] strings) {
            foreach (string s in strings) {
                TestExists(s);
            }
        }

        private void CheckStringsNotExists(string[] strings) {
            foreach (string s in strings) {
                TestDoNotExists(s);
            }
        }

        private void TestAdd(string key, string value) {
            _cache.Add(key, value);
            TestExists(key);
            TestElement(key, value);
        }

        private void TestRemove(string key) {
            _cache.Remove(key);
            TestDoNotExists(key);
        }

        private void TestExists(string key) {
            Assert.IsTrue(_cache.ContainsKey(key));
            string current = null;
            Assert.IsTrue(_cache.TryGetValue(key, out current));
        }

        private void TestElement(string key, string expected) {
            string current = null;
            Assert.IsTrue(_cache.TryGetValue(key, out current));
            Assert.AreEqual(expected, current);
        }

        private void TestDoNotExists(string key) {
            Assert.IsFalse(_cache.ContainsKey(key));
            string current = null;
            Assert.IsFalse(_cache.TryGetValue(key, out current));
        }
    }
}
