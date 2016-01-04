using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using dk.gov.oiosi.xml.converter.configuration;

namespace dk.gov.oiosi.xml.validator.configuration {

    [Obsolete("No registered uses and is therefore marked for deletion. Please inform us of any use for this class/interface/method.")]
    [XmlRoot("SchematronValidatorConfiguration", Namespace = "dk.gov.oiosi.xml.validator.configuration")]
    public class SchematronValidatorXmlConfiguration : ISchematronValidatorConfiguration {
        private uint _minSizeForErrors;
        private string _errorXPath;
        private string _errorMessageXPath;
        private PreloadedConverterXmlConfiguration _converterConfiguration;

        #region ISchematronValidatorConfiguration Members

        [Obsolete("No registered uses and is therefore marked for deletion. Please inform us of any use for this class/interface/method.")]
        public uint MinSizeForErrors
        {
            get { return _minSizeForErrors; }
            set { _minSizeForErrors = value; }
        }

        [Obsolete("No registered uses and is therefore marked for deletion. Please inform us of any use for this class/interface/method.")]
        public string ErrorXPath
        {
            get { return _errorXPath; }
            set { _errorXPath = value; }
        }

        [Obsolete("No registered uses and is therefore marked for deletion. Please inform us of any use for this class/interface/method.")]
        public string ErrorMessageXPath
        {
            get { return _errorMessageXPath; }
            set { _errorMessageXPath = value; }
        }

        [XmlIgnore]
        public IPreloadedConverterConfiguration ConverterConfiguration {
            get { return _converterConfiguration; }
        }

        #endregion

        [Obsolete("No registered uses and is therefore marked for deletion. Please inform us of any use for this class/interface/method.")]
        [XmlElement("ConverterConfiguration")]
        public PreloadedConverterXmlConfiguration ConverterXmlConfiguration {
            get { return _converterConfiguration; }
            set { _converterConfiguration = value; }
        }


    }
}
