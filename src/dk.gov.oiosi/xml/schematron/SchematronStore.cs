using System;
using System.IO;
using System.Xml;
using System.Xml.Xsl;
using dk.gov.oiosi.common.cache;
using dk.gov.oiosi.xml.xslt;
using dk.gov.oiosi.configuration;
using System.Configuration;
using System.Xml.XPath;
using System.Xml.Linq;
using System.Collections.Generic;

namespace dk.gov.oiosi.xml.schematron {
    
    /// <summary>
    /// A global schematron store that can be used anywhere in the application to access
    /// schematrons from the filesystem. Further it caches a specific number of compiled 
    /// stylesheets in memory
    /// </summary>
    public class SchematronStore 
    {
        private static object lockObject = new object();
        private string basePath = string.Empty;
        private ICache<string, CompiledXslt> cache;

        /// <summary>
        /// Constructor that uses a default of max two cohierent compiled stylesheeets 
        /// in memory.
        /// </summary>
        public SchematronStore() 
        {
            // get the cache from the cache factory
            this.cache = CacheFactory.Instance.SchematrongStoreCache;

            // Get or create the base path for the resources
            // In development, if deployed on ASP.Net development  server, the default base path can not be used.
            // Therefore the basepath is retrived from the web.config file
            // If no basePath in app.config is defined, the Applications default base path is used.
            string basePath = ConfigurationManager.AppSettings["ResourceBasePath"];
            if (!string.IsNullOrEmpty(basePath))
            {
                this.basePath = basePath;
            }
        }

        /// <summary>
        /// Gets the compiled schematron from a given path.
        /// </summary>
        /// <param name="path">The path where to find the </param>
        /// <returns></returns>
        public CompiledXslt GetCompiledSchematron(string path)
        {
            if (path == null)
            {
                throw new ArgumentNullException("path");
            }

            string cacheKey = path;
            FileInfo fileInfo;

            if (string.IsNullOrEmpty(this.basePath))
            {
                fileInfo = new FileInfo(path);
            }
            else
            {
                string combinedPath = Path.Combine(this.basePath, path);
                fileInfo = new FileInfo(combinedPath);
            }

            CompiledXslt compiledXsltEntry = null;

            lock (lockObject)
            {
                if (cache.TryGetValue(cacheKey, out compiledXsltEntry))
                {
                    //  pre-compiled schematron found
                }
                else
                {
                    compiledXsltEntry = new CompiledXslt(fileInfo);
                    cache.Add(cacheKey, compiledXsltEntry);
                }
            }

            return compiledXsltEntry;
        }        
    }
}
