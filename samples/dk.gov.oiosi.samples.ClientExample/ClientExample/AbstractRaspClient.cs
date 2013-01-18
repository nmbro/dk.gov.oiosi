using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dk.gov.oiosi.samples.ClientExample
{
    public abstract class AbstractRaspClient
    {
        protected OiosiRaspClient client = null;

        public AbstractRaspClient()
        {
        }

        public bool Perform()
        {
            return client.SendDocument();
        }
    }
}