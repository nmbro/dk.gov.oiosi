using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

using dk.gov.oiosi.extension.wcf.Interceptor;

namespace dk.gov.oiosi.test.interceptors.messageCounter {
    public class MessageCounterBindingElement: CommonBindingElement {
        private static Dictionary<string, List<XmlDocument>> _messagesReceived = new Dictionary<string, List<XmlDocument>>();
        private static Dictionary<string, List<XmlDocument>> _messagesSent = new Dictionary<string, List<XmlDocument>>();
        
        public static Dictionary<string, List<XmlDocument>> MessagesSent {
            get { return _messagesSent; }
        }

        public static Dictionary<string, List<XmlDocument>> MessagesReceived {
            get { return _messagesReceived; }
        }

        public static int NoSentMessages(string action){
            try {
                return MessagesSent[action].Count;
            }
            catch (KeyNotFoundException) {
                throw new Exception("No messages with that action have been sent");
            }
        }

        public static int NoReceivedMessages(string action) {
            try {
                return MessagesReceived[action].Count;
            }
            catch (KeyNotFoundException) {
                throw new Exception("No messages with that action have been sent");
            }
        }

        public override bool DoesFaultOnRequestException{get { return false; }}
        public override bool DoesRequestIntercept{get { return true; }}
        public override bool DoesResponseIntercept{get { return true; }}

        public override void InterceptRequest(dk.gov.oiosi.extension.wcf.Interceptor.Channels.InterceptorMessage interceptorMessage) {
            string action = interceptorMessage.GetHeaders().Action;
            List<XmlDocument> sentMessagesWithSameAction;
            MessagesSent.TryGetValue(action, out sentMessagesWithSameAction);
            
            if(sentMessagesWithSameAction == null) {
                sentMessagesWithSameAction = new List<XmlDocument>();
                MessagesSent.Add(action, sentMessagesWithSameAction);
            }

            sentMessagesWithSameAction.Add(interceptorMessage.GetBody());
        }

        public override void InterceptResponse(dk.gov.oiosi.extension.wcf.Interceptor.Channels.InterceptorMessage interceptorMessage) {
            string action = interceptorMessage.GetHeaders().Action;
            List<XmlDocument> receivedMessagesWithSameAction;
            MessagesReceived.TryGetValue(action, out receivedMessagesWithSameAction);

            if (receivedMessagesWithSameAction == null) {
                receivedMessagesWithSameAction = new List<XmlDocument>();
                MessagesReceived.Add(action, receivedMessagesWithSameAction);
            }

            receivedMessagesWithSameAction.Add(interceptorMessage.GetBody());
        }

        public override System.ServiceModel.Channels.BindingElement Clone() {
            return new MessageCounterBindingElement();
        }
    }
}
