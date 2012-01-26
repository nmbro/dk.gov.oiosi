using System;
using System.IO;
using System.Xml;
using System.Xml.Xsl;
using dk.gov.oiosi.common.cache;
using dk.gov.oiosi.xml.xslt;
using dk.gov.oiosi.configuration;

namespace dk.gov.oiosi.xml.schematron {
    
    /// <summary>
    /// A global schematron store that can be used anywhere in the application to access
    /// schematrons from the filesystem. Further it caches a specific number of compiled 
    /// stylesheets in memory
    /// </summary>
    public class SchematronStore 
    {
        private static object lockObject = new object();
        private ICache<string, XslCompiledTransform> cache;

        /// <summary>
        /// Constructor that uses a default of max two cohierent compiled stylesheeets 
        /// in memory.
        /// </summary>
        public SchematronStore() 
        {
            // get the cache from the cache factory
            this.cache = CacheFactory.Instance.SchematrongStoreCache;
        }

        /// <summary>
        /// Gets the compiled schematron from a given path.
        /// </summary>
        /// <param name="path">The path where to find the </param>
        /// <returns></returns>
        public XslCompiledTransform GetCompiledSchematron(string path)
        {
            if (path == null)
            {
                throw new ArgumentNullException("path");
            }

            FileInfo fileInfo = new FileInfo(path);
            XslCompiledTransform compiledSchematron = null;
            string cacheKey = fileInfo.FullName;

            lock (lockObject)
            {
                if (cache.TryGetValue(cacheKey, out compiledSchematron))
                {
                    //  pre-compiled schematron found
                }
                else
                {
                    compiledSchematron = this.LoadCompiledSchematron(fileInfo);
                    cache.Add(cacheKey, compiledSchematron);
                }
            }

            return compiledSchematron;
        }

        private XslCompiledTransform LoadCompiledSchematron(FileInfo fileInfo) 
        {
            XslCompiledTransform xslCompiledTransform;
            XmlDocument xmlStylesheet = new XmlDocument();
            XsltUtility xsltUtility = new XsltUtility();
            try 
            {
                xmlStylesheet.Load(fileInfo.FullName);
                xslCompiledTransform = xsltUtility.PrecompiledStyleSheet(xmlStylesheet);
            }
            catch (Exception ex) {
                throw new FailedToLoadSchematronStylesheetException(fileInfo, ex);
            }

            return xslCompiledTransform;
        }
    }
}
