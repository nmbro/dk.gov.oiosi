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
using System.ServiceModel;
using System.ServiceModel.Channels;
using dk.gov.oiosi.communication.handlers.email;
using dk.gov.oiosi.communication.service;
using dk.gov.oiosi.communication.transport;
using dk.gov.oiosi.exception;
using dk.gov.oiosi.extension.wcf.Behavior;
using dk.gov.oiosi.extension.wcf.EmailTransport;
using dk.gov.oiosi.security.oces;

namespace dk.gov.oiosi.communication.listener {
    
    /// <summary>
    /// An OIOSI service listener
    /// </summary>
    public class Listener : IListener {

        /// <summary>
        /// The current state of the listener
        /// </summary>
        protected ListenerState pState = ListenerState.uninitialized;

        /// <summary>
        /// The identity of the listener
        /// </summary>
        protected ListenerIdentity pListenerIdentity;

        /// <summary>
        /// The ServiceHost object used by the listener to host the service
        /// </summary>
        protected ServiceHost pServiceHost;

        /// <summary>
        /// Dictionary over serviceHost / Listener pairs. Used by services to perform callbacks via the
        /// correct Listener object.
        /// </summary>
        protected static Dictionary<ServiceHostBase, Listener> pHostList = new Dictionary<ServiceHostBase, Listener>();

        /// <summary>
        /// Event handler for asynchronous exceptions
        /// </summary>
        protected AsyncExceptionThrownHandler pExceptionThrownHandler;

        /// <summary>
        /// The email transport binding
        /// </summary>
        protected EmailBindingElement pEmailBinding;

        /// <summary>
        /// MessageReceive event
        /// </summary>
        public event MessageEventDelegate MessageReceive;


        /// <summary>
        /// Add the current service host and Listener to a static list.
        /// </summary>
        /// <param name="listener"></param>
        /// <param name="host"></param>
        protected static void AddHost(Listener listener, ServiceHostBase host) {
            // 1. Check for null
            if (listener == null) {
                throw new NullArgumentException("listener");
            }
            if (host == null) {
                throw new NullArgumentException("host");
            }

            // 2. Register host / listener pair:
            lock (pHostList) {
                pHostList[host] = listener;
            }
        }

        /// <summary>
        /// Removes a host from the static host/listener dictionary.
        /// </summary>
        /// <param name="host">The ServiceHost to remove from the list</param>
        protected static void RemoveHost(ServiceHostBase host) {
            if (host == null) {
                throw new ArgumentNullException("host");
            }
            lock (pHostList) {
                pHostList.Remove(host);
            }
        }

        /// <summary>
        /// Call this method from a service implementation, when the service is invoked. It will then 
        /// invoke the correct Listener event.
        /// </summary>
        /// <param name="message">The received message</param>
        /// <param name="messageProcessStatus">Process status of the message</param>
        public static void TriggerMessageReceiveEvent(
            ListenerRequest message,
            MessageProcessStatus messageProcessStatus
        ) {
            // 1. Find the relevant Listener object
            ServiceHostBase currentServiceHost = OperationContext.Current.Host;
            if (currentServiceHost == null)
                throw new CurrentServiceHostSearchFailedException();

            Listener listener = null;
            lock (pHostList) {
                try {
                    listener = pHostList[currentServiceHost];
                } catch (KeyNotFoundException) {
                    // The listener was not found!
                }
                
                if (listener == null)
                    throw new WcfListenerSearchFailedException(currentServiceHost);
            }

            // 3. Invoke the event
            listener.TriggerInternalMessageReceiveEvent(message, messageProcessStatus);
        }

        /// <summary>
        /// Used by the public TriggerMessageReceiveEvent to trigger a message receive event
        /// on a particular Listener instance (this)
        /// </summary>
        /// <param name="message">The received message</param>
        /// <param name="messageProcessStatus">The message process status</param>
        protected void TriggerInternalMessageReceiveEvent(
            ListenerRequest message, 
            MessageProcessStatus messageProcessStatus
        ) {
            if (MessageReceive != null) {
                MessageReceive(message, messageProcessStatus);
            }
        }


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="listenerIdentity">A listener identity, defining this listener</param>
        public Listener (ListenerIdentity listenerIdentity) {
            this.pListenerIdentity = listenerIdentity;

            pExceptionThrownHandler = new AsyncExceptionThrownHandler(EmailTransportExceptionEvent_ExceptionThrown);

            if (listenerIdentity.ServiceType != null)
                CreateHost(listenerIdentity.ServiceType);
            else
                CreateHost(typeof(ServiceImplementation));
        }

        /// <summary>
        /// Default constructor
        /// </summary>
        public Listener() {
            pExceptionThrownHandler = new AsyncExceptionThrownHandler(EmailTransportExceptionEvent_ExceptionThrown);
            CreateHost(typeof(ServiceImplementation));
        }

