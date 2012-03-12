
namespace dk.gov.oiosi.logging
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Internal struc, only used in this class
    /// </summary>
    public struct SourceData
    {
        /// <summary>
        /// Name of the module/assebly in which the logging was requested
        /// </summary>
        private string moduleName;

        /// <summary>
        /// The name of the class/struct in which the logging was requested
        /// </summary>
        private string className;

        /// <summary>
        /// The method in which the logging was requested
        /// </summary>
        private string methodName;

        /// <summary>
        /// The line in which the logging was requested 
        /// </summary>
        private int lineNumber;

        /// <summary>
        /// Initializes a new instance of the SourceData struct.
        /// </summary>
        /// <param name="moduleName">The name of the module</param>
        /// <param name="className">The name of the class</param>
        /// <param name="methodName">The name of the class/struct</param>
        /// <param name="lineNumber">The line number</param>
        public SourceData(string moduleName, string className, string methodName, int lineNumber)
        {
            this.moduleName = moduleName;
            this.className = className;
            this.methodName = methodName;
            this.lineNumber = lineNumber;
        }

        /// <summary>
        /// Gets that name of the module
        /// </summary>
        public string ModuleName
        {
            get
            {
                return this.moduleName;
            }
        }

        /// <summary>
        /// Gets that name of the class/struct
        /// </summary>
        public string ClassName
        {
            get
            {
                return this.className;
            }
        }

        /// <summary>
        /// Gets the name of the methods
        /// </summary>
        public string MethodName
        {
            get
            {
                return this.methodName;
            }
        }

        /// <summary>
        /// Get the line number
        /// </summary>
        public int Line
        {
            get
            {
                return this.lineNumber;
            }
        }

        public string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(this.ModuleName);
            builder.Append(" ");
            builder.Append(this.className);
            builder.Append(".");
            builder.Append(this.methodName);
            builder.Append("(");
            builder.Append(this.lineNumber);
            builder.Append(")");

            return builder.ToString();
        }
    }
}
