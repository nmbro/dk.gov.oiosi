using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using dk.gov.oiosi.xml.validator.configuration;

namespace dk.gov.oiosi.xml.converter.configuration {
    [Obsolete("No registered uses and is therefore marked for deletion. Please inform us of any use for this class/interface/method.")]
    [XmlRoot("PreloadedConverterConfiguration", Namespace = "urn:dk.gov.oiosi.xml.validator.configuration")]
    public class PreloadedConverterXmlConfiguration : IPreloadedConverterConfiguration {
        private bool _closeSourceStream;
        private string _transformStylesheetPath;
        private SchemaValidatorXmlConfiguration _sourceSchemaConfiguration;
        private SchemaValidatorXmlConfiguration _resultSchemaConfiguration;
        private SchematronValidatorXmlConfiguration[] _sourceSchematronConfigurations;
        private SchematronValidatorXmlConfiguration[] _resultSchematronConfigurations;

        #region IPreloadedConverterConfiguration Members
        [Obsolete("No registered uses and is therefore marked for deletion. Please inform us of any use for this class/interface/method.")]
        public bool CloseSourceStream {
            get { return _closeSourceStream; }
            set { _closeSourceStream = value; }
        }
        [Obsolete("No registered uses and is therefore marked for deletion. Please inform us of any use for this class/interface/method.")]
        public string TransformStylesheetPath {
            get { return _transformStylesheetPath; }
            set { _transformStylesheetPath = value; }
        }
        [Obsolete("No registered uses and is therefore marked for deletion. Please inform us of any use for this class/interface/method.")]
        [XmlIgnore]
        public ISchemaValidatorConfiguration SourceSchemaConfiguration {
            get { return _sourceSchemaConfiguration; }
        }
        [Obsolete("No registered uses and is therefore marked for deletion. Please inform us of any use for this class/interface/method.")]
        [XmlIgnore]
        public ISchemaValidatorConfiguration ResultSchemaConfiguration {
            get { return _resultSchemaConfiguration; }
        }
        [Obsolete("No registered uses and is therefore marked for deletion. Please inform us of any use for this class/interface/method.")]
        public IEnumerable<ISchematronValidatorConfiguration> GetSourceSchematronConfigurations() {
            return _sourceSchematronConfigurations;
        }
        [Obsolete("No registered uses and is therefore marked for deletion. Please inform us of any use for this class/interface/method.")]
        public IEnumerable<ISchematronValidatorConfiguration> GetResultSchematronConfigurations() {
            return _resultSchematronConfigurations;
        }

        #endregion
        [Obsolete("No registered uses and is therefore marked for deletion. Please inform us of any use for this class/interface/method.")]
        [XmlElement("SourceSchemaConfiguration")]
        public SchemaValidatorXmlConfiguration SourceSchemaXmlConfiguration {
            get { return _sourceSchemaConfiguration; }
            set { _sourceSchemaConfiguration = value; }
        }
        [Obsolete("No registered uses and is therefore marked for deletion. Please inform us of any use for this class/interface/method.")]
        [XmlElement("ResultSchemaConfiguration")]
        public SchemaValidatorXmlConfiguration ResultSchemaXmlConfiguration {
            get { return _resultSchemaConfiguration; }
            set { _resultSchemaConfiguration = value; }
        }
        [Obsolete("No registered uses and is therefore marked for deletion. Please inform us of any use for this class/interface/method.")]
        [XmlElement("SourceSchematronConfigurations")]
        public SchematronValidatorXmlConfiguration[] SourceSchematronXmlConfigurations {
            get { return _sourceSchematronConfigurations; }
            set { _sourceSchematronConfigurations = value; }
        }
        [Obsolete("No registered uses and is therefore marked for deletion. Please inform us of any use for this class/interface/method.")]
        [XmlElement("ResultSchematronConfigurations")]
        public SchematronValidatorXmlConfiguration[] ResultSchematronConfigurations {
            get { return _resultSchematronConfigurations; }
            set { _resultSchematronConfigurations = value; }
        }
    }
}
