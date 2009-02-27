using dk.gov.oiosi.exception;

namespace dk.gov.oiosi.uddi {

    /// <summary>
    /// Represents the process role definition in the UDDI structure.
    /// </summary>
    public class ProcessRoleDefinition {
        private string _name;
        private string _description;
        private string _role;
        private string _roleType;
        private UddiId _processDefinitionId;

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
            _name = name;
            _description = description;
            _role = role;
            _roleType = roleType;
            _processDefinitionId = processDefinitionId;
        }

        /// <summary>
        /// Gets the name
        /// </summary>
        public string Name {
            get { return _name; }
        }

        /// <summary>
        /// Gets the description
        /// </summary>
        public string Description {
            get { return _description; }
        }

        /// <summary>
        /// Gets the role
        /// </summary>
        public string Role {
            get { return _role; }
        }

        /// <summary>
        /// Gets the roletype
        /// </summary>
        public string RoleType {
            get { return _roleType; }
        }

        /// <summary>
        /// Gets the process definition id
        /// </summary>
        public UddiId ProcessDefinitionId {
            get { return _processDefinitionId; }
        }
    }
}
