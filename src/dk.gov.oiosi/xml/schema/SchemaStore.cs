using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using dk.gov.oiosi.common.cache;
using System.Xml.Schema;
using dk.gov.oiosi.configuration;
using dk.gov.oiosi.communication.configuration;
using System.IO;
using dk.gov.oiosi.extension.wcf.Interceptor.Validation.Schema;

namespace dk.gov.oiosi.xml.schema
{
    /// <summary>
    /// A global schema store that can be used anywhere in the application to access
    /// schem from the filesystem. Further it caches a specific number of compiled 
    /// schema in memory
    /// </summary>
    public class SchemaStore
    {
        private static object lockObject = new object();
        private ICache<string, XmlSchemaSet> cache;

        public SchemaStore()
        {
            // Gets the cache from the CacheFactory
            cache = CacheFactory.Instance.SchemaStoreCache;
        }

        /// <summary>
        /// Load the compiled XmlSchemaSet. If the XmlSchemaSet already exist in the cache, that XmlSchemaSet is used,
        /// else the XmlSchemaSet is compiled, and added to the cache, and then returned.
        /// </summary>
        /// <param name="documentType">The DocumentTypeConfig</param>
        /// <returns>The compiled XmlSchemaSet</returns>
        public XmlSchemaSet GetCompiledXmlSchemaSet(DocumentTypeConfig documentType)
        {
            return this.GetCompiledXmlSchemaSet(documentType, null);
        }

        /// <summary>
        /// Load the compiled XmlSchemaSet. If the XmlSchemaSet already exist in the cache, that XmlSchemaSet is used,
        /// else the XmlSchemaSet is compiled, and added to the cache, and then returned.
        /// </summary>
        /// <param name="documentType">The DocumentTypeConfig</param>
        /// <param name="validationEventHandler"></param>
        /// <returns>The compiled XmlSchemaSet</returns>
        public XmlSchemaSet GetCompiledXmlSchemaSet(DocumentTypeConfig documentType, ValidationEventHandler validationEventHandler)
        {
            XmlSchemaSet xmlSchemaSet = null;

            lock (lockObject)
            {
                string key = documentType.RootNamespace;
                if (cache.TryGetValue(key, out xmlSchemaSet))
                {
                    // XmlSchemaSet found, returning the cached XmlSchemaSet
                }
                else
                {
                    // XmlSchemaSet not cached
                    // so create it and add it to the cache
                    XmlSchema xmlSchema = this.LoadXmlSchema(documentType);
                    this.CheckNamespace(documentType, xmlSchema);

                    xmlSchemaSet = this.LoadXmlSchemaSet(documentType, xmlSchema, validationEventHandler);
                    
                    if (validationEventHandler == null)
                    {
                        // no validationEventHandler, so we can cache the compiled XmlSchemaSet
                        this.cache.TryAddValue(key, xmlSchemaSet);
                    }
                    else
                    {
                        // validationEventHandler is used, so the compiled xmlSchemaSet can not be cached 
                        // (next time it can be another EventHandler, for the same XmlSchemaSet)
                    }
                }
            }

            return xmlSchemaSet;
        }

        /// <summary>
        /// Load the schema from the harddisk
        /// </summary>
        /// <param name="documentType"></param>
        /// <returns></returns>
        public XmlSchema LoadXmlSchema(DocumentTypeConfig documentType)
        {
            // ToDo: The XmlSchema file could be cached for faster retrivel
            XmlSchema schema = null;
            FileInfo schemaFile = new FileInfo(documentType.SchemaPath);
            FileStream fs = null;            
            try
            {
                fs = File.OpenRead(schemaFile.FullName);
                schema = XmlSchema.Read(fs, null);
            }
            catch (Exception ex)
            {
                throw new FailedToLoadSchemaException(schemaFile, ex);
            }
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                }
            }

            return schema;
        }

        public XmlSchemaSet LoadXmlSchemaSet(DocumentTypeConfig documentType, XmlSchema xmlSchema)
        {
            return this.LoadXmlSchemaSet(documentType.SchemaPath, xmlSchema, null);
        }

        public XmlSchemaSet LoadXmlSchemaSet(string schemaPath, XmlSchema xmlSchema)
        {
            return this.LoadXmlSchemaSet(schemaPath, xmlSchema, null);
        }

        public XmlSchemaSet LoadXmlSchemaSet(DocumentTypeConfig documentType, XmlSchema xmlSchema, ValidationEventHandler validationEventHandler)
        {
            return this.LoadXmlSchemaSet(documentType.SchemaPath, xmlSchema, validationEventHandler);
        }

        public XmlSchemaSet LoadXmlSchemaSet(string schemaPath, XmlSchema xmlSchema, ValidationEventHandler validationEventHandler)
        {

            FileInfo localSchemaLocationFileInfo = new FileInfo(schemaPath);
            DirectoryInfo directoryInfo = localSchemaLocationFileInfo.Directory;
            UrlToLocalFilelResolver urlResolver = new UrlToLocalFilelResolver(directoryInfo);

            XmlSchemaSet xmlSchemaSet = new XmlSchemaSet();
            xmlSchemaSet.XmlResolver = urlResolver;
            xmlSchemaSet.Add(xmlSchema);

            if (validationEventHandler == null)
            {
                // no validationEventHandler, so we can cache the compiled XmlSchemaSet
            }
            else
            {
                // validationEventHandler is used, so the compiled xmlSchemaSet can not be cached 
                // (next time it can be another EventHandler, for the same XmlSchemaSet)
                xmlSchemaSet.ValidationEventHandler += validationEventHandler;
                
            }

            xmlSchemaSet.Compile();

            return xmlSchemaSet;
        }


        /// <summary>
        /// Controls that the schema and the document has the same namespace.
        /// </summary>
        /// <exception cref="UnexpectedNamespaceException"></exception>
        /// <param name="xmlDocument"></param>
        /// <param name="xmlSchema"></param>
        private void CheckNamespace(DocumentTypeConfig documentType, XmlSchema xmlSchema)
        {
            string documentNamespace = documentType.RootNamespace;
            string expectedNamespace = xmlSchema.TargetNamespace;

            if (documentNamespace != expectedNamespace)
            {
                throw new UnexpectedNamespaceException(documentNamespace, expectedNamespace);
            }
        }
    }
}
