

namespace dk.gov.oiosi.samples.ProductionUddi
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using dk.gov.oiosi.samples.ClientExample;

    public class Oioubl_Invoice_EndpointNetHttp : AbstractRaspClient
    {
        public Oioubl_Invoice_EndpointNetHttp()
        {
            this.client = new OiosiRaspClient(UddiType.Production, "./Resources/xml/ProductionUddi/ProductionUddi_Oioubl_Invoice_EndpointNetHttp_5798009811561.xml");
        }

        static void Main(string[] args)
        {
            new Oioubl_Invoice_EndpointNetHttp().Perform();
            Console.WriteLine("Finish");
            Console.ReadKey();
        }
    }
}
