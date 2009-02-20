namespace dk.gov.oiosi.communication.fault
{
    /// <summary>
    /// enum representing inner faultcode
    /// </summary>
    public enum OiosiInnerFaultCode
    {
        /// <summary>
        /// Fault returned when schema validation fails
        /// </summary>
        SchemaValidationFault,
        /// <summary>
        /// Fault returned when schematron validation fails
        /// </summary>
        SchematronValidationFault,
        /// <summary>
        /// Fault returned when the SOAP message signature isn't valid
        /// </summary>
        SignatureNotValidFault,
        /// <summary>
        /// Fault returned when the document isn't of a known UBL type
        /// </summary>
        UnknownDocumentTypeFault,
        /// <summary>
        /// Fault returned when message couldn't be persisted
        /// </summary>
        MessagePersistencyFault,
        /// <summary>
        /// Fault returned when xml transformation fails
        /// </summary>
        XsltTransformationFault,
        /// <summary>
        /// Fault returned when an unknown internal failure occurs
        /// </summary>
        InternalSystemFailureFault,
        /// <summary>
        /// Fault returned when an obligatory header is missing
        /// </summary>
        MissingHeaderFault,
        /// <summary>
        /// Fault returned when the sender is not authorized in the current context
        /// </summary>
        NotAuthorizedFault
    };
}
