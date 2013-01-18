using dk.gov.oiosi.exception;

namespace dk.gov.oiosi.uddi {

    /// <summary>
    /// Represents the process role definition in the UDDI structure.
    /// </summary>
    public class ProcessRoleDefinition {

        /// <summary>
        /// Constructor that takes the given parameters.
        /// 
        /// No parameters are allowed to be null if any is a 
        /// </summary>
        /// <exception cref="NullOrEmptyArgumentException">Exception thrown if the name parameter string is null or empty</exception>
        /// <exception cref="NullArgumentException">Exception thrown if any of the given parameters description, role, roletype or processDefinitionId is null</exception>
        /// <param name="name">Name of the process information</param>
        /// <param name="description">Description of the process information</param>
        /// <param name="role">Role of the process information</param>
        /// <param name="roleType">Role type of the process information</param>
        /// <param name="processDefinitionId">Identifier of the process information</param>
        public ProcessRoleDefinition(string name, string description, string role, string roleType, UddiId processDefinitionId) {
            if (string.IsNullOrEmpty(name)) throw new NullOrEmptyArgumentException("name");
            if (description == null) throw new NullArgumentException("description");
            if (role == null) throw new NullArgumentException("role");
            if (roleType == null) throw new NullArgumentException("roleType");
            if (processDefinitionId == null) throw new NullArgumentException("processDefinitionId");
            Name = name;
            Description = description;
            Role = role;
            RoleType = roleType;
            ProcessDefinitionId = processDefinitionId;
        }

        /// <summary>
        /// Gets the name
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the description
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// Gets the role
        /// </summary>
        public string Role { get; private set; }

        /// <summary>
        /// Gets the roletype
        /// </summary>
        public string RoleType { get; private set; }

        /// <summary>
        /// Gets the process definition id
        /// </summary>
        public UddiId ProcessDefinitionId { get; private set; }
    }
}
