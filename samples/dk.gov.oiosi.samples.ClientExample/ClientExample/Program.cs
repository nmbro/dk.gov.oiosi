using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dk.gov.oiosi.samples.ClientExample
{
    class Program
    {
        static void Main(string[] args)
        {
            new OiosiRaspClient().SendDocument();
            Console.ReadKey();
        }
    }
}