        /// <summary>
        /// Aborts listening
        /// </summary>
        public void Abort () {
            ChangeState(ListenerState.faulted);
            if (pServiceHost != null)
                try {
                    pServiceHost.Abort();
                    pEmailBinding.ExceptionThrown -= pExceptionThrownHandler;
                }
                finally {
                    RemoveHost(pServiceHost);
                }
        }

        /// <summary>
        /// Creates the service host
        /// </summary>
        private void CreateHost(Type typeOfService) {
            ChangeState(ListenerState.initializing);
            // Create service host:
            pServiceHost = new ServiceHost(typeOfService);
            // Make sure only one endpoint has been given
            // Also make sure that it is a mail endpoint
            int numberOfEndpoints = pServiceHost.Description.Endpoints.Count;
            if (numberOfEndpoints != 1)
                throw new NoUniqueEndpointException(typeOfService);
            // We only support mail services
            if(pServiceHost.Description.Endpoints[0].Address.Uri.Scheme != "mailto")
                throw new EndpointTypeNotSupportedException(pServiceHost.Description.Endpoints[0].Address.Uri.Scheme);
            
            // Adds the behavior that will encrypt the bodies of WS-RM messages
            pServiceHost.Description.Behaviors.Add(new EncryptRmBodiesBehavior());
            // Override certificates
            OverrideConfig();
            // Add to static list of host/listener pairs:
            AddHost(this, pServiceHost);
            // Adds a callback for exceptions
            pEmailBinding.ExceptionThrown += pExceptionThrownHandler;
            pEmailBinding.OnInboxStateChange += new dk.gov.oiosi.communication.handlers.email.OnInboxStateChangeDelegate(pEmailBinding_OnInboxStateChange);
            ChangeState(ListenerState.initialized);
        }

        void pEmailBinding_OnInboxStateChange(dk.gov.oiosi.communication.handlers.email.InboxState newState) {
            switch(newState){
                case InboxState.Listening:
                    ChangeState(ListenerState.started);
                    break;
                case InboxState.AttemptingLogOn:
                    ChangeState(ListenerState.starting);
                    break;
            }
        }

        /// <summary>
        /// Callback for exceptions from the communications layer
        /// </summary>
        /// <param name="caller">The caller</param>
        /// <param name="ex"></param>
        private void EmailTransportExceptionEvent_ExceptionThrown(object caller, Exception ex) {
            ChangeState(ListenerState.faulted);
            if (ExceptionThrown != null)
                ExceptionThrown.Invoke(this, ex);
        }

        /// <summary>
        /// Starts listening
        /// </summary>
        public void Start() {
            if(State != ListenerState.stopped && State != ListenerState.initialized) throw new Exception("Invalid state change");
            ChangeState(ListenerState.starting);
            try {
                pServiceHost.Open();
            }
            catch (Exception ex) {
                RemoveHost(pServiceHost);
                throw new StartListeningFailedException(pServiceHost, ex);
            }
            
        }


        /// <summary>
        /// Stops the listening
        /// </summary>
        public void Stop () {

            if (State != ListenerState.faulted) ChangeState(ListenerState.stopping);

            if (pServiceHost != null ) {
                try {
                    pServiceHost.Close();
                }
                catch (Exception ex) {
                    throw new StopListeningFailedException(pServiceHost, ex);
                }
                finally {
                    RemoveHost(pServiceHost);
                }
            }

            pEmailBinding.ExceptionThrown -= pExceptionThrownHandler;
            if (State != ListenerState.faulted) ChangeState(ListenerState.stopped);

        }

        /// <summary>
        /// property for state of the listener
        /// </summary>
        public ListenerState State {
            get { return pState; }
            private set { pState = value; }
        }


        /// <summary>
        /// The identity of the listener, defining things like certificates and protocol type
        /// </summary>
        public ListenerIdentity Identity {
            get { return pListenerIdentity; }
        }
        
