using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using dk.gov.oiosi.xml.validator.configuration;

namespace dk.gov.oiosi.xml.converter.configuration {

    [XmlRoot("PreloadedConverterConfiguration", Namespace = "urn:dk.gov.oiosi.xml.validator.configuration")]
    public class PreloadedConverterXmlConfiguration : IPreloadedConverterConfiguration {
        private bool _closeSourceStream;
        private string _transformStylesheetPath;
        private SchemaValidatorXmlConfiguration _sourceSchemaConfiguration;
        private SchemaValidatorXmlConfiguration _resultSchemaConfiguration;
        private SchematronValidatorXmlConfiguration[] _sourceSchematronConfigurations;
        private SchematronValidatorXmlConfiguration[] _resultSchematronConfigurations;

        #region IPreloadedConverterConfiguration Members

        public bool CloseSourceStream {
            get { return _closeSourceStream; }
            set { _closeSourceStream = value; }
        }

        public string TransformStylesheetPath {
            get { return _transformStylesheetPath; }
            set { _transformStylesheetPath = value; }
        }

        [XmlIgnore]
        public ISchemaValidatorConfiguration SourceSchemaConfiguration {
            get { return _sourceSchemaConfiguration; }
        }

        [XmlIgnore]
        public ISchemaValidatorConfiguration ResultSchemaConfiguration {
            get { return _resultSchemaConfiguration; }
        }

        public IEnumerable<ISchematronValidatorConfiguration> GetSourceSchematronConfigurations() {
            return _sourceSchematronConfigurations;
        }

        public IEnumerable<ISchematronValidatorConfiguration> GetResultSchematronConfigurations() {
            return _resultSchematronConfigurations;
        }

        #endregion

        [XmlElement("SourceSchemaConfiguration")]
        public SchemaValidatorXmlConfiguration SourceSchemaXmlConfiguration {
            get { return _sourceSchemaConfiguration; }
            set { _sourceSchemaConfiguration = value; }
        }

        [XmlElement("ResultSchemaConfiguration")]
        public SchemaValidatorXmlConfiguration ResultSchemaXmlConfiguration {
            get { return _resultSchemaConfiguration; }
            set { _resultSchemaConfiguration = value; }
        }

        [XmlElement("SourceSchematronConfigurations")]
        public SchematronValidatorXmlConfiguration[] SourceSchematronXmlConfigurations {
            get { return _sourceSchematronConfigurations; }
            set { _sourceSchematronConfigurations = value; }
        }

        [XmlElement("ResultSchematronConfigurations")]
        public SchematronValidatorXmlConfiguration[] ResultSchematronConfigurations {
            get { return _resultSchematronConfigurations; }
            set { _resultSchematronConfigurations = value; }
        }
    }
}
