﻿using System;
using System.Collections.Generic;
using System.Text;

namespace dk.gov.oiosi.xml.schematron {
    /// <summary>
    /// Interface that describes what how the configuration of the schematron store should
    /// look.
    /// </summary>
    public interface ISchematronStoreConfig {
        /// <summary>
        /// Gets the maximum number of compiled stylesheet to have in the memory
        /// </summary>
        ushort MaxCompiledStylesheetsInMemory { get; }
    }
}
