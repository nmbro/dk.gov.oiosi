namespace dk.gov.oiosi.common {
    public interface IExternalCodeFactoryConfiguration {
        string ImplementationAssembly { get; }
        string ImplementationNamespaceClass { get; }
    }
}
