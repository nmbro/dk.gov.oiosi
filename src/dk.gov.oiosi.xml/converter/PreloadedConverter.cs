using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Xsl;

using dk.gov.oiosi.xml.converter.configuration;
using dk.gov.oiosi.xml.validator;
using dk.gov.oiosi.xml.validator.configuration;

namespace dk.gov.oiosi.xml.converter {
    /// <summary>
    /// Class that can convert XML. It can also validate both the source XML and
    /// the result XML, before returning the result.
    /// </summary>
    public class PreloadedConverter {
        private bool _closeSourceStream;
        private XslCompiledTransform _transform;
        private List<IValidator> _resultValidators = new List<IValidator>();
        private List<IValidator> _sourceValidators = new List<IValidator>();

        /// <summary>
        /// Default constructor that initiated a configuration from app.config or web.config.
        /// </summary>
        public PreloadedConverter() {
            IPreloadedConverterConfiguration configuration = (IPreloadedConverterConfiguration)ConfigurationManager.GetSection(PreloadedConverterAppConfiguration.PreloadedConverterConfigurationName);
            InitFromConfiguration(configuration);
        }

        /// <summary>
        /// Constructor that takes the configuration the converter uses as parameter
        /// </summary>
        /// <param name="configuration"></param>
        public PreloadedConverter(IPreloadedConverterConfiguration configuration) {
            if (configuration == null) throw new ArgumentNullException("configuration");
            InitFromConfiguration(configuration);
        }

        /// <summary>
        /// Constructor that takes the XSL compiled transform and the validators
        /// as parameter.
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="resultValidators"></param>
        /// <param name="sourceValidators"></param>
        public PreloadedConverter(bool closeSourceStream, XslCompiledTransform transform, List<IValidator> resultValidators, List<IValidator> sourceValidators) {
            if (transform == null) throw new ArgumentNullException("transform");
            if (resultValidators == null) throw new ArgumentNullException("resultValidators");
            if (sourceValidators == null) throw new ArgumentNullException("sourceValidators");
            _closeSourceStream = closeSourceStream;
            _transform = transform;
            _resultValidators = resultValidators;
            _sourceValidators = sourceValidators;
        }

        /// <summary>
        /// Converts the xml structure to another xml structure using a xslt 
        /// stylesheet.
        /// Further it validates whether the source and result is correct to 
        /// the given schemas and stylesheets.
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public Stream ValidatedConvert(Stream source) {
            Stream result;
            ValidateSource(source);
            result = Convert(source);
            ValidateResult(result);
            return result;
        }

        /// <summary>
        /// Converts the xml structure to another xml structure using a xslt 
        /// stylesheet
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public Stream Convert(Stream source) {
            try {
                source.Position = 0;
                MemoryStream result = new MemoryStream();
                XmlReader reader = XmlReader.Create(source);
                XmlWriter writer = XmlWriter.Create(result);
                _transform.Transform(reader, (XsltArgumentList)null, writer);
                result.Position = 0;
                if (_closeSourceStream) source.Close();
                return result;
            }
            catch (Exception ex) {
                throw new ConverterException("Convertion failed", ex);
            }
        }

        private void InitFromConfiguration(IPreloadedConverterConfiguration configuration) {
            if (configuration.TransformStylesheetPath == null) throw new ArgumentNullException("configuration.TransformStylesheetPath");
            try {
                XsltSettings xsltSettings = new XsltSettings(true, true);
                XmlUrlResolver resolver = new XmlUrlResolver();

                _closeSourceStream = configuration.CloseSourceStream;
                _transform = new XslCompiledTransform();
                //Tries to load the transformation path, if the path is not absolute and the file
                //does not exists then try with the appdomain base directory as root folder
                string transformationPath = configuration.TransformStylesheetPath;
                if (!Path.IsPathRooted(transformationPath) && !File.Exists(transformationPath)) {
                    transformationPath = AppDomain.CurrentDomain.BaseDirectory + Path.DirectorySeparatorChar + transformationPath;
                    transformationPath = Path.GetFullPath(transformationPath);
                }

                _transform.Load(transformationPath, xsltSettings, resolver);

                IValidator resultSchemaValidator = null;
                if (TryInitSchemaValidator(configuration.ResultSchemaConfiguration, out resultSchemaValidator)) {
                    _resultValidators.Add(resultSchemaValidator);
                }

                IValidator sourceSchemaValidator = null;
                if (TryInitSchemaValidator(configuration.SourceSchemaConfiguration, out sourceSchemaValidator)) {
                    _sourceValidators.Add(sourceSchemaValidator);
                }

                IEnumerable<ISchematronValidatorConfiguration> resultSchematrons = configuration.GetResultSchematronConfigurations();
                if (resultSchematrons != null) {
                    foreach (ISchematronValidatorConfiguration schematronConfiguration in resultSchematrons) {
                        SchematronValidator validator = new SchematronValidator(schematronConfiguration);
                        _resultValidators.Add(validator);
                    }
                }

                IEnumerable<ISchematronValidatorConfiguration> sourceSchematrons = configuration.GetSourceSchematronConfigurations();
                if (sourceSchematrons != null) {
                    foreach (ISchematronValidatorConfiguration schematronConfiguration in sourceSchematrons) {
                        SchematronValidator validator = new SchematronValidator(schematronConfiguration);
                        _sourceValidators.Add(validator);
                    }
                }
            }
            catch (Exception ex) {
                throw new ConverterException("Initiation of the Converter failed, used stylesheetpath=" + configuration.TransformStylesheetPath, ex);
            }
        }

        private bool TryInitSchemaValidator(ISchemaValidatorConfiguration configuration, out IValidator schemaValidator) {
            schemaValidator = null;
            if (configuration == null) return false;
            if (string.IsNullOrEmpty(configuration.Path)) return false;
            schemaValidator = new SchemaValidator(configuration);
            return true;
        }
        private void ValidateSource(Stream source) {
            try {
                Validate(source, _sourceValidators);
            }
            catch (Exception ex) {
                throw new ConverterException("Source validation failed.", ex);
            }
        }
        private void ValidateResult(Stream result) {
            try {
                Validate(result, _resultValidators);
            }
            catch (Exception ex) {
                throw new ConverterException("Result validation failed.", ex);
            }
        }
        private void Validate(Stream source, IEnumerable<IValidator> validators) {
            source.Position = 0;
            foreach (IValidator validator in validators) {
                validator.Validate(source);
            }
            source.Position = 0;
        }
    }
}
