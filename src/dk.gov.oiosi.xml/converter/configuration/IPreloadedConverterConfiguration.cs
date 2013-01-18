using System;
using System.Collections.Generic;
using System.Text;

using dk.gov.oiosi.xml.validator.configuration;

namespace dk.gov.oiosi.xml.converter.configuration {
    public interface IPreloadedConverterConfiguration {
        bool CloseSourceStream { get; }
        string TransformStylesheetPath { get; }
        ISchemaValidatorConfiguration SourceSchemaConfiguration { get; }
        ISchemaValidatorConfiguration ResultSchemaConfiguration { get; }
        IEnumerable<ISchematronValidatorConfiguration> GetSourceSchematronConfigurations();
        IEnumerable<ISchematronValidatorConfiguration> GetResultSchematronConfigurations();
    }
}
