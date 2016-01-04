using System;
using System.Collections.Generic;
using System.Text;

using dk.gov.oiosi.xml.converter.configuration;

namespace dk.gov.oiosi.xml.validator.configuration {
    [Obsolete("No registered uses and is therefore marked for deletion. Please inform us of any use for this class/interface/method.")]
    public interface ISchematronValidatorConfiguration
    {
        [Obsolete("No registered uses and is therefore marked for deletion. Please inform us of any use for this class/interface/method.")]
        uint MinSizeForErrors { get; }
        [Obsolete("No registered uses and is therefore marked for deletion. Please inform us of any use for this class/interface/method.")]
        string ErrorXPath { get; }
        [Obsolete("No registered uses and is therefore marked for deletion. Please inform us of any use for this class/interface/method.")]
        string ErrorMessageXPath { get; }
        [Obsolete("No registered uses and is therefore marked for deletion. Please inform us of any use for this class/interface/method.")]
        IPreloadedConverterConfiguration ConverterConfiguration { get; }
    }
}
