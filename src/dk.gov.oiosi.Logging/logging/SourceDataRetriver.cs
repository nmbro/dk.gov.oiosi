

namespace dk.gov.oiosi.logging
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Diagnostics;
    using System.Reflection;

    /// <summary>
    /// Source data - used to skip certain frame to find the correct sender
    /// </summary>
    public class SourceDataRetriver
    {
        /// <summary>
        /// 
        /// </summary>
        public SourceDataRetriver()
        {
        }

        /// <summary>
        /// Get the source data
        /// </summary>
        /// <returns></returns>
        public SourceData SourceData()
        {
            return this.SourceData(4);

        }

        /// <summary>
        /// Get the source data, by skipping stack
        /// </summary>
        /// <param name="skipStackTrace"></param>
        /// <returns></returns>
        public SourceData SourceData(int skipStackTrace)
        {
            SourceData result;

            // The call stack; skip the 3 first frames.
            // The first frame is this method,
            // The second frame is LoggerLog4Net.Log
            // The third frame is the LoggerLog4Net.[Debug/Info/Warn/Error/Fatal]
            StackTrace stackTrace = new StackTrace(skipStackTrace, true);

            // TODO: Protentiel null point exception. The stackTrace can be null, is using service WCF
            StackFrame stackFrame = stackTrace.GetFrame(0); // The frame that called me
            MethodBase method = stackFrame.GetMethod(); // The method that called me

            if (method.IsConstructor)
            {
                result = new SourceData(method.Module.Name, method.DeclaringType.FullName, method.DeclaringType.Name, stackFrame.GetFileLineNumber());
            }
            else
            {
                result = new SourceData(method.Module.Name, method.DeclaringType.FullName, method.Name, stackFrame.GetFileLineNumber());
            }

            return result;
        }
    }
}
