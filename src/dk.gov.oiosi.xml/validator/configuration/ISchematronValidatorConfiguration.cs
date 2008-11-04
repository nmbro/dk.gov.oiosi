using System;
using System.Collections.Generic;
using System.Text;

using dk.gov.oiosi.xml.converter.configuration;

namespace dk.gov.oiosi.xml.validator.configuration {
    public interface ISchematronValidatorConfiguration {
        uint MinSizeForErrors { get; }
        string ErrorXPath { get; }
        string ErrorMessageXPath { get; }
        IPreloadedConverterConfiguration ConverterConfiguration { get; }
    }
}
