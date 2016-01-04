using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.IO;

namespace dk.gov.oiosi.xml {
    [Obsolete("No registered uses and is therefore marked for deletion. Please inform us of any use for this class/interface/method.")]
    public class Serializer<T> where T : class
    {
        private XmlSerializer _serializer;

        [Obsolete("No registered uses and is therefore marked for deletion. Please inform us of any use for this class/interface/method.")]
        public Serializer()
        {
            _serializer = new XmlSerializer(typeof(T));
        }

        [Obsolete("No registered uses and is therefore marked for deletion. Please inform us of any use for this class/interface/method.")]
        public void Serialise(T t, Stream stream)
        {
            _serializer.Serialize(stream, t);
        }

        [Obsolete("No registered uses and is therefore marked for deletion. Please inform us of any use for this class/interface/method.")]
        public T Deserialise(Stream stream)
        {
            return (T)_serializer.Deserialize(stream);
        }
    }
}
