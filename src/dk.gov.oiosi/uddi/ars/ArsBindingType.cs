using System;
using System.Collections.Generic;
using System.Text;

using dk.gov.oiosi.uddi.TModels;
using dk.gov.oiosi.uddi.category;

namespace dk.gov.oiosi.uddi.ars {
    /// <summary>
    /// This class represents the binding type in the ARS
    /// library.
    /// </summary>
    public class ArsBindingType {
        private TModel _tmodel = new TModel();

        /// <summary>
        /// Default constructor
        /// </summary>
        public ArsBindingType() { }

        /// <summary>
        /// Constructor that takes a tmodel that represents a binding type
        /// </summary>
        /// <param name="tmodel"></param>
        public ArsBindingType(TModel tmodel) {
            _tmodel = tmodel;
        }

        /// <summary>
        /// Gets or sets the underlaying tmodel.
        /// </summary>
        public TModel TModel {
            get { return _tmodel; }
            set {
                if (value == null) throw new ArgumentNullException("TModel");
                _tmodel = value; 
            }
        }

        /// <summary>
        /// Gets the UDDI identifier of the binding type
        /// </summary>
        public UddiId ID {
            get {
                if (_tmodel.Value.tModelKey == null) return null;
                return new UddiGuidId(_tmodel.Value.tModelKey, true);
            }
        }

        /// <summary>
        /// Gets the name of the binding type
        /// </summary>
        public Name Name {
            get { return _tmodel.Name; }
        }

        /// <summary>
        /// Gets the port type reference
        /// </summary>
        /// <returns></returns>
        public OasisPortTypeRegistrationReference GetPortTypeReference() {
            if (_tmodel.CategoryBag == null) return null;
            
            KeyedReference category = _tmodel.CategoryBag.GetCategoryByIdentifierAndKeyName(UddiOrgWsdlPortTypeReference.CATEGORYID, UddiOrgWsdlPortTypeReference.DEFAULTCATEGORYKEYNAME);
            UddiGuidId referenceId = new UddiGuidId(category.KeyValue, true);
            OasisPortTypeRegistrationReference portTypeReference = new OasisPortTypeRegistrationReference(referenceId);
            return portTypeReference;
        }

        /// <summary>
        /// Gets the reference to this ars binding type
        /// </summary>
        /// <returns></returns>
        public OasisBindingRegistrationReference GetBindingRegistrationReference() {
            if (_tmodel.CategoryBag == null) return null;

            
            UddiGuidId referenceId = new UddiGuidId(_tmodel.Value.tModelKey, true);
            OasisBindingRegistrationReference reference = new OasisBindingRegistrationReference(referenceId);
            return reference;
        }

        /// <summary>
        /// Gets the transport
        /// </summary>
        /// <returns></returns>
        public UddiOrgWsdlCategorizationTransport GetTransport() {
            if (_tmodel.CategoryBag == null) return null;
            KeyedReference category = _tmodel.CategoryBag.GetCategoryByIdentifierAndKeyName(UddiOrgWsdlCategorizationTransport.CATEGORYID, UddiOrgWsdlCategorizationTransport.DEFAULTCATEGORYKEYNAME);
            UddiOrgWsdlCategorizationTransport transport = new UddiOrgWsdlCategorizationTransport(category);
            return transport;
        }
    }
}
