﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18444
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace dk.gov.oiosi.xml {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class ErrorMessages {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal ErrorMessages() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("dk.gov.oiosi.xml.ErrorMessages", typeof(ErrorMessages).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Søgning efter dokumenttype ud fra XML dokumentet med rodnavn &quot;[rootname]&quot; og namespace &quot;[namespace]&quot; gav flere resultater..
        /// </summary>
        internal static string dk_gov_oiosi_xml_documentType_MoreThanOneDocumentTypeFoundFromXmlDocumentException {
            get {
                return ResourceManager.GetString("dk_gov_oiosi_xml_documentType_MoreThanOneDocumentTypeFoundFromXmlDocumentExceptio" +
                        "n", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Søgning efter dokumenttype ud fra XML dokumentet med rodnavn &quot;[rootname]&quot; og rodnamespace &quot;[rootnamespace]&quot; gav ingen resultater..
        /// </summary>
        internal static string dk_gov_oiosi_xml_documentType_NoDocumentTypeFoundFromXmlDocumentException {
            get {
                return ResourceManager.GetString("dk_gov_oiosi_xml_documentType_NoDocumentTypeFoundFromXmlDocumentException", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Søgning efter dokumenttype ud fra XML dokumentet med rodnavn &quot;[rootname]&quot; og rodnamespace &quot;[rootnamespace]&quot; fejlede..
        /// </summary>
        internal static string dk_gov_oiosi_xml_documentType_SearchForDocumentTypeFromXmlDocumentFailedException {
            get {
                return ResourceManager.GetString("dk_gov_oiosi_xml_documentType_SearchForDocumentTypeFromXmlDocumentFailedException" +
                        "", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Der er sket en general XML fejl..
        /// </summary>
        internal static string dk_gov_oiosi_xml_XmlException {
            get {
                return ResourceManager.GetString("dk_gov_oiosi_xml_XmlException", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Udtrykket &apos;[xpath]&apos; returnerede ingen elementer eller attributter..
        /// </summary>
        internal static string dk_gov_oiosi_xml_xpath_NoXPathResultsException {
            get {
                return ResourceManager.GetString("dk_gov_oiosi_xml_xpath_NoXPathResultsException", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Xpath-udtrykket &apos;[xpath]&apos; returnerede for mange resultater ([results])  found)..
        /// </summary>
        internal static string dk_gov_oiosi_xml_xpath_TooManyXpathResultsException {
            get {
                return ResourceManager.GetString("dk_gov_oiosi_xml_xpath_TooManyXpathResultsException", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Xpath-udtrykket &apos;[xpath]&apos;s  længde er for kort..
        /// </summary>
        internal static string dk_gov_oiosi_xml_xpath_XPathSizeTooSmallException {
            get {
                return ResourceManager.GetString("dk_gov_oiosi_xml_xpath_XPathSizeTooSmallException", resourceCulture);
            }
        }
    }
}
