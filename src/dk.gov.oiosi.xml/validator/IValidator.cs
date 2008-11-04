using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace dk.gov.oiosi.xml.validator {
    public interface IValidator {
        void Validate(Stream source);
    }
}
