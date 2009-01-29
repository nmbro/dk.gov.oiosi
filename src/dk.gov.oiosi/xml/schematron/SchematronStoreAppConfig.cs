using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace dk.gov.oiosi.xml.schematron {
    public class SchematronStoreAppConfig : ConfigurationSection, ISchematronStoreConfig {
        public const string SCHEMATRONSTOREAPPCONFIGNAME = "schematronStoreAppConfig";
        public const string MAXCOMPILEDSTYLESHEETSINMEMORYNAME = "maxCompiledStylesheetsInMemory";

        #region ISchematronStoreConfig Members

        [ConfigurationProperty(MAXCOMPILEDSTYLESHEETSINMEMORYNAME, IsRequired=false, DefaultValue=2)]
        public ushort MaxCompiledStylesheetsInMemory {
            get { return (ushort)this[MAXCOMPILEDSTYLESHEETSINMEMORYNAME]; }
        }

        #endregion
    }
}
