﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.1433
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace dk.gov.oiosi.extension.wcf.EmailTransport {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "2.0.0.0")]
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
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("dk.gov.oiosi.extension.wcf.EmailTransport.ErrorMessages", typeof(ErrorMessages).Assembly);
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
        ///   Looks up a localized string similar to Kunne ikke finde email binding elementet..
        /// </summary>
        internal static string dk_gov_oiosi_extension_wcf_EmailTransport_EmailBindingElemenNotFoundtException {
            get {
                return ResourceManager.GetString("dk_gov_oiosi_extension_wcf_EmailTransport_EmailBindingElemenNotFoundtException", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Kunne ikke sende svaret.
        /// </summary>
        internal static string dk_gov_oiosi_extension_wcf_EmailTransport_EmailReplyCouldNotBeSentException {
            get {
                return ResourceManager.GetString("dk_gov_oiosi_extension_wcf_EmailTransport_EmailReplyCouldNotBeSentException", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Der blev ikke modtaget noget svar.
        /// </summary>
        internal static string dk_gov_oiosi_extension_wcf_EmailTransport_EmailResponseNotGottenException {
            get {
                return ResourceManager.GetString("dk_gov_oiosi_extension_wcf_EmailTransport_EmailResponseNotGottenException", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to En kanal af typen [type] kunne ikke instantieres.
        /// </summary>
        internal static string dk_gov_oiosi_extension_wcf_EmailTransport_EmailTransportChannelCouldNotBeBuiltException {
            get {
                return ResourceManager.GetString("dk_gov_oiosi_extension_wcf_EmailTransport_EmailTransportChannelCouldNotBeBuiltExc" +
                        "eption", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Der opstod en fejl i transportlaget.
        /// </summary>
        internal static string dk_gov_oiosi_extension_wcf_EmailTransport_EmailTransportException {
            get {
                return ResourceManager.GetString("dk_gov_oiosi_extension_wcf_EmailTransport_EmailTransportException", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Konfigurationen indeholdt ikke nogen valid reference til en [type] implementation. Dette kan skyldes at reference mangler..
        /// </summary>
        internal static string dk_gov_oiosi_extension_wcf_EmailTransport_MailboxImplementationCouldNotBeFoundException {
            get {
                return ResourceManager.GetString("dk_gov_oiosi_extension_wcf_EmailTransport_MailboxImplementationCouldNotBeFoundExc" +
                        "eption", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to En instans af Mailbox-implementationen &apos;[type]&apos; kunne ikke skabes. Dette kan skyldes at der mangler en reference, eller at klassen ikke implementerer en default constructor..
        /// </summary>
        internal static string dk_gov_oiosi_extension_wcf_EmailTransport_MailboxImplementationCouldNotBeInstantiated {
            get {
                return ResourceManager.GetString("dk_gov_oiosi_extension_wcf_EmailTransport_MailboxImplementationCouldNotBeInstanti" +
                        "ated", resourceCulture);
            }
        }
    }
}