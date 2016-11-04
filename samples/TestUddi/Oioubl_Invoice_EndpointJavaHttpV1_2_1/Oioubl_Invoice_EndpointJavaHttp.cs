

namespace dk.gov.oiosi.samples.ProductionUddi
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using dk.gov.oiosi.samples.ClientExample;

    public class Oioubl_Invoice_EndpointJavaHttp : AbstractRaspClient
    {
        public Oioubl_Invoice_EndpointJavaHttp()
        {
            this.client = new OiosiRaspClient(UddiType.Production, "./Resources/xml/ProductionUddi/ProductionUddi_Oioubl_Invoice_EndpointJavaHttp_5798009811547.xml");
        }

        static void Main(string[] args)
        {
            new Oioubl_Invoice_EndpointJavaHttp().Perform();
            Console.WriteLine("Finish");
            Console.ReadKey();
        }
    }
}
