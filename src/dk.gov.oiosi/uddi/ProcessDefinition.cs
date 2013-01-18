using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dk.gov.oiosi.uddi {

    /// <summary>
    /// Class that contains information about a process definition
    /// </summary>
    public class ProcessDefinition {
        /// <summary>
        /// Constructor that takes the identifier, the name and the description of
        /// the process definition.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="identifier"></param>
        /// <param name="identifierType"></param>
        /// <param name="registrationConformanceClaim"></param>
        public ProcessDefinition(UddiId id, string name, string description, string identifier, string identifierType, string registrationConformanceClaim) {
            Id = id;
            Name = name;
            Description = description;
            Identifier = identifier;
            IdentifierType = identifierType;
            RegistrationConformanceClaim = registrationConformanceClaim;
        }

        /// <summary>
        /// Gets the identifier of the process defintion in the UDDI register
        /// </summary>
        public UddiId Id { get; private set; }

        /// <summary>
        /// Gets the name of the process definition
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the description of the process definition
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// Gets the business process identifier
        /// </summary>
        public string Identifier { get; private set; }

        /// <summary>
        /// Gets the business process identifier type
        /// </summary>
        public string IdentifierType { get; private set; }

        /// <summary>
        /// Gets the registration conformance claim
        /// </summary>
        public string RegistrationConformanceClaim { get; private set; }
    }
}
