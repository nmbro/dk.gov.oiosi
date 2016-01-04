using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using dk.gov.oiosi.communication.fault;

namespace dk.gov.oiosi.extension.wcf.Interceptor.Channels
{
    class InterceptorChannelWrapperException : InterceptorChannelException
    {
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="innerException">The inner exception</param>
        public InterceptorChannelWrapperException(OiosiFaultCode oiosiFaultCode, OiosiInnerFaultCode oiosiInnerFaultCode, Exception innerException)
            : base(oiosiFaultCode, oiosiInnerFaultCode, CreatKeywords(innerException.Message))
        { }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="oiosiFaultCode"></param>
        /// <param name="oiosiInnerFaultCode"></param>
        /// <param name="message">The message</param>
        public InterceptorChannelWrapperException(OiosiFaultCode oiosiFaultCode, OiosiInnerFaultCode oiosiInnerFaultCode, string message)
            : base(oiosiFaultCode, oiosiInnerFaultCode, CreatKeywords(message))
        { }

        private static Dictionary<string, string> CreatKeywords(string message)
        {
            Dictionary<string, string> map = new Dictionary<string, string>();
            map.Add("message", message);
            return map;
        }
    }
}
