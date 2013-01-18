using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dk.gov.oiosi.samples.ClientExample
{
    public abstract class AbstractTestAll
    {

        private IList<MyKeyValuePair<AbstractRaspClient, Boolean>> list;

        public AbstractTestAll()
        {
            this.list = new List<MyKeyValuePair<AbstractRaspClient, Boolean>>();
        }

        public void Add()
        {
            this.Add(this.list);
        }

        public abstract void Add(IList<MyKeyValuePair<AbstractRaspClient, Boolean>> list);


        public void Perform()
        {
            // perform all defined test
            Console.WriteLine("Start performing all the test.");
            MyKeyValuePair<AbstractRaspClient, Boolean> pair;

            for (int index = 0; index < this.list.Count(); index++)
            {
                pair = this.list[index];
                pair.Value = pair.Key.Perform();
            }

            // print out the result
            Console.WriteLine("All test is now performed. Printing the result.");
            for (int index = 0; index < this.list.Count(); index++)
            {
                pair = this.list[index];
                Console.WriteLine("Test : " + ((object)pair.Key).GetType().AssemblyQualifiedName + (pair.Value ? " was performed succesfull." : " failed."));
            }
        }

        /**
        * The Map.Entry objects contained in the Set returned by entrySet().
        */
        public class MyKeyValuePair<K, V>
        {
            private K key;
            private V value;

            public MyKeyValuePair(K key, V value)
            {
                this.key = key;
                this.value = value;
            }

            public K Key
            {
                get
                {
                    return key;
                }
            }



            public V Value
            {
                get
                {
                    return value;
                }

                set
                {
                    this.value = value;
                }
            }
        }
    }
}