using System;
using dk.gov.oiosi.exception;

namespace dk.gov.oiosi.common {
    /// <summary>
    /// Factory to create instances from a given class name and assembly and case the 
    /// type to T.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ExternalCodeFactory<T> {
        /// <summary>
        /// Creates a an instance from the given implementation namespace class and
        /// implementation assembly, and returns it as type T.
        /// </summary>
        /// <param name="implementationNamespaceClass">The implementation class with namespace</param>
        /// <param name="implementationAssembly">The implementation assembly</param>
        /// <returns></returns>
        public T CreateInstance(string implementationNamespaceClass, string implementationAssembly) {
            if (implementationNamespaceClass == null) throw new NullArgumentException("implementationNamespaceClass");
            if (implementationAssembly == null) throw new NullArgumentException("implementationAssembly");
            try {
                string qualifiedTypename = implementationNamespaceClass + ", " + implementationAssembly;
                Type instanceType = Type.GetType(qualifiedTypename);
                if (instanceType == null)
                    throw new CouldNotLoadTypeException(qualifiedTypename);
                // 3. Instantiate the type:
                T instance = (T)instanceType.GetConstructor(new Type[0]).Invoke(null);
                return instance;
            }
            catch (Exception ex) {
                throw new CreateInstanceFailedException(implementationNamespaceClass, implementationAssembly, ex);
            }
        }

        /// <summary>
        /// Creates an instance from the given configuration.
        /// </summary>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public T CreateInstance(IExternalCodeFactoryConfiguration configuration) {
            if (configuration == null) throw new NullArgumentException("configuration");
            return CreateInstance(configuration.ImplementationNamespaceClass, configuration.ImplementationAssembly);
        }
    }
}
