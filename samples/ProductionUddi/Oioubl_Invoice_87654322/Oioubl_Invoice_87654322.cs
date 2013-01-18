

namespace dk.gov.oiosi.samples.TestUddi
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using dk.gov.oiosi.samples.ClientExample;

    public class Oioubl_Invoice_5798000418554 : AbstractRaspClient
    {

        public Oioubl_Invoice_5798000418554()
        {
            this.client = new OiosiRaspClient(UddiType.Production, "./Resources/xml/ProductionUddi/OIOUBL_Invoice_5798000418554.xml");
        }

        static void Main(string[] args)
        {
            new Oioubl_Invoice_5798000418554().Perform();
            Console.WriteLine("Finish");
            Console.ReadKey();
        }
    }
}
