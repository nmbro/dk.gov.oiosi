using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

using dk.gov.oiosi.xml.converter;
using dk.gov.oiosi.xml.converter.configuration;
using dk.gov.oiosi.xml.validator.configuration;
using System.IO;

namespace dk.gov.oiosi.xml.validator {
    public class SchematronValidator : IValidator {
        private uint _minSizeForErrors;
        private string _errorXPath;
        private string _errorMessageXPath;
        private PreloadedConverter _converter;


        public SchematronValidator(ISchematronValidatorConfiguration configuration) {
            if (configuration == null) throw new ArgumentNullException("configuration");
            if (configuration.ErrorMessageXPath == null) throw new ArgumentNullException("configuration.ErrorMessageXPath");
            if (configuration.ErrorXPath == null) throw new ArgumentNullException("configuration.ErrorXPath");
            _minSizeForErrors = configuration.MinSizeForErrors;
            _errorMessageXPath = configuration.ErrorMessageXPath;
            _errorXPath = configuration.ErrorXPath;
            _converter = new PreloadedConverter(configuration.ConverterConfiguration);
        }

        #region IValidator Members

        public void Validate(Stream source) {
            Stream result = _converter.ValidatedConvert(source);
            //The resulting stream must be over this size to assume any errors
            if (result.Length < _minSizeForErrors) return;
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(result);
            XmlNodeList errorNodes = xmlDocument.SelectNodes(_errorXPath);
            if (errorNodes.Count < 1) return;
            XmlNodeList errorMessageNodes = xmlDocument.SelectNodes(_errorMessageXPath);
            string firstErrorMessage = errorMessageNodes[0].InnerText;
            throw new SchematronValidationFailedException("Schematron validation failed with following message: " + firstErrorMessage);
        }

        #endregion

    }
}
