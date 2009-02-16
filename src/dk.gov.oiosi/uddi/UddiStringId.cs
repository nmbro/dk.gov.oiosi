using System;
using System.Collections.Generic;
using System.Text;
using dk.gov.oiosi.exception;

namespace dk.gov.oiosi.uddi {
    /// <summary>
    /// Simplefied uddi id type that uses a string representation.
    /// This is a very lax implementation.
    /// </summary>
    public class UddiStringId : UddiId {
        private string _noUddiPrefix;

        /// <summary>
        /// Creates a new Id
        /// </summary>
        public UddiStringId() {
            _noUddiPrefix = Guid.NewGuid().ToString();
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">uddi id</param>
        /// <param name="isUddiType">is it uddi type</param>
        public UddiStringId(string id, bool isUddiType) {
            if (String.IsNullOrEmpty(id)) throw new NullOrEmptyArgumentException("id");
            if (id.Length < 10) throw new UnexpectedNumberOfCharactersException("id", 10);
            if (isUddiType) {
                _noUddiPrefix = id.Substring(5);
            } else {
                _noUddiPrefix = id;
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="guid">guid</param>
        public UddiStringId(Guid guid) {
            if (guid == Guid.Empty) {
                throw new UddiEmptyGuidException();
            }
            _noUddiPrefix = guid.ToString();
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="Id"></param>
        public UddiStringId(UddiId Id) : this(Id.ID, true) {}

        /// <summary>
        /// Gets the uddi id prefixed with uddi:
        /// </summary>
        public override string ID {
            get {
                return "uddi:" + _noUddiPrefix;
            }
        }

        /// <summary>
        /// Gets as string
        /// </summary>
        /// <returns>string</returns>
        public override string ToString() {
            return ID;
        }
    }
}
