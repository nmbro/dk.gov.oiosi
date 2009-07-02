using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

using NUnit.Framework;

using dk.gov.oiosi.common;
using dk.gov.oiosi.common.cache;

namespace dk.gov.oiosi.test.unit.common.cache {

    [TestFixture]
    public class TimedCacheTest {
        private TimedCache<string, string> _cache;

        [Test]
        public void SingleAddRemoveTest() {
            Console.WriteLine("{0} Single Add Remove Test Started", DateTime.Now);

            _cache = new TimedCache<string,string>(TimeSpan.FromHours(5.0));
            _cache.Add("test", "test");
            TestExists("test");
            _cache.Remove("test");
            TestDoNotExists("test");

            Console.WriteLine("{0} Single Add Remove Test Completed", DateTime.Now);
        }

        [Test]
        public void MultipleAddRemoveTest() {
            Console.WriteLine("{0} Multiple Add Remove Test Stated", DateTime.Now);

            _cache = new TimedCache<string,string>(TimeSpan.FromHours(5.0));
            string[] a = new string[]{ "a1", "a2", "a3" };
            string[] b = new string[] { "b1", "b2" };
            string[] c = new string[] { "c1", "c2", "c3", "c4" };

            AddStrings(a);
            AddStrings(b);
            CheckStringsExists(a);
            CheckStringsExists(b);

            RemoveStrings(b);
            CheckStringsExists(a);

            AddStrings(c);
            CheckStringsExists(a);
            CheckStringsExists(c);

            Console.WriteLine("{0} Multiple Add Remove Test Completed", DateTime.Now);
        }

        [Test]
        public void SingleTimeoutRemovalTest() {
            Console.WriteLine("{0} Single Timeout Removal Test Stated", DateTime.Now);
            
            TimeSpan timeout = TimeSpan.FromSeconds(1.0);
            _cache = new TimedCache<string, string>(timeout);
            _cache.Add("test", "test");

            Console.WriteLine("{0} Waiting for cache timeout 2x{1}", DateTime.Now, timeout);
            Thread.Sleep(timeout);
            Thread.Sleep(timeout);

            TestDoNotExists("test");

            Console.WriteLine("{0} Single Timeout Removal Test Completed", DateTime.Now);
        }

        [Test]
        public void MultipleTimeoutRemovalTest() {
            Console.WriteLine("{0} Multiple Timeout Removal Test Stated", DateTime.Now);

            TimeSpan timeout = TimeSpan.FromSeconds(4.0);
            TimeSpan oneQuaterTimeout = new TimeSpan(timeout.Ticks / 4);
            string a = "a";
            string b = "b";
            _cache = new TimedCache<string, string>(timeout);
            TestAdd(a, a);

            Console.WriteLine("{0} Waiting 2x{1}", DateTime.Now, oneQuaterTimeout);
            Thread.Sleep(oneQuaterTimeout);
            Thread.Sleep(oneQuaterTimeout);

            TestAdd(b, b);
            TestExists(a);

            Console.WriteLine("{0} Waiting {1}", DateTime.Now, oneQuaterTimeout);
            Thread.Sleep(oneQuaterTimeout);

            TestExists(a);
            TestExists(b);

            Console.WriteLine("{0} Waiting 2x{1}", DateTime.Now, oneQuaterTimeout);
            Thread.Sleep(oneQuaterTimeout);
            Thread.Sleep(oneQuaterTimeout);

            TestDoNotExists(a);
            TestExists(b);

            Console.WriteLine("{0} Waiting 2x{1}", DateTime.Now, oneQuaterTimeout);
            Thread.Sleep(oneQuaterTimeout);
            Thread.Sleep(oneQuaterTimeout);

            TestDoNotExists(a);
            TestDoNotExists(b);

            Console.WriteLine("{0} Multiple Timeout Removal Test Completed", DateTime.Now);
        }

        [Test]
        public void TimeoutRemovalWithDelete() {
            Console.WriteLine("{0} Multiple Timeout Removal Test Stated", DateTime.Now);

            TimeSpan timeout = TimeSpan.FromSeconds(2.0);
            TimeSpan halfTimeout = new TimeSpan(timeout.Ticks / 2);
            string a = "a";
            string b = "b";
            _cache = new TimedCache<string, string>(timeout);

            TestAdd(a, a);
            TestAdd(b, b);

            Console.WriteLine("{0} Waiting {1}", DateTime.Now, halfTimeout);
            Thread.Sleep(halfTimeout);

            TestRemove(a);
            TestExists(b);

            Console.WriteLine("{0} Waiting 2x{1}", DateTime.Now, halfTimeout);
            Thread.Sleep(halfTimeout);
            Thread.Sleep(halfTimeout);

            TestDoNotExists(a);
            TestDoNotExists(b);

            Console.WriteLine("{0} Multiple Timeout Removal Test Completed", DateTime.Now);
        }

        [Test]
        public void SetTest() {
            string a = "a";
            string b = "b";
            string c = "c";
            TestSet(a, b);
            TestSet(a, c);
            TestElement(a, c);
            TestRemove(a);
            TestDoNotExists(a);
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
            foreach(string s in strings) {
                TestExists(s);
            }
        }

        private void TestAdd(string key, string value) {
            _cache.Add(key, value);
            TestExists(key);
            TestElement(key, value);
        }

        private void TestSet(string key, string value)
        {
            _cache.Set(key, value);
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
