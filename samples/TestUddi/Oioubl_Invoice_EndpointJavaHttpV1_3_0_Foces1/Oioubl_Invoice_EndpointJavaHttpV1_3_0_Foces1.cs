

namespace dk.gov.oiosi.samples.TestUddi
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using dk.gov.oiosi.samples.ClientExample;

    public class Oioubl_Invoice_EndpointJavaHttpV1_3_0_Foces1 : AbstractRaspClient
    {
        public Oioubl_Invoice_EndpointJavaHttpV1_3_0_Foces1()
        {
            this.client = new OiosiRaspClient(UddiType.Test, "./Resources/xml/TestUddi/TestUddi_Oioubl_Invoice_EndpointJavaHttpV1_3_0_Foces1.xml");
        }

        static void Main(string[] args)
        {
            new Oioubl_Invoice_EndpointJavaHttpV1_3_0_Foces1().Perform();
            Console.WriteLine("Finish");
            Console.ReadKey();
        }
    }
}
