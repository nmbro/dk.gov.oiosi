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
using System.Text;

using dk.gov.oiosi.oioublProfileLibrary;
using dk.gov.oiosi.communication.configuration;

namespace dk.gov.oiosi.raspProfile {

    /// <summary>
    /// Default profile definitions
    /// </summary>
    public class DefaultOioUblProfiles {

        /// <summary>
        /// The human readable name of the OIOXML electronic invoice profile
        /// </summary>
        public const string OioxmlElektroniskRegningProfileName = "OIOXML Elektronisk Regning";

        /// <summary>
        /// The human readable name of the NES basic billing profile
        /// </summary>
        public const string NesProfil5BasicBilling1_0 = "NES Profil 5 Basic Billing-1.0";

        /// <summary>
        /// The human readable name of the simple billing profile
        /// </summary>
        public const string ProcurementBilSim_1_0 = "Procurement-BilSim-1.0";

        /// <summary>
        /// The human readable name of the simple order and billing profile
        /// </summary>
        public const string ProcurementOrdSimRBilSim_1_0 = "Procurement-OrdSimR-BilSim-1.0";
        
        private DefaultDocumentTypes _documentTypes;

        /// <summary>
        /// Constructor
        /// </summary>
        public DefaultOioUblProfiles() {
            _documentTypes = new DefaultDocumentTypes();
        }

        /// <summary>
        /// Returns a list of all defined profiles
        /// </summary>
        /// <returns></returns>
        public IEnumerable<OioublProfile> GetProfiles() {
            List<OioublProfile> profiles = new List<OioublProfile>();
            profiles.Add(GetCustomerOioxmlElektroniskRegningProfile());
            profiles.Add(GetCustomerNesProfil5BasicBilling1_0());
            profiles.Add(GetSupplierProcurementBilSim_1_0());
            profiles.Add(GetCustomerProcurementBilSim_1_0());
            profiles.Add(GetSupplierProcurementOrdSimRBilSim_1_0());
            profiles.Add(GetCustomerProcurementOrdSimRBilSim_1_0());
            return profiles;
        }

        /// <summary>
        /// Returns the profile definition
        /// </summary>
        /// <returns>Returns the profile definition</returns>
        public OioublProfile GetCustomerOioxmlElektroniskRegningProfile() {
            string profileUddiId = "uddi:CD8A1434-AE29-4f6d-A26D-F0F25F2D3DA6";
            DocumentTypeConfig invoiceDocumentType = _documentTypes.InvoiceV07();
            DocumentTypeConfig creditNote07DocumentType = _documentTypes.CreditNoteV07();
            List<DocumentTypeConfig> profileDocumentTypes = new List<DocumentTypeConfig>();
            profileDocumentTypes.Add(invoiceDocumentType);
            profileDocumentTypes.Add(creditNote07DocumentType);
            OioublProfile profile = new OioublProfile(OioxmlElektroniskRegningProfileName, profileUddiId, OioublProfileRole.CustomerParty, profileDocumentTypes);
            return profile;
        }

        /// <summary>
        /// Returns the profile definition
        /// </summary>
        /// <returns>Returns the profile definition</returns>
        public OioublProfile GetCustomerNesProfil5BasicBilling1_0() {
            string profileUddiId = "uddi:AEE8B6DE-298F-4cbc-A96D-9AE8AED0AC31";
            DocumentTypeConfig invoiceDocumentType = _documentTypes.Invoice();
            List<DocumentTypeConfig> profileDocumentTypes = new List<DocumentTypeConfig>();
            profileDocumentTypes.Add(invoiceDocumentType);
            OioublProfile profile = new OioublProfile(NesProfil5BasicBilling1_0, profileUddiId, OioublProfileRole.CustomerParty, profileDocumentTypes);
            return profile;
        }

