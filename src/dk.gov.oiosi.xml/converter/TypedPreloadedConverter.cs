using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Xsl;
using dk.gov.oiosi.xml.validator;
using dk.gov.oiosi.xml.converter.configuration;
using System.Configuration;
using System.IO;

namespace dk.gov.oiosi.xml.converter {
    [Obsolete("No registered uses and is therefore marked for deletion. Please inform us of any use for this class/interface/method.")]
    public class TypedPreloadedConverter<ALPHA, BETA>
        where ALPHA : class
        where BETA : class
    {
        private PreloadedConverter _innerConverter;
        private Serializer<ALPHA> _alphaSerializer;
        private Serializer<BETA> _betaSerializer;

        /// <summary>
        /// Default constructor that initiated a configuration from app.config or web.config.
        /// </summary>
        [Obsolete("No registered uses and is therefore marked for deletion. Please inform us of any use for this class/interface/method.")]
        public TypedPreloadedConverter()
        {
            Init();
            _innerConverter = new PreloadedConverter();
        }

        /// <summary>
        /// Constructor that takes the configuration the converter uses as parameter
        /// </summary>
        /// <param name="configuration"></param>
        [Obsolete("No registered uses and is therefore marked for deletion. Please inform us of any use for this class/interface/method.")]
        public TypedPreloadedConverter(IPreloadedConverterConfiguration configuration)
        {
            Init();
            _innerConverter = new PreloadedConverter(configuration);
        }

        /// <summary>
        /// Constructor that takes the XSL compiled transform and the validators
        /// as parameter.
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="resultValidators"></param>
        /// <param name="sourceValidators"></param>
        [Obsolete("No registered uses and is therefore marked for deletion. Please inform us of any use for this class/interface/method.")]
        public TypedPreloadedConverter(bool closeSourceStream, XslCompiledTransform transform, List<IValidator> resultValidators, List<IValidator> sourceValidators)
        {
            Init();
            _innerConverter = new PreloadedConverter(closeSourceStream, transform, resultValidators, sourceValidators);
        }

        /// <summary>
        /// Converts the xml structure to another xml structure using a xslt 
        /// stylesheet.
        /// Further it validates whether the source and result is correct to 
        /// the given schemas and stylesheets.
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        [Obsolete("No registered uses and is therefore marked for deletion. Please inform us of any use for this class/interface/method.")]
        public BETA ValidatedConvert(ALPHA source)
        {
            using (MemoryStream alphaStream = new MemoryStream()) {
                _alphaSerializer.Serialise(source, alphaStream);
                alphaStream.Position = 0;
                using (Stream betaStream = _innerConverter.Convert(alphaStream)) {
                    return _betaSerializer.Deserialise(betaStream);
                }
            }
        }

        /// <summary>
        /// Converts the xml structure to another xml structure using a xslt 
        /// stylesheet
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        [Obsolete("No registered uses and is therefore marked for deletion. Please inform us of any use for this class/interface/method.")]
        public BETA Convert(ALPHA source)
        {
            try {
                using (MemoryStream alphaStream = new MemoryStream()) {
                    _alphaSerializer.Serialise(source, alphaStream);
                    alphaStream.Position = 0;
                    using (Stream betaStream = _innerConverter.ValidatedConvert(alphaStream)) {
                        return _betaSerializer.Deserialise(betaStream);
                    }
                }
            }
            catch (Exception ex) {
                throw new ConverterException("Convertion failed", ex);
            }
        }


        [Obsolete("No registered uses and is therefore marked for deletion. Please inform us of any use for this class/interface/method.")]
        private void Init()
        {
            _alphaSerializer = new Serializer<ALPHA>();
            _betaSerializer = new Serializer<BETA>();
        }
    }
}
