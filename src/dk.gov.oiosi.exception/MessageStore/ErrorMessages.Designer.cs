﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace dk.gov.oiosi.exception.MessageStore {
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
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("dk.gov.oiosi.exception.MessageStore.ErrorMessages", typeof(ErrorMessages).Assembly);
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
        ///   Looks up a localized string similar to The string &quot;[stringDescription]&quot; was null..
        /// </summary>
        internal static string dk_gov_oiosi_exception_EmptyStringException {
            get {
                return ResourceManager.GetString("dk_gov_oiosi_exception_EmptyStringException", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The argument &quot;[argument]&quot; was null..
        /// </summary>
        internal static string dk_gov_oiosi_exception_NullArgumentException {
            get {
                return ResourceManager.GetString("dk_gov_oiosi_exception_NullArgumentException", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The string argument &quot;[argument]&quot; was null or empty..
        /// </summary>
        internal static string dk_gov_oiosi_exception_NullOrEmptyArgumentException {
            get {
                return ResourceManager.GetString("dk_gov_oiosi_exception_NullOrEmptyArgumentException", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The string argument &quot;[argument]&quot; had unexpected number of characters. Expected &quot;[characters]&quot; characters..
        /// </summary>
        internal static string dk_gov_oiosi_exception_UnexpectedNumberOfCharactersException {
            get {
                return ResourceManager.GetString("dk_gov_oiosi_exception_UnexpectedNumberOfCharactersException", resourceCulture);
            }
        }
    }
}
