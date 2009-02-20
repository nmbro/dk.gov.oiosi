/*
  * The contents of this file are subject to the Mozilla Public
  * License Version 1.1 (the "License"); you may not use this
  * file except in compliance with the License. You may obtain
  * a copy of the License at http://www.mozilla.org/MPL/
  *
  * Software distributed under the License is distributed on an
  * "AS IS" basis, WITHOUT WARRANTY OF ANY KIND, either express
  * or implied. See the License for the specific language governing
  * rights and limitations under the License.
  *
  *
  * The Original Code is .NET RASP toolkit.
  *
  * The Initial Developer of the Original Code is Accenture and Avanade.
  * Portions created by Accenture and Avanade are Copyright (C) 2007
  * Danish National IT and Telecom Agency (http://www.itst.dk). 
  * All Rights Reserved.
  *
  * Contributor(s):
  *   Gert Sylvest (gerts@avanade.com)
  *   Patrik Johansson (p.johansson@accenture.com)
  *   Michael Nielsen (michaelni@avanade.com)
  *   Dennis Søgaard (dennis.j.sogaard@accenture.com)
  *   Ramzi Fadel (ramzif@avanade.com)
  *   Mikkel Hippe Brun (mhb@itst.dk)
  *   Finn Hartmann Jordal (fhj@itst.dk)
  *   Christian Lanng (chl@itst.dk)
  *
  */

using System;
using System.Collections.Generic;
using dk.gov.oiosi.exception;

namespace dk.gov.oiosi.communication.handlers.email {

    /// <summary>
    /// Inbox factory
    /// </summary>
    public class InboxFactory {
        private static InboxFactory _instance;
        private static object _instanceLock = new object();
        private object _inboxFactoringLock = new object();
        private Dictionary<string, IInbox> _inboxes;
        private Dictionary<object, IInbox> _userInboxes;
        private Dictionary<IInbox, ulong> _inboxUsers;

        private InboxFactory() {
            _inboxes = new Dictionary<string, IInbox>();
            _userInboxes = new Dictionary<object, IInbox>();
            _inboxUsers = new Dictionary<IInbox, ulong>();
        }

        /// <summary>
        /// Returns the singleton instance of the InboxFactory
        /// </summary>
        /// <returns></returns>
        public static InboxFactory GetInstance() {
            lock (_instanceLock) {
                if (_instance == null)
                    _instance = new InboxFactory();
                return _instance;
            }
        }

        /// <summary>
        /// True if the factory has produced inboxes
        /// </summary>
        public bool HasInboxes {
            get {
                lock (_inboxFactoringLock) {
                    return _inboxes.Count > 0;
                }
            }
        }

        /// <summary>
        /// Creates an IInbox
        /// </summary>
        /// <param name="serverConfiguration">The mail server configuration</param>
        /// <param name="inBoxImplementationType">The type of inbox implementation</param>
        /// <param name="user">The user</param>
        /// <returns>An IInbox instance</returns>
        public IInbox GetInbox(IMailServerConfiguration serverConfiguration, 
                               Type inBoxImplementationType, 
                               object user) {
            if (serverConfiguration == null) throw new NullArgumentException("serverConfiguration");
            if (inBoxImplementationType == null) throw new NullArgumentException("inboxImplementationType");
            if (user == null) throw new NullArgumentException("user");
            string replyAddress = serverConfiguration.ReplyAddress;
            lock (_inboxFactoringLock) {
                try {
                    IInbox inbox = null;
                    if (!_inboxes.TryGetValue(replyAddress, out inbox)) {
                        inbox = (IInbox)inBoxImplementationType.GetConstructor(new Type[0]).Invoke(null);
                        inbox.InboxServerConfiguration = serverConfiguration;
                        _inboxUsers[inbox] = 0;
                        _inboxes[replyAddress] = inbox;
                        inbox.BeginReceiving();
                    }
                    _userInboxes[user] = inbox;
                    _inboxUsers[inbox] = _inboxUsers[inbox] + 1;
                    return inbox;
                }
                catch (Exception ex) {
                    throw new FailedToGetInboxException(serverConfiguration, inBoxImplementationType, user, ex);
                }
            }
        }

        /// <summary>
        /// Indicates that an inbox is no longer in use
        /// </summary>
        /// <param name="user">The user</param>
        public void FinishedUsingInbox(object user) {
            if (user == null) throw new NullArgumentException("user");
            lock (_inboxFactoringLock) {
                try {
                    IInbox inbox = _userInboxes[user];
                    _userInboxes.Remove(user);
                    ulong inboxUsers = _inboxUsers[inbox] - 1;
                    if (inboxUsers == 0) {
                        string replyAddress = inbox.InboxServerConfiguration.ReplyAddress;
                        if (inbox.InboxState == InboxState.Listening) inbox.Close();
                        _inboxes.Remove(replyAddress);
                        _inboxUsers.Remove(inbox);
                    }
                    _inboxUsers[inbox] = inboxUsers;
                }
                catch (Exception ex) {
                    throw new FailedToStopUsingInboxException(user, ex);
                }
            }
        }
    }
}
