using System;
using dk.gov.oiosi.communication;
using OiosiMessage=dk.gov.oiosi.communication.OiosiMessage;
using Response=dk.gov.oiosi.communication.Response;

namespace dk.gov.oiosi.raspProfile.communication {

    /// <summary>
    /// Extends Request's functionailty by adding Rasp custom headers to the message in the GetResponse and BeginGetResponse methods.
    /// </summary>
    public interface IRaspRequest {

        #region Methods

        /// <summary>
        /// Synchronously sends a request and gets a response
        /// </summary>
        /// <param name="request">The request to send </param>
        /// <param name="documentId">Document id</param>
        /// <param name="response">The response</param>
        void GetResponse(OiosiMessage request, string documentId, out Response response);

        /// <summary>
        /// Synchronously sends a request and gets a response
        /// </summary>
        /// <param name="request">The request to send </param>
        /// <param name="response">The response</param>
        /// <param name="documentId">Document id</param>
        [Obsolete("void GetResponse(OiosiMessage request, out Response response, string documentId", false)]
        void GetResponse(OiosiMessage request, out Response response, string documentId);

        /// <summary>
        /// Asynchronously starts sending a request
        /// </summary>
        IAsyncResult BeginGetResponse(OiosiMessage message, string documentId, out Response response, AsyncCallback callback);
       
        /// <summary>
        /// Asynchronously ends sending a request
        /// </summary>
        /// <returns>Response message</returns>
        void EndGetResponse(IAsyncResult asyncResult, out Response response);


        /// <summary>
        /// Shut-down
        /// </summary>
        void Close();

        /// <summary>
        /// Hard shut-down
        /// </summary>
        void Abort();

        #endregion

        #region Properties

        /// <summary>
        /// Credentials
        /// </summary>
        Credentials Credentials { get; set; }

        /// <summary>
        /// Policy describing how we will send our messages
        /// </summary>
        SendPolicy Policy { get; set;}

        /// <summary>
        /// Remote endpoint that messages will be sent to
        /// </summary>
        Uri RequestUri { get; }

        #endregion
    }
}
