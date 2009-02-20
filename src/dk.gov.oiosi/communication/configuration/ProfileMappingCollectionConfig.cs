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
using dk.gov.oiosi.exception;

namespace dk.gov.oiosi.communication.configuration {

    /// <summary>
    /// A collection of ProfileMapping
    /// </summary>
    [System.Xml.Serialization.XmlRoot(Namespace = dk.gov.oiosi.configuration.ConfigurationHandler.RaspNamespaceUrl)]
    public class ProfileMappingCollectionConfig {
        private List<ProfileMapping> _profileMappings = new List<ProfileMapping>();

        /// <summary>
        /// A list OIOUBL Profiles, and the mapping between unique profile name and the 
        /// corresponding tModel GUID
        /// </summary>
        [XmlArray("ProfileMappingCollection")]
        public ProfileMapping[] ProfileMappings
        {
            get { return _profileMappings.ToArray(); }
            set { _profileMappings = new List<ProfileMapping>(value); } 
        }

        /// <summary>
        /// Adds a new ProfileMapping type to the configuration
        /// </summary>
        /// <param name="profileMapping">documenttype to add</param>
        public void AddProfileMapping(ProfileMapping profileMapping) {
            if (profileMapping == null)
                throw new NullArgumentException("profileMapping");
            if (ContainsProfileMappingByName(profileMapping.Name))
                throw new DocumentAllreadyAddedException(profileMapping.Name);
            _profileMappings.Add(profileMapping);
        }

        /// <summary>
        /// Clears the mapping list
        /// </summary>
        public void Clear() {
            if (_profileMappings != null) {
                _profileMappings.Clear();
            }
        }

        /// <summary>
        /// Returns true if mapping collection contains a mapping with profileMappingName
        /// </summary>
        /// <param name="profileMappingName"></param>
        /// <returns></returns>
        public bool ContainsProfileMappingByName(string profileMappingName)
        {
            Predicate<ProfileMapping> match = delegate(ProfileMapping current) {
                return current.Name.Equals(profileMappingName);
            };
            return _profileMappings.Exists(match);
        }

        /// <summary>
        /// Get a ProfileMapping type from the id
        /// </summary>
        /// <param name="profileMappingName"></param>
        /// <returns></returns>
        public ProfileMapping GetMapping(string profileMappingName)
        {
            Predicate<ProfileMapping> match = delegate(ProfileMapping current) {
                return current.Name.Equals(profileMappingName);
            };
            if (ContainsProfileMappingByName(profileMappingName))
                return _profileMappings.Find(match);

            return null;
        }

    }
}