        /// <summary>
        /// Overrides the certificates given in app.config with the ones set programatically
        /// </summary>
        protected void OverrideConfig(){
            if (pListenerIdentity != null) {
                
            
                // Override certificate
                // TOOD: what is going on here? why only override the servicehost cridentials one time ?
                if (pListenerIdentity.ListenerCertificate != null && pServiceHost != null) {
                    pServiceHost.Credentials.ServiceCertificate.Certificate = pListenerIdentity.ListenerCertificate.Certificate;
                }
                else {
                    pListenerIdentity.ListenerCertificate = new OcesX509Certificate(pServiceHost.Credentials.ServiceCertificate.Certificate);
                }

            }
            else {
                CustomBinding customBinding = (CustomBinding)pServiceHost.Description.Endpoints[0].Binding;
                TransportBindingElement transportBinding = customBinding.Elements.Find<TransportBindingElement>();
                EmailTransport emailTransport = new EmailTransport(transportBinding);
                OcesX509Certificate ocesCertificate = new OcesX509Certificate(pServiceHost.Credentials.ServiceCertificate.Certificate);
                pListenerIdentity = new ListenerIdentity(emailTransport, ocesCertificate);
            }

            //Override transport binding
            pEmailBinding = GetEmailBinding();
            if (pListenerIdentity.Transport != null) {
                pEmailBinding.CopyElement((EmailBindingElement)pListenerIdentity.Transport.TransportBinding);
            }
            else {
                pListenerIdentity.Transport = new EmailTransport(pEmailBinding);
            }
        }
        
        private EmailBindingElement GetEmailBinding() {
            return ((CustomBinding)pServiceHost.Description.Endpoints[0].Binding).Elements.Find<EmailBindingElement>();
        }

        private void ChangeState(ListenerState newState) {

            // Check that only allowed state changes are made
            switch(State){
                case ListenerState.uninitialized:
                    if (!(newState == ListenerState.initializing || newState == ListenerState.faulted))
                        throw new Exception("Invalid state change. Uninitialized->" + newState.ToString());
                    break;
                case ListenerState.initializing:
                    if (!(newState == ListenerState.initialized || newState == ListenerState.faulted))
                        throw new Exception("Invalid state change. Initializing->" + newState.ToString());
                    break;
                case ListenerState.initialized:
                    if (!(newState == ListenerState.starting || newState == ListenerState.faulted))
                        throw new Exception("Invalid state change. Initialized->" + newState.ToString());
                    break;
                case ListenerState.starting:
                    if (!(newState == ListenerState.started || newState ==  ListenerState.stopping || newState == ListenerState.faulted))
                        throw new Exception("Invalid state change. Starting->" + newState.ToString());
                    break;
                case ListenerState.started:
                    if (!(newState == ListenerState.stopping || newState == ListenerState.faulted || newState == ListenerState.starting))
                        throw new Exception("Invalid state change. Started->" + newState.ToString());
                    break;
                case ListenerState.stopping:
                    if (!(newState == ListenerState.stopped || newState == ListenerState.faulted))
                        throw new Exception("Invalid state change. Stopping->" + newState.ToString());
                    break;
                case ListenerState.stopped:
                    if (!(newState == ListenerState.faulted))
                        throw new Exception("Invalid state change. Stopped->" + newState.ToString());
                    break;

                default:
                    throw new Exception("Invalid state change");
            }

            // Raise event
            switch (newState) {
                case ListenerState.initializing:
                    if (OnInitializing != null) OnInitializing.Invoke();
                    break;
                case ListenerState.initialized:
                    if (OnInitialized != null) OnInitialized.Invoke();
                    break;
                case ListenerState.starting:
                    if (OnStarting != null) OnStarting.Invoke();
                    break;
                case ListenerState.started:
                    if (OnStarted != null) OnStarted.Invoke();
                    break;
                case ListenerState.stopping:
                    if (OnStopping != null) OnStopping.Invoke();
                    break;
                case ListenerState.stopped:
                    if (OnStopped != null) OnStopped.Invoke();
                    break;
                case ListenerState.faulted:
                    if (OnFaulted != null) OnFaulted.Invoke();
                    break;
            }

            State = newState;

        }

        /// <summary>
        /// Event raised when the state is changed to Initializing
        /// </summary>
        public event ListenerStateChangeHandler OnInitializing;

        /// <summary>
        /// Event raised when the state is changed to Initialized
        /// </summary>
        public event ListenerStateChangeHandler OnInitialized;

        /// <summary>
        /// Event raised when the state is changed to Starting
        /// </summary>
        public event ListenerStateChangeHandler OnStarting;

        /// <summary>
        /// Event raised when the state is changed to Started
        /// </summary>
        public event ListenerStateChangeHandler OnStarted;

        /// <summary>
        /// Event raised when the state is changed to Stopping
        /// </summary>
        public event ListenerStateChangeHandler OnStopping;

        /// <summary>
        /// Event raised when the state is changed to Stopped
        /// </summary>
        public event ListenerStateChangeHandler OnStopped;

        /// <summary>
        /// Event raised when the state is changed to Faulted
        /// </summary>
        public event ListenerStateChangeHandler OnFaulted;

        #region IAsyncExceptionThrower Members

        /// <summary>
        /// Raised whenever an asynchronous exception is thrown (in a separate thread for instance. This event should always be listened to, to ensure that one is notified when the service stops running.
        /// </summary>
        public event AsyncExceptionThrownHandler ExceptionThrown;

        #endregion
    }
}