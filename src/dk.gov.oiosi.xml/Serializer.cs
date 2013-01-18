using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.IO;

namespace dk.gov.oiosi.xml {
    public class Serializer<T> where T:class {
        private XmlSerializer _serializer;

        public Serializer() {
            _serializer = new XmlSerializer(typeof(T));
        }

        public void Serialise(T t, Stream stream) {
            _serializer.Serialize(stream, t);
        }

        public T Deserialise(Stream stream) {
            return (T)_serializer.Deserialize(stream);
        }
    }
}