        /// <summary>
        /// Returns the profile definition
        /// </summary>
        /// <returns>Returns the profile definition</returns>
        public OioublProfile GetSupplierProcurementBilSim_1_0() {
            string profileUddiId = "uddi:362229AC-B657-452a-B8F8-C93E62C670FF";
            DocumentTypeConfig applicationResponseDocumentType = _documentTypes.ApplicationResponse();
            List<DocumentTypeConfig> profileDocumentTypes = new List<DocumentTypeConfig>();
            profileDocumentTypes.Add(applicationResponseDocumentType);
            OioublProfile profile = new OioublProfile(ProcurementBilSim_1_0, profileUddiId, OioublProfileRole.SupplierParty, profileDocumentTypes);
            return profile;
        }

        /// <summary>
        /// Returns the profile definition
        /// </summary>
        /// <returns>Returns the profile definition</returns>
        public OioublProfile GetCustomerProcurementBilSim_1_0() {
            string profileUddiId = "uddi:362229AC-B657-452a-B8F8-C93E62C670FF";
            DocumentTypeConfig invoiceDocumentType = _documentTypes.Invoice();
            DocumentTypeConfig creditNoteDocumentType = _documentTypes.CreditNote();
            DocumentTypeConfig reminderDocumentType = _documentTypes.Reminder();
            List<DocumentTypeConfig> profileDocumentTypes = new List<DocumentTypeConfig>();
            profileDocumentTypes.Add(invoiceDocumentType);
            profileDocumentTypes.Add(creditNoteDocumentType);
            profileDocumentTypes.Add(reminderDocumentType);
            OioublProfile profile = new OioublProfile(ProcurementBilSim_1_0, profileUddiId, OioublProfileRole.CustomerParty, profileDocumentTypes);
            return profile;
        }

        /// <summary>
        /// Returns the profile definition
        /// </summary>
        /// <returns>Returns the profile definition</returns>
        public OioublProfile GetSupplierProcurementOrdSimRBilSim_1_0() {
            string profileUddiId = "uddi:EBABEE8B-A5D3-4dc9-B976-3AAFF9A4E855";
            DocumentTypeConfig orderDocumentType = _documentTypes.Order();
            DocumentTypeConfig applicationResponseDocumentType = _documentTypes.ApplicationResponse();
            List<DocumentTypeConfig> profileDocumentTypes = new List<DocumentTypeConfig>();
            profileDocumentTypes.Add(orderDocumentType);
            profileDocumentTypes.Add(applicationResponseDocumentType);
            OioublProfile profile = new OioublProfile(ProcurementOrdSimRBilSim_1_0, profileUddiId, OioublProfileRole.SupplierParty, profileDocumentTypes);
            return profile;
        }

        /// <summary>
        /// Returns the profile definition
        /// </summary>
        /// <returns>Returns the profile definition</returns>
        public OioublProfile GetCustomerProcurementOrdSimRBilSim_1_0() {
            string profileUddiId = "uddi:EBABEE8B-A5D3-4dc9-B976-3AAFF9A4E855";
            DocumentTypeConfig orderResponseSimpleDocumentType = _documentTypes.OrderResponseSimple();
            DocumentTypeConfig invoiceDocumentType = _documentTypes.Invoice();
            DocumentTypeConfig creditNoteDocumentType = _documentTypes.CreditNote();
            DocumentTypeConfig reminderDocumentType = _documentTypes.Reminder();
            List<DocumentTypeConfig> profileDocumentTypes = new List<DocumentTypeConfig>();
            profileDocumentTypes.Add(orderResponseSimpleDocumentType);
            profileDocumentTypes.Add(invoiceDocumentType);
            profileDocumentTypes.Add(creditNoteDocumentType);
            profileDocumentTypes.Add(reminderDocumentType);
            OioublProfile profile = new OioublProfile(ProcurementOrdSimRBilSim_1_0, profileUddiId, OioublProfileRole.CustomerParty, profileDocumentTypes);
            return profile;
        }
    }
}
