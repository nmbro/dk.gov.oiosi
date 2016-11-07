

namespace dk.gov.oiosi.samples.TestUddi
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using dk.gov.oiosi.samples.ClientExample;

    public class TestUddi_TestAll : AbstractTestAll
    {
        public TestUddi_TestAll()
        { }

        public override void Add(IList<MyKeyValuePair<AbstractRaspClient, Boolean>> list)
        {
            // Add all the test from the local service
            list.Add(new MyKeyValuePair<AbstractRaspClient, Boolean>(new dk.gov.oiosi.samples.ProductionUddi.Oioubl_Invoice_EndpointJavaHttp(), false));
            list.Add(new MyKeyValuePair<AbstractRaspClient, Boolean>(new dk.gov.oiosi.samples.ProductionUddi.Oioubl_Invoice_EndpointNetHttp(), false));
            list.Add(new MyKeyValuePair<AbstractRaspClient, Boolean>(new dk.gov.oiosi.samples.ProductionUddi.Oioubl_Invoice_LiveTestEndpoint(), false));
        }


        static void Main(string[] args)
        {
            TestUddi_TestAll all = new TestUddi_TestAll();
            all.Add();
            all.Perform();
            Console.WriteLine("Finish");
            Console.ReadKey();
        }
    }
}
