using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using dk.gov.oiosi.test.unit.common.cache;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            TimedCacheTest timedCacheTest = new TimedCacheTest();
            timedCacheTest.GetValidUntilDateTimeConfigTest();
        }
    }
}
