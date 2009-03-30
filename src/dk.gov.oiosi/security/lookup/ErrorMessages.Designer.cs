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
  * Portions created by Accenture and Avanade are Copyright (C) 2009
  * Danish National IT and Telecom Agency (http://www.itst.dk). 
  * All Rights Reserved.
  *
  * Contributor(s):
  *   Gert Sylvest, Avanade
  *   Jesper Jensen, Avanade
  *   Ramzi Fadel, Avanade
  *   Patrik Johansson, Accenture
  *   Dennis S�gaard, Accenture
  *   Christian Pedersen, Accenture
  *   Martin Bentzen, Accenture
  *   Mikkel Hippe Brun, ITST
  *   Finn Hartmann Jordal, ITST
  *   Christian Lanng, ITST
  *
  */
namespace dk.gov.oiosi.security.lookup {
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "2.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCode()]
    [global::System.Runtime.CompilerServices.CompilerGenerated()]
    internal class ErrorMessages {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal ErrorMessages() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("dk.gov.oiosi.security.lookup.ErrorMessages", typeof(ErrorMessages).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Kunne ikke finde det tilh�rende certifikat.
        /// </summary>
        internal static string RASP_CertificateLookup_CertificateNotFoundException {
            get {
                return ResourceManager.GetString("RASP_CertificateLookup_CertificateNotFoundException", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Kunne ikke forbinde til LDAP serveren p� adressen &quot;[address]&quot; og port &quot;[port]&quot;.
        /// </summary>
        internal static string RASP_CertificateLookup_Ldap_ConnectingToLdapServerFailedException {
            get {
                return ResourceManager.GetString("RASP_CertificateLookup_Ldap_ConnectingToLdapServerFailedException", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Kunne ikke converter certifikat data fra LDAP sserveren til et certifikat.
        /// </summary>
        internal static string RASP_CertificateLookup_Ldap_ConvertingSearchResultToCertificateFailedException {
            get {
                return ResourceManager.GetString("RASP_CertificateLookup_Ldap_ConvertingSearchResultToCertificateFailedException", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Kunne ikke finde et eller flere korrekte match til det regul�re udtryk &quot;[patterns]&quot; i subject strengen &quot;[subject]&quot;..
        /// </summary>
        internal static string RASP_CertificateLookup_Ldap_PatternsDoesNotMatchException {
            get {
                return ResourceManager.GetString("RASP_CertificateLookup_Ldap_PatternsDoesNotMatchException", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Der blev fundet flere certifkater ud fra kriterierne.
        /// </summary>
        internal static string RASP_CertificateLookup_MultipleCertificatesFoundException {
            get {
                return ResourceManager.GetString("RASP_CertificateLookup_MultipleCertificatesFoundException", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to S�gningen efter et certifikat fejlede.
        /// </summary>
        internal static string RASP_CertificateLookup_SearchFailedException {
            get {
                return ResourceManager.GetString("RASP_CertificateLookup_SearchFailedException", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Certifikatet er udl�bet den &quot;[expiredate]&quot;.
        /// </summary>
        internal static string RASP_CertificateLookup_Validation_CertificateExpiredException {
            get {
                return ResourceManager.GetString("RASP_CertificateLookup_Validation_CertificateExpiredException", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Certifikatet fejlede k�devalideringen med status &quot;[chainstatus]&quot;.
        /// </summary>
        internal static string RASP_CertificateLookup_Validation_CertificateFailedChainValidationException {
            get {
                return ResourceManager.GetString("RASP_CertificateLookup_Validation_CertificateFailedChainValidationException", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Certifikatet er ikke aktivt endnu, aktiveres den &quot;[activedate]&quot;.
        /// </summary>
        internal static string RASP_CertificateLookup_Validation_CertificateNotActivedException {
            get {
                return ResourceManager.GetString("RASP_CertificateLookup_Validation_CertificateNotActivedException", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Certifikatet er ikke i korrekt format.
        /// </summary>
        internal static string RASP_CertificateLookup_Validation_CertificateNotInCorrectFormatException {
            get {
                return ResourceManager.GetString("RASP_CertificateLookup_Validation_CertificateNotInCorrectFormatException", resourceCulture);
            }
        }
    }
}
