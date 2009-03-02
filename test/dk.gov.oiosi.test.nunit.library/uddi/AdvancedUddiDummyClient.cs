/*
  * The contents of this file are subject to the Mozilla Public
  * License Version 1.1 (the "License"); you may not use this
  * file except in compliance with the License. You may obtain
  * a copy of the License at http://www.mozilla.org/MPL/
  *
  * Software distributed under the License is distributed on an
  * "AS IS" basis, WITHOUT WARRANTY OF ANY KIND, either express
  * or implied. See the License for the specific language governing
  * rights and limitations under the License.
  *
  *
  * The Original Code is .NET RASP toolkit.
  *
  * The Initial Developer of the Original Code is Accenture and Avanade.
  * Portions created by Accenture and Avanade are Copyright (C) 2007
  * Danish National IT and Telecom Agency (http://www.itst.dk). 
  * All Rights Reserved.
  *
  * Contributor(s):
  *   Gert Sylvest (gerts@avanade.com)
  *   Patrik Johansson (p.johansson@accenture.com)
  *   Michael Nielsen (michaelni@avanade.com)
  *   Dennis Søgaard (dennis.j.sogaard@accenture.com)
  *   Ramzi Fadel (ramzif@avanade.com)
  *   Mikkel Hippe Brun (mhb@itst.dk)
  *   Finn Hartmann Jordal (fhj@itst.dk)
  *   Christian Lanng (chl@itst.dk)
  *
  */
using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using dk.gov.oiosi.addressing;
using dk.gov.oiosi.uddi;

namespace dk.gov.oiosi.test.nunit.library.uddi {
    public class AdvancedUddiDummyClient :IUddiLookupClient {
        private List<Uri> _erroneousEndpoints = new List<Uri>();
        private Dictionary<Uri,List<Identifier>> _nonExistingRegistrations = new Dictionary<Uri, List<Identifier>>();
    	private Uri _address;

    	public AdvancedUddiDummyClient(Uri address){
			_address = address;
			AdvancedUddiDummyClientConfig config = dk.gov.oiosi.configuration.ConfigurationHandler.GetConfigurationSection<AdvancedUddiDummyClientConfig>();
    		ErroneousEndpoints = config.ErroneousEndpoints;
    		NonExistingRegistrations = config.NonExistingRegistrations;
		}

    	public List<Uri> ErroneousEndpoints {
            get { return _erroneousEndpoints; }
            set { _erroneousEndpoints = value; }
        }

        public Dictionary<Uri, List<Identifier>> NonExistingRegistrations {
            get { return _nonExistingRegistrations; }
            set { _nonExistingRegistrations = value; }
        }


        public List<UddiLookupResponse> Lookup(LookupParameters parameters) {
			if (ErroneousEndpoints.Contains(_address)){
				Console.WriteLine("Dummy UDDI throwing on register " + _address);
				throw new UddiException();
			}
			if (NonExistingRegistrations.ContainsKey(_address) && NonExistingRegistrations[_address].Contains(parameters.Identifier)){
				Console.WriteLine("Dummy UDDI returning empty from register " + _address);	
				return new List<UddiLookupResponse>();
			}

			Console.WriteLine("Dummy UDDI returning " + parameters.Identifier.GetAsString() + " from " + _address);
            UddiLookupResponse response = new UddiLookupResponse();
            response.EndpointAddress = new EndpointAddressHttp(_address);
			return new List<UddiLookupResponse>{response};
        }

        [XmlRoot(Namespace = dk.gov.oiosi.configuration.ConfigurationHandler.RaspNamespaceUrl)]
        public class AdvancedUddiDummyClientConfig {
            private List<Uri> _erroneousEndpoints = new List<Uri>();
            private Dictionary<Uri, List<Identifier>> _nonExistingRegistrations = new Dictionary<Uri, List<Identifier>>();

            [XmlIgnore]
            public List<Uri> ErroneousEndpoints {
                get { return _erroneousEndpoints; }
                set { _erroneousEndpoints = value; }
            }

            [XmlIgnore]
            public Dictionary<Uri, List<Identifier>> NonExistingRegistrations {
                get { return _nonExistingRegistrations; }
                set { _nonExistingRegistrations = value; }
            }
        }
    }
}
