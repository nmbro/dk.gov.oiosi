

namespace dk.gov.oiosi.samples.ProductionUddi
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
            this.client = new OiosiRaspClient(UddiType.Production, "./Resources/xml/ProductionUddi/ProductionUddi_Oioubl_Invoice_LiveTestEndpoint_5798009811578.xml");
        }

        static void Main(string[] args)
        {
            new Oioubl_Invoice_LiveTestEndpoint().Perform();
            Console.WriteLine("Finish");
            Console.ReadKey();
        }
    }
}
