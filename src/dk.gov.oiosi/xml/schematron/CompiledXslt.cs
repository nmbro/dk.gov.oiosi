using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;
using System.Xml.Xsl;
using dk.gov.oiosi.xml.xslt;

namespace dk.gov.oiosi.xml.schematron
{
    public class CompiledXslt
    {
        /// <summary>
        /// The compiles cache entry, if xslt 1.0, and not to complex
        /// </summary>
        private XslCompiledTransform transform;

        /// <summary>
        /// The resource info
        /// </summary>
        private FileInfo fileInfo;

        public CompiledXslt(FileInfo fileInfo)
        {
            this.fileInfo = fileInfo;

            try
            {
                // Note, if not explicite marked as version 1.0, we default to SaXon
                XsltUtility xsltUtility = new XsltUtility();
                XmlDocument stylesheet = new XmlDocument();
                stylesheet.Load(this.fileInfo.FullName);

                // Get XSLT version
                XPathNavigator navigator = stylesheet.CreateNavigator();
                XPathNodeIterator node = navigator.Select("/*/@version");
                node.MoveNext();
                string xsltVersionInResource = node.Current.Value;
                string xsltVersion = string.Empty;
                if (!string.IsNullOrEmpty(xsltVersionInResource))
                {
                    xsltVersion = xsltVersionInResource;
                }

                if (xsltVersion.Equals("1.0"))
                {
                    // The XslCompiledTransform can only handle xslt version 1.0
                    transform = new XslCompiledTransform(false);
                    transform.Load(stylesheet, XsltSettings.Default, null);
                }
            }
            catch (System.Xml.Xsl.XsltException ex)
            {
                if (ex.Message.Equals("Too complex!"))
                {
                    // To complex - normally behavior
                    // using saxon
                    this.transform = null;
                }
                else if (ex.Message.Equals("XSLT compile error."))
                {
                    // To complex - normally behavior
                    // using saxon
                    this.transform = null;
                }
                else if (ex.Message.Contains("is an unknown XSLT function"))
                {
                    // To complex - normally behavior
                    // using saxon
                    this.transform = null;
                }
                else
                {
                    //Debug.Assert(false, "XslCompiledTransform failed loading stylesheet.", ex.ToString());
                    // what if the message has been translated to Danish???
                    throw;
                }
            }
            catch (Exception ex)
            {
                Debug.Fail("XslCompiledTransform failed loading stylesheet.", ex.ToString());
                throw;
            }

            ////XDocument xmlFile = XDocument.Load(fileInfo.FullName);
            ////IEnumerable<XAttribute> attributeCollection = xmlFile.Root.Attributes();
            ////bool useDefault = false;
            ////foreach (XAttribute attribute in attributeCollection)
            ////{
            ////    if (attribute.Name.LocalName.Equals("version", StringComparison.OrdinalIgnoreCase))
            ////    {
            ////        if (attribute.Value.Equals("1") || attribute.Value.Equals("1.0"))
            ////        {
            ////            useDefault = true;
            ////            break;
            ////        }
            ////    }
            ////}

            ////if (useDefault)
            ////{
            ////    // use Microsoft default
            ////    XmlDocument xmlStylesheet = new XmlDocument();
            ////    XsltUtility xsltUtility = new XsltUtility();

            ////    DirectoryInfo directoryInfo = fileInfo.Directory;
            ////    UrlToLocalFilelResolver urlResolver = new UrlToLocalFilelResolver(directoryInfo.FullName);

            ////    xmlStylesheet.Load(fileInfo.FullName);
            ////    xmlStylesheet.XmlResolver = urlResolver;
            ////    this.transform = xsltUtility.PrecompiledStyleSheet(xmlStylesheet);
            ////}
            ////else
            ////{
            ////    // use saxon
            ////    this.transform = null;
            ////}

        }

        public XslCompiledTransform XslCompiledTransform
        {
            get {return  this.transform; }
        }

        public FileInfo FileInfo
        {
            get { return this.fileInfo; }
        }

        public MemoryStream Stream
        {
            get 
            {
                MemoryStream memoryStream = new MemoryStream();
                using (FileStream fileStream = this.fileInfo.Open(FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    memoryStream.SetLength(fileStream.Length);
                    fileStream.Read(memoryStream.GetBuffer(), 0, (int)fileStream.Length);
                    memoryStream.Flush();
                    fileStream.Close();
                }

                return memoryStream;           
            }
        }
    }
}
