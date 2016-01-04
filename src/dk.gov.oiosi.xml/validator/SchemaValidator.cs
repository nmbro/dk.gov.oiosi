using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.Schema;

using dk.gov.oiosi.xml.validator.configuration;

namespace dk.gov.oiosi.xml.validator {
    [Obsolete("No registered uses and is therefore marked for deletion. Please inform us of any use for this class/interface/method.")]
    public class SchemaValidator : IValidator
    {
        private XmlSchema _schema;

        [Obsolete("No registered uses and is therefore marked for deletion. Please inform us of any use for this class/interface/method.")]
        public SchemaValidator(string path)
        {
            InitSchema(path);
        }

        [Obsolete("No registered uses and is therefore marked for deletion. Please inform us of any use for this class/interface/method.")]
        public SchemaValidator(ISchemaValidatorConfiguration configuration)
        {
            if (configuration == null) throw new ArgumentNullException("configuration");
            InitSchema(configuration.Path);
        }

        #region IValidator Members

        [Obsolete("No registered uses and is therefore marked for deletion. Please inform us of any use for this class/interface/method.")]
        public void Validate(Stream source)
        {
            if (source == null) throw new ArgumentNullException("source");
            XmlReaderSettings settings = new XmlReaderSettings();
                settings.ProhibitDtd = true;
            List<string> errors = new List<string>();
            ValidationEventHandler validationEventHandler = delegate(object sender, ValidationEventArgs e) { errors.Add(e.Message); };
            settings.ValidationEventHandler += new ValidationEventHandler(validationEventHandler);
            settings.ValidationType = ValidationType.Schema;
            settings.Schemas.Add(_schema);
            XmlReader reader = XmlReader.Create(source, settings);
            while (reader.Read()) { }
            if (errors.Count > 0) throw new SchemaValidationFailedException("Schema validation failed with: " + errors[0]);
        }


        #endregion

        [Obsolete("No registered uses and is therefore marked for deletion. Please inform us of any use for this class/interface/method.")]
        private void InitSchema(string path)
        {
            if (string.IsNullOrEmpty(path)) throw new ArgumentNullException("path");
            //Tries to load the transformation path, if the path is not absolute and the file
            //does not exists then try with the appdomain base directory as root folder
            string schemaPath = path;
            if (!Path.IsPathRooted(schemaPath) && !File.Exists(schemaPath)) {
                schemaPath = AppDomain.CurrentDomain.BaseDirectory + Path.DirectorySeparatorChar + schemaPath;
                schemaPath = Path.GetFullPath(schemaPath);
            }
            if (!File.Exists(schemaPath)) throw new FileNotFoundException("Schemafile does not exists " + path);
            using (FileStream fs = File.OpenRead(schemaPath)) {
                _schema = XmlSchema.Read(fs, null);
            }
        }
    }
}
