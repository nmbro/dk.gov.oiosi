using System;
using System.Collections.Generic;
using System.Text;

namespace dk.gov.oiosi.xml.schematron {
    public interface ISchematronStoreConfig {
        ushort MaxCompiledStylesheetsInMemory { get; }
    }
}
