using System;
using System.Collections.Generic;
using System.Text;

using dk.gov.oiosi.xml.validator.configuration;

namespace dk.gov.oiosi.xml.converter.configuration {
    [Obsolete("No registered uses and is therefore marked for deletion. Please inform us of any use for this class/interface/method.")]
    public interface IPreloadedConverterConfiguration {
        [Obsolete("No registered uses and is therefore marked for deletion. Please inform us of any use for this class/interface/method.")]
        bool CloseSourceStream { get; }
        [Obsolete("No registered uses and is therefore marked for deletion. Please inform us of any use for this class/interface/method.")]
        string TransformStylesheetPath { get; }
        [Obsolete("No registered uses and is therefore marked for deletion. Please inform us of any use for this class/interface/method.")]
        ISchemaValidatorConfiguration SourceSchemaConfiguration { get; }
        [Obsolete("No registered uses and is therefore marked for deletion. Please inform us of any use for this class/interface/method.")]
        ISchemaValidatorConfiguration ResultSchemaConfiguration { get; }
        [Obsolete("No registered uses and is therefore marked for deletion. Please inform us of any use for this class/interface/method.")]
        IEnumerable<ISchematronValidatorConfiguration> GetSourceSchematronConfigurations();
        [Obsolete("No registered uses and is therefore marked for deletion. Please inform us of any use for this class/interface/method.")]
        IEnumerable<ISchematronValidatorConfiguration> GetResultSchematronConfigurations();
    }
}
