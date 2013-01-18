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
  * Portions created by Accenture and Avanade are Copyright (C) 2009
  * Danish National IT and Telecom Agency (http://www.itst.dk). 
  * All Rights Reserved.
  *
  * Contributor(s):
  *   Gert Sylvest, Avanade
  *   Jesper Jensen, Avanade
  *   Ramzi Fadel, Avanade
  *   Patrik Johansson, Accenture
  *   Dennis Søgaard, Accenture
  *   Christian Pedersen, Accenture
  *   Martin Bentzen, Accenture
  *   Mikkel Hippe Brun, ITST
  *   Finn Hartmann Jordal, ITST
  *   Christian Lanng, ITST
  *
  */
using System;
using System.Collections.Generic;
using System.Text;

using Lesnikowski.Mail.Headers.Constants;

using dk.gov.oiosi.exception.Keyword;

namespace dk.gov.oiosi.lesnikowskiMailProvider
{
    /// <summary>
    /// Exception thrown when the mail recieved has an invalid mime type.
    /// </summary>
    public class LesnikowskiInvalidMimeTypeException : LesnikowskiException
    {
        /// <summary>
        /// Constructor that takes the mime type and the mime sub type that is 
        /// invalid.
        /// </summary>
        /// <param name="mimeType"></param>
        /// <param name="mimeSubtypeName"></param>
        public LesnikowskiInvalidMimeTypeException(MimeType mimeType, string mimeSubtypeName) 
            : base(GetKeywords(mimeType, mimeSubtypeName))
        { 
        }

        private static Dictionary<string, string> GetKeywords(MimeType mimeType, string mimeSubtypeName)
        {
            Dictionary<string, string> keywords = KeywordFromString.GetKeyword("mimetype", mimeType.ToString());
            keywords.Add("mimesubtype", mimeSubtypeName);
            return keywords;
        }
    }
}
