

namespace dk.gov.oiosi.samples.TestUddi
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using dk.gov.oiosi.samples.ClientExample;

    public class Oioubl_Invoice_LiveTestEndpoint : AbstractRaspClient
    {

        public Oioubl_Invoice_LiveTestEndpoint()
        {

            //this.client = new OiosiRaspClient(UddiType.Production, "./Resources/xml/ProductionUddi/123.xml");
            this.client = new OiosiRaspClient(UddiType.Production, "./Resources/xml/ProductionUddi/OIOUBL_Invoice_v2p2.xml");
        }

        static void Main(string[] args)
        {
            new Oioubl_Invoice_LiveTestEndpoint().Perform();
            Console.WriteLine("Finish");
            Console.ReadKey();
        }
    }
}
