

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
            list.Add(new MyKeyValuePair<AbstractRaspClient, Boolean>(new dk.gov.oiosi.samples.TestUddi.Oioubl_Invoice_EndpointJavaHttpV1_2_1(), false));
            list.Add(new MyKeyValuePair<AbstractRaspClient, Boolean>(new dk.gov.oiosi.samples.TestUddi.Oioubl_Invoice_EndpointJavaHttpV1_2_3(), false));            
            list.Add(new MyKeyValuePair<AbstractRaspClient, Boolean>(new dk.gov.oiosi.samples.TestUddi.Oioubl_Invoice_EndpointNetHttpV1_2_1(), false));
            list.Add(new MyKeyValuePair<AbstractRaspClient, Boolean>(new dk.gov.oiosi.samples.TestUddi.Oioubl_Invoice_EndpointNetHttpV1_2_3(), false));
            /*
            list.Add(new MyKeyValuePair<AbstractRaspClient, Boolean>(new dk.gov.oiosi.samples.TestUddi.Oioubl_UTS_EndpointJavaHttpV1_2_3.Program(), false));
            list.Add(new MyKeyValuePair<AbstractRaspClient, Boolean>(new dk.gov.oiosi.samples.TestUddi.Oioubl_UTS_EndpointNetHttpV1_2_3.Program(), false));*/



           /* LocalService_TestAll localService_testAll = new LocalService_TestAll();
            localService_testAll.AddTest(list);

            // Add all the test from the TestUddi
            TestUddi_TestAll testUddi_TestAll = new TestUddi_TestAll();
            testUddi_TestAll.AddTest(list);*/
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